using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour, IPause
{
    public Canvas pauseCanvas;
    public List<GameObject> subCanvases = new List<GameObject>();
    IPlayerStats player => GetComponent<IPlayerStats>();
    bool isPaused;
    GameObject mainPanel;
    // Start is called before the first frame update
    void Start()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = false;
            Cursor.visible = true; //change the cursor
            SetupCanvases();
        }
    }

    void SetupCanvases()
    {
        if(subCanvases.Count >= 1)
        {
            mainPanel = subCanvases[0];
        }
        for (int i = 1; i < subCanvases.Count; i++)
        {
            subCanvases[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !player.GetPlayerDeath())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandlePause();
            }
        }
    }

    void HandlePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseCanvas.enabled = false;
           // Cursor.visible = false;
            player.SetPlayerPaused(false);
            DisableSubMenus();
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseCanvas.enabled = true;
           // Cursor.visible = true;
            player.SetPlayerPaused(true);
            if(mainPanel != null)
            {
                mainPanel.SetActive(true);
            }
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        HandlePause();
    }

    void DisableSubMenus()
    {
        if (subCanvases.Count > 0)
        {
            foreach (GameObject can in subCanvases)
            {
                can.SetActive(false);
            }
        }
    }


    public void SetPause()
    {
        isPaused = false;
    }
}
