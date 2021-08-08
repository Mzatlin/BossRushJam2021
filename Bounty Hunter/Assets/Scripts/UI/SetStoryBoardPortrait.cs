using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStoryBoardPortrait : MonoBehaviour, IPortrait
{
    protected List<List<Sprite>> daySprites = new List<List<Sprite>>();
    [SerializeField] Image portrait;
    [SerializeField] CurrentDaySO day;
    [SerializeField] int offset = 1;

    public List<Sprite> firstDeath;
    public List<Sprite> secondDeath;
    public List<Sprite> thirdDeath;

    List<Sprite> currentList = new List<Sprite>();

    Sprite lastPortrait;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSprites();
        SetCurrentList();

        lastPortrait = currentList[0];
        SetPortrait(0);
        if (portrait == null)
        {
            Debug.Log("No Image Assigned!");
        }
    }

    void SetCurrentList()
    {
        if(day != null)
        {
            currentList = daySprites[day.currentDay - offset];
        }
    }

    void InitializeSprites()
    {
        daySprites.Add(firstDeath);
        daySprites.Add(secondDeath);
        daySprites.Add(thirdDeath);
    }

    public void SetPortrait(int index)
    {
        if (index < currentList.Count)
        {
            lastPortrait = currentList[index];
            portrait.sprite = currentList[index];
        }
        else
        {
            if (lastPortrait != null)
            {
                portrait.sprite = lastPortrait;
            }
        }

    }
}
