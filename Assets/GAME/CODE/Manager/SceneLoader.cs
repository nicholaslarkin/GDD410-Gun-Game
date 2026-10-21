using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndex
{
    TestLevel = 0,
    MovementTestLevel = 1,
    Shop = 2,
}

public class SceneLoader : MonoBehaviour
{
    public SceneIndex sceneToGoTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHurtbox"))
        {
            LoadScene(sceneToGoTo);
        }
    }
    private void LoadScene(SceneIndex scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
