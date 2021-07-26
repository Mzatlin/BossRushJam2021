using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevelOnClick : MonoBehaviour, ISetLevel
{
    Camera cam => Camera.main;
    [SerializeField] string levelName;
    public void LoadLevel()
    {
        AkSoundEngine.PostEvent("Stop_All", cam.gameObject);
        AkSoundEngine.PostEvent("Play_Button_Click", cam.gameObject);
        SceneManager.LoadScene(levelName);
    }

    public void LoadTitle()
    {
        AkSoundEngine.PostEvent("Stop_All", cam.gameObject);
        AkSoundEngine.PostEvent("Play_Button_Click", cam.gameObject);
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
