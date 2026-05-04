using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void SceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }
}
