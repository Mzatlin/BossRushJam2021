using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevelOnClick : MonoBehaviour
{
    AudioManager audio => FindObjectOfType<AudioManager>();
    Camera cam => Camera.main;
    [SerializeField] string levelName;
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene(0);
    }
}
