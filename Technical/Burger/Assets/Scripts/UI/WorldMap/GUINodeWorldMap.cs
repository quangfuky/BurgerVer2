using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using MadLevelManager;

public class GUINodeWorldMap : MonoSingleton<GUINodeWorldMap>
{

    public ListBox listbox;

    public Text txtGoldTaget;

    public GameObject imageUnlook;
    public bool isUnlook;

    public List<Sprite> listSpriteDate;
    public List<Sprite> listSpriteStar;

    public Image imageDate;
    public Image imageStar;

    public GameObject AddLife;
    void Start()
    {
        //CheckUnlook();

        //SetUnlook();
        //string txt = listbox.content.text.ToString();
        //Debug.Log("txt = " + txt);

        //isUnlook = false;
        //imageUnlook.SetActive(false);
    }
    void Update()
    {
        //CheckUnlook();
        //SetUnlook();
        //SetDate();
        //SetTarget();
    }

    public void SetUnlook()
    {
        if (isUnlook == true)
        {
            imageUnlook.SetActive(false);
        }
        else
        {
            imageUnlook.SetActive(true);
        }
    }
    public void Unlook()
    {
        isUnlook = true;
        SetUnlook();
    }
    public void CheckUnlook()
    {
        string txt = listbox.content.text.ToString();
        //Debug.Log("txt = " + txt);
        int level = int.Parse(txt);
        int lv = GUIWorldMap.Instance.listScoreLevel.Count;
        if (level > lv + 1)
        {
            isUnlook = false;
        }
        else
        {
            isUnlook = true;
        }

        SetUnlook();

    }

    public void btJoinGamePlay()
    {
        if (LiveManager.Instance.Live > 0)
        {
            //if (isUnlook == true && ListPositionCtrl.Instance.isMove == false)
            {
                MyApplycation.Instance.analytics.LogEvent("GamePlay", "PlayGame", "", (int)Time.fixedTime);

                //string txt = listbox.content.text.ToString();
                //int level = int.Parse(txt);
                int level = int.Parse(MadLevel.currentLevelName);
                LevelInfo levelCurrent = GameData.Instance.listLevel[level - 2 > 0 ? level - 2 : 0];
                LevelInfo levelNext = GameData.Instance.listLevel[level - 1 > 0 ? level - 1 : 0];
                Debug.Log("Current lv: " + (level - 1 > 0 ? level - 1 : 0));
                Debug.Log("Next lv: " + (level - 0 > 0 ? level - 0 : 0));
                if (levelNext.maxUnlockCake > levelCurrent.maxUnlockCake || levelNext.maxUnlockFruit > levelCurrent.maxUnlockFruit)
                {
                    GUIController.Instance.btNewItem();
                    if (levelNext.maxUnlockCake > levelCurrent.maxUnlockCake)
                    {
                        GUINewItem.Instance.SetImageNewItem(levelNext.maxUnlockCake, true);
                    }
                    if (levelNext.maxUnlockFruit > levelCurrent.maxUnlockFruit)
                    {
                        GUINewItem.Instance.SetImageNewItem(levelNext.maxUnlockFruit, false);
                    }
                }
                else
                {
                    GameController.Instance.CurrentLevel = level - 1 > 0 ? level - 1 : 0;
                    GUIController.Instance.btJoinGamePlay();
                }

                //GUIWorldMap.Instance.nodeCurrent = gameObject.GetComponent<GUINodeWorldMap>();
            }
        }
        else
        {
            if (AddLife != null)
            {
                AddLife.SetActive(true);

            }
        }
    }
    void SetDate()
    {
        string txt = listbox.content.text.ToString();
        int level = int.Parse(txt);

        int index = (level - 1) / 10;
        if (listSpriteDate[index] != null)
            imageDate.sprite = listSpriteDate[index];
    }

    void SetTarget()
    {
        string txt = listbox.content.text.ToString();
        int level = int.Parse(txt);

        txtGoldTaget.text = GameData.Instance.listLevel[level-1].oneStar.ToString();
    }
}
