using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene transition via build scenes
/// </summary>

// NOTE: Functionality changes with each build, refactor properly when consistent levels are present
public enum SceneIndex
{
    TestLevel = 0,
    MovementTestLevel = 1,
    Shop = 2,
    PrototypeMenu = 3,
}

public class SceneLoader : MonoBehaviour
{
    public SceneIndex sceneToGoTo;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneIndex.PrototypeMenu.ToString());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHurtbox"))
        {
            LoadScene(sceneToGoTo);
        }
    }
    public void LoadScene(SceneIndex scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadSceneFromShop()
    {
        SceneManager.LoadScene(SceneIndex.MovementTestLevel.ToString());
    }

    public void LoadTestLevel()
    {
        SceneManager.LoadScene(SceneIndex.TestLevel.ToString());
    }
}
