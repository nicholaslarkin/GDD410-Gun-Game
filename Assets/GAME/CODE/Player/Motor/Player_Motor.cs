using UnityEngine;

public class Player_Motor : MonoBehaviour
{
    #region VARIABLES

    [Header("Components")]
    public Rigidbody rb;
    public Player_Input input;
    public Transform playerCamera;

    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float deceleration;

    [Header("Mouse Look")]
    public float mouseSensitivity;
    public float cameraPitchLimit;

    [Header("Gravity")]
    public bool useGravity = true;
    public float gravity;
    public float terminalVelocity;

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
    }


    private void Update()
    {
        //handling camera in Update since mouse movement is frame-based
        Look();
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

        velocity.y +=
            gravity * Time.fixedDeltaTime;

        velocity.y = Mathf.Max(
            velocity.y,
            terminalVelocity
        );
    }

    #endregion
}