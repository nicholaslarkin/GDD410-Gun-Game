using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("PrototypeMenu");
        }
    }

    public void LoadTestLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void LoadMVPLevel()
    {
        SceneManager.LoadScene("MVPLevel");
    }

    public void LoadPressureLevel()
    {
        SceneManager.LoadScene("PressureLevel");
    }
}
