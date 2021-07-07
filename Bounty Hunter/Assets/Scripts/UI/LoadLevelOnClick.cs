using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevelOnClick : MonoBehaviour
{
    [SerializeField] string levelName;
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
