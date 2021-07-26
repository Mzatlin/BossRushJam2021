using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlavorTextController : MonoBehaviour
{
    IInteract interact;
    public Canvas nameCanvas;
    public TextMeshProUGUI flavorText;
    public string text;
    bool isShowingPlate;
    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<IInteract>();
        if (nameCanvas != null)
        {
            nameCanvas.enabled = false;
        }
        else
        {
            Debug.Log("No Canvas Assigned!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (interact != null && interact.IsCurrentingInteracting)
        {
            ShowPlate();
        }
        else if (isShowingPlate)
        {
            HidePlate();
        }
    }

    void ShowPlate()
    {
        if (nameCanvas != null && flavorText != null)
        {
            nameCanvas.enabled = true;
            flavorText.text = text;
            isShowingPlate = true;
        }
    }

    void HidePlate()
    {
        if (nameCanvas != null)
        {
            nameCanvas.enabled = false;
            isShowingPlate = false;
        }
    }

    public void DisableNamePlate()
    {
        nameCanvas.enabled = false;
    }
}
