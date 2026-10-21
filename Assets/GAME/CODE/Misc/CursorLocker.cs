using UnityEngine;

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
