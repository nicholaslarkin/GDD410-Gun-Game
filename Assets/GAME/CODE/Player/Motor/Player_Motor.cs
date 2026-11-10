using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls motor functions and input registration for other scripts to read and run their codes
/// when told to do so
/// </summary>

//NOTE: Move anything that takes input into its own scripts later? (cut out the middle man)
public class Player_Motor : MonoBehaviour
{
    #region VARIABLES

    [Header("Components")]
    public Rigidbody rb;
    public Player_Input input;
    public Transform playerCamera;
    public Player_Statistics playerStats; //changed from Weapon for testing
    public Player_Weapon playerWeapon;

    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float deceleration;

    [Header("Jump")]
    public float jumpForce;
    public float chargeJumpForce;

    public float currentCoyoteTime;
    public float maxCoyoteTime;

    public int currentJumpCount;
    public int maxJumpCount;

    [Header("Mouse Look")]
    public float mouseSensitivity;
    public float cameraPitchLimit;

    [Header("Gravity")]
    public bool useGravity = true;
    public float gravity;
    public float terminalVelocity;
    public float fallForce;

    [Header("Grounded")]
    public bool wasGrounded;
    public bool Grounded;
    public float GroundedOffset;
    public float GroundedRadius;
    public float upwardSlopeInfluence;
    public float downwardSlopeInfluence;
    public LayerMask GroundLayers;

    [Header("Runtime")]
    public Vector3 velocity;
    public readonly Vector2 moveInput;
    public readonly Vector2 lookInput;

    private float cameraPitch;

    #endregion


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<Player_Input>();
        playerStats = GetComponent<Player_Statistics>();
        playerWeapon = GetComponent<Player_Weapon>();

        /*Scene currentScene = SceneManager.GetActiveScene();

        //
        //Fix having input in shop scene later; this doesnt work because it doesnt destroy on load
        //
        if (currentScene.name != "Shop")
        {
            input = GetComponent<Player_Input>();
        }*/
    }

    private void Start()
    {
        StartCoroutine(Footsteps());
    }

    private void Update()
    {
        //handling camera in Update since mouse movement is frame-based
        Look();
        CameraLean();

        WeaponSwap();
        Jump();
        Reloading();
        Shooting();

        if (Grounded)
        {
            currentJumpCount = maxJumpCount;
        }
    }


    private void FixedUpdate()
    {
        velocity = rb.linearVelocity;

        Move();
        Gravity();

        rb.linearVelocity = velocity;
    }

    #region MOVEMENT

    private void Move()
    {
        Vector3 cameraForward = playerCamera.forward;
        Vector3 cameraRight = playerCamera.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection =
            cameraRight * input.movement.x +
            cameraForward * input.movement.y;

        if (moveDirection.sqrMagnitude > 1f)
            moveDirection.Normalize();

        Vector3 desiredVelocity =
            moveDirection * moveSpeed;

        Vector3 currentHorizontalVelocity =
            new Vector3(
                velocity.x,
                0f,
                velocity.z
            );

        float currentAcceleration =
            moveDirection.sqrMagnitude > 0.01f
            ? acceleration
            : deceleration;

        Vector3 velocityChange =
            desiredVelocity - currentHorizontalVelocity;

        velocityChange = Vector3.ClampMagnitude(
            velocityChange,
            currentAcceleration * Time.fixedDeltaTime
        );

        currentHorizontalVelocity += velocityChange;

        velocity.x = currentHorizontalVelocity.x;
        velocity.z = currentHorizontalVelocity.z;
    }

    private IEnumerator Footsteps()
    {
        while (true)
        {
            if (Grounded && (velocity.x != 0f || velocity.z != 0f))
            {
                AudioManager.Instance.PlaySFXMult("Footsteps", 0.2f);

                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                yield return null;
            }
        }
    } 
    #endregion

    #region JUMP
    private void Jump()
    {
        if (input.jumpPressed)
        {
            if (currentJumpCount == 0)
            {
                return;
            }

            AudioManager.Instance.PlaySFXMult("Player_Jump", 0.5f);

            currentJumpCount--;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }
    #endregion

    #region SHOOTING
    private void Shooting()
    {
        Weapon currentWeapon = playerWeapon.CurrentWeapon;

        if (input.shootPressed && currentWeapon.WeaponType != WeaponTypes.AssaultRifle)
            currentWeapon.Shot();
        else if (input.shoot && currentWeapon.WeaponType == WeaponTypes.AssaultRifle)
            currentWeapon.Shot();
    }

    private void Reloading()
    {
        if (input.reloadPressed)
        {
            Weapon currentWeapon = playerWeapon.CurrentWeapon;
            currentWeapon.Reload();
        }
    }
    #endregion

    #region WEAPON TOGGLE
    private void WeaponSwap() //Legacy Input Implementation
    {
        Vector2 scrollDelta = Input.mouseScrollDelta;

        if (input.weaponSwapPressed)
        {
            if (scrollDelta.y > 0) //up
            {
                playerWeapon.WeaponSwap(input.weaponSwapValue, 1);
            }
            if (scrollDelta.y < 0) //down
            {
                playerWeapon.WeaponSwap(input.weaponSwapValue, 2);
            }
            else
            {
                playerWeapon.WeaponSwap(input.weaponSwapValue, 0);
            }
        }
    }
    #endregion

    private float cameraLean;

    #region LOOK
    private void Look()
    {
        if (!input.cursorInputForLook)
            return;

        float mouseX = input.look.x * mouseSensitivity * Time.deltaTime;
        float mouseY = input.look.y * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -cameraPitchLimit, cameraPitchLimit);

        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, cameraLean);
    }
    #endregion


    #region GRAVITY
    private void Gravity()
    {
        if (!useGravity)
            return;

        if (!Grounded && useGravity)
        {
            if (velocity.y > 0)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
                velocity.y = Mathf.Max(velocity.y, terminalVelocity);
            }
            else
            {
                velocity.y -= fallForce * Time.fixedDeltaTime;
                velocity.y = Mathf.Max(velocity.y, terminalVelocity);
            }
        }
    }
    #endregion

    #region CAMERA
    private void CameraLean()
    {
        float targetLean = 0f;

        if (input.movement.x > 0)
            targetLean = 2.5f;
        else if (input.movement.x < 0)
            targetLean = -2.5f;

        cameraLean = Mathf.MoveTowardsAngle(cameraLean, targetLean, 100f * Time.deltaTime);
    }
}
    #endregion