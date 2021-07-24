using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevelOnClick : MonoBehaviour, ISetLevel
{
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

    public void LoadCasinoLevel()
    {
        SceneManager.LoadScene("NonCombatTestScene");
    }

    public void SetLevel(string _levelName)
    {
        levelName = _levelName;
    }
}
