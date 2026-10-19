using UnityEngine;

public class Player_Motor : MonoBehaviour
{
    #region VARIABLES

    [Header("Components")]
    public Rigidbody rb;
    public GameObject raycastPointer;
    public LayerMask hurtbox;
    public Player_Input input;
    public Transform playerCamera;
    public Player_Statistics playerStats; //changed from Weapon for testing
    public Player_Weapon weaponSwap;

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
    public Vector2 moveInput;
    public Vector2 lookInput;

    private float cameraPitch;

    #endregion


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<Player_Input>();
        playerStats = GetComponent<Player_Statistics>();
        weaponSwap = GetComponent<Player_Weapon>();

        raycastPointer = GameObject.Find("RaycastPointer");
    }


    private void Update()
    {
        //Needs to be in update for weapon swapping; change later if performance hit
        playerStats = GetComponentInChildren<Player_Statistics>();

        //handling camera in Update since mouse movement is frame-based
        Look();

        WeaponSwap();
        Jump();
        Interact();
        Reloading();
        Shooting();

        if (Grounded && !wasGrounded)
        {
            currentJumpCount = maxJumpCount;
        }
    }


    private void FixedUpdate()
    {
        SetMoveInput();

        velocity = rb.linearVelocity;

        Move();
        Gravity();

        rb.linearVelocity = velocity;
    }


    #region INPUT

    private void SetMoveInput()
    {
        moveInput = input.movement;
    }

    #endregion


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
            cameraRight * moveInput.x +
            cameraForward * moveInput.y;

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
    #endregion

    #region JUMP
    private void Jump()
    {
        if (input.jumpPressed)
        {
            if (currentJumpCount == 0)
                return;
            else
                currentJumpCount--;

            float currentJumpForce = jumpForce; //kept for future jump height changes for different states

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, currentJumpForce, rb.linearVelocity.z);
        }
    }
    #endregion

    #region SHOOTING

    private void Shooting()
    {
        if (!input.shootPressed)
            return;

        if (playerStats == null || weaponSwap == null)
            return;

        Weapon currentWeapon = weaponSwap.CurrentWeapon;

        if (currentWeapon == null || currentWeapon.AmmoClip <= 0)
            return;

        if (raycastPointer == null)
            return;

        Vector3 origin = raycastPointer.transform.position;
        Vector3 direction = raycastPointer.transform.forward;

        if (Physics.Raycast(
            origin,
            direction,
            out RaycastHit hit,
            currentWeapon.Data.maxRange,
            hurtbox))
        {
            Debug.DrawRay(
                origin,
                direction * hit.distance,
                Color.green,
                3f
            );

            playerStats.Shot(true, hit);
        }
        else
        {
            Debug.DrawRay(
                origin,
                direction * currentWeapon.Data.maxRange,
                Color.red,
                3f
            );

            playerStats.Shot(false, default);
        }
    }

    private void Reloading()
    {
        if (input.reloadPressed)
        {
            playerStats.Reload();
        }
    }

    #endregion

    #region INTERACT
    private void Interact()
    {
        // Refactor and merge this functionality with Shooting as well later

        if (input.interactPressed)
        {
            RaycastHit hit;

            if (Physics.Raycast(raycastPointer.transform.position, raycastPointer.transform.TransformDirection(Vector3.forward), out hit, 1f))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                interactable?.PickupItem();
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 3f);
                Debug.Log("Pick up Item!");
            }
            else
            {
                Debug.DrawRay(raycastPointer.transform.position, raycastPointer.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 3f);
                Debug.Log("No Item to pick up!");
            }
        }
    }
    #endregion

    #region WEAPON TOGGLE
    private void WeaponSwap()
    {
        if (input.weaponSwapPressed)
        {
            weaponSwap.WeaponSwap(input.weaponSwapValue);
        }
    }
    #endregion


    #region LOOK

    private void Look()
    {
        if (!input.cursorInputForLook)
            return;

        lookInput = input.look;

        float mouseX =
            lookInput.x * mouseSensitivity;

        float mouseY =
            lookInput.y * mouseSensitivity;

        transform.Rotate(
            Vector3.up * mouseX
        );

        // Rotate camera up/down.
        cameraPitch -= mouseY;

        cameraPitch = Mathf.Clamp(
            cameraPitch,
            -cameraPitchLimit,
            cameraPitchLimit
        );

        playerCamera.localRotation =
            Quaternion.Euler(
                cameraPitch,
                0f,
                0f
            );
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
                velocity.y +=
                    gravity * Time.fixedDeltaTime;

                /*if (playerBase.INPUT.jump && !Spinning) // controls jump height based on how long button is pressed
                    gravityFlux = 3f;
                else
                    gravityFlux = 10f;*/

                velocity.y = Mathf.Max(velocity.y, terminalVelocity);
            }
            else
            {
                //velocity.y = -10f;
                velocity.y -= fallForce * Time.fixedDeltaTime; //Falling Down
                velocity.y = Mathf.Max(velocity.y, terminalVelocity);
            }
        }
    }
    #endregion
}