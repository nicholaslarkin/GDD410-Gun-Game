using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class Player_Input : MonoBehaviour
{
    [Header("Character Input Values")]

    [Header("Vectors")]
    public Vector2 movement;
    public Vector2 look;

    [Header("Jump")]
    public bool jump;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool jumpReleased;

    [Header("Mouse Cursor Settings")]
    public bool cursorInputForLook = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

#if ENABLE_INPUT_SYSTEM
    public void OnMovement(InputValue value)
    {
        MovementInput(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        bool pressed = value.isPressed;

        if (pressed && !jump)
        {
            jumpPressed = true;
        }

        if (!pressed && jump)
        {
            jumpReleased = true;
        }

        jump = pressed;
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }
#endif

    public void MovementInput(Vector2 newMoveDirection)
    {
        movement = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    void LateUpdate()
    {
        jumpPressed = false;
        jumpReleased = false;
    }
}
