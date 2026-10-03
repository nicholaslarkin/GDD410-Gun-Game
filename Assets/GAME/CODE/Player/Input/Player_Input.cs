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

    [Header("Shoot")]
    public bool shoot;
    [HideInInspector] public bool shootPressed;
    [HideInInspector] public bool shootReleased;

    [Header("Reload")]
    public bool reload;
    [HideInInspector] public bool reloadPressed;
    [HideInInspector] public bool reloadReleased;

    [Header("Interact")]
    public bool interact;
    [HideInInspector] public bool interactPressed;
    [HideInInspector] public bool interactReleased;

    [Header("WeaponSwap")]
    public bool weaponSwap;
    public int weaponSwapValue;
    [HideInInspector] public bool weaponSwapPressed;
    [HideInInspector] public bool weaponSwapReleased;

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

    public void OnShoot(InputValue value)
    {
        bool pressed = value.isPressed;

        if (pressed && !shoot)
        {
            shootPressed = true;
        }

        if (!pressed && shoot)
        {
            shootReleased = true;
        }

        shoot = pressed;
    }

    public void OnReload(InputValue value)
    {
        bool pressed = value.isPressed;

        if (pressed && !reload)
        {
            reloadPressed = true;
        }

        if (!pressed && reload)
        {
            reloadReleased = true;
        }

        reload = pressed;
    }

    public void OnInteract(InputValue value)
    {
        bool pressed = value.isPressed;

        if (pressed && !interact)
        {
            interactPressed = true;
        }

        if (!pressed && interact)
        {
            interactReleased = true;
        }

        interact = pressed;
    }

    public void OnWeapon1(InputValue value)
    {
        if (value.isPressed)
            weaponSwapValue = 1;
    }

    public void OnWeapon2(InputValue value)
    {
        if (value.isPressed)
            weaponSwapValue = 2;
    }

    public void OnWeapon3(InputValue value)
    {
        if (value.isPressed)
            weaponSwapValue = 3;
    }

    public void OnWeaponSwap(InputValue value) 
    { 
        bool pressed = value.isPressed; 
        
        if (pressed && !weaponSwap) 
        { 
            weaponSwapPressed = true; 
        } 
        
        if (!pressed && weaponSwap) 
        { 
            weaponSwapReleased = true; 
        } 
        
        weaponSwap = pressed; 
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

    public void ShootInput(bool newShootState)
    {
        shoot = newShootState;
    }

    public void ReloadInput(bool newReloadState)
    {
        shoot = newReloadState;
    }

    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }

    public void WeaponSwapInput(bool newWeaponSwapState)
    {
        weaponSwap = newWeaponSwapState;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    void LateUpdate()
    {
        jumpPressed = false;
        jumpReleased = false;
        shootPressed = false;
        shootReleased = false;
        reloadPressed = false;
        reloadReleased = false;
        interactPressed = false;
        interactReleased = false;
        weaponSwapPressed = false;
        weaponSwapReleased = false;
    }
}
