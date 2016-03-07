using UnityEngine;
using System.Collections;
using MadLevelManager;

public class GUIWorld : MonoSingleton<GUIWorld> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btJoinGamePlay()
    {
        if (LiveManager.Instance.Live > 0)
        {
            //if (/*isUnlook == true && */ ListPositionCtrl.Instance.isMove == false)
            {
                //MyApplycation.Instance.analytics.LogEvent("GamePlay", "PlayGame", "", (int)Time.fixedTime);

                //string txt = listbox.content.text.ToString();
                int level = int.Parse(MadLevel.currentLevelName);
                LevelInfo levelCurrent = GameData.Instance.listLevel[level - 2 > 0 ? level - 2 : 0];
                LevelInfo levelNext = GameData.Instance.listLevel[level - 1 > 0 ? level - 1 : 0];


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
            //AddLife.SetActive(true);
        }
    }
}
