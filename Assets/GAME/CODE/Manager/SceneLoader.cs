using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Handles scene transition via build scenes
/// </summary>

// NOTE: Functionality changes with each build, refactor properly when consistent levels are present
public enum SceneIndex
{
   MVPLevel = 1,
   Nov4Level = 2,
   CombatPure = 3,
   PlatformingPure = 4,
   PrototypeMenu = 5,
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
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToGoTo.ToString());
    }

    public void LoadScene(SceneIndex scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public IEnumerator BuildLoadSceneUponDeath()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneIndex.PrototypeMenu.ToString());
    }

    /*public void LoadSceneFromShop()
    {
        SceneManager.LoadScene(SceneIndex.MovementTestLevel.ToString());
    }

    public void LoadTestLevel()
    {
        SceneManager.LoadScene(SceneIndex.TestLevel.ToString());
    }*/
}
