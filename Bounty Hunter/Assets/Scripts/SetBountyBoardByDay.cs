using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct BountyBoardEntry
{
    public Sprite bossImage;
    public string bossName;
    public string sceneName;
}


public class SetBountyBoardByDay : MonoBehaviour
{
    [SerializeField] Image boardImage;
    [SerializeField] TextMeshProUGUI boardName;
    [SerializeField] Button boardLevelButton;
    ISetLevel level;

    [SerializeField] CurrentDaySO day;
    [SerializeField] List<BountyBoardEntry> boardEntries = new List<BountyBoardEntry>();
    // Start is called before the first frame update
    void Start()
    {
        level = boardLevelButton.GetComponent<ISetLevel>();
        SetBountyBoard();
    }

    void SetBountyBoard()
    {
        if(day.currentDay < boardEntries.Count)
        {
            boardImage.sprite = boardEntries[day.currentDay].bossImage;
            boardName.text = boardEntries[day.currentDay].bossName;
            if (level != null)
            {
                level.SetLevel(boardEntries[day.currentDay].sceneName);
            }
        }
    }
}
