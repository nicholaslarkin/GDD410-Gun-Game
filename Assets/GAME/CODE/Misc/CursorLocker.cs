using UnityEngine;

/// <summary>
/// Scene specific instruction on if the cursor should be locked or not
/// Usually just placed on its own unique object when needed
/// </summary>

public enum CursorMode
{
    Unlock = 0,
    Lock = 1,
}

public class CursorLocker : MonoBehaviour
{
    public CursorMode mode;

    private void Awake()
    {
        if (mode == CursorMode.Unlock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (mode == CursorMode.Lock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
