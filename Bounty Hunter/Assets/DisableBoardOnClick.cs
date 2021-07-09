using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoardOnClick : MonoBehaviour
{
    [SerializeField] Canvas board;
    [SerializeField] PlayerStatsSO player;
    IInteract interact;
    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<IInteract>();
    }

    public void BoardDisable()
    {
        if(board != null && player != null)
        {
            board.enabled = false;
            player.isReady = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BoardDisable();
        }
    }
}
