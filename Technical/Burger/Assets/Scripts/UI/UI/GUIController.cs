using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Holoville.HOTween;

[System.Serializable]
public class InfoGUI
{
    public enum GUI_TYPE
    {
        NONE = 0,
        GAME_START = 1,
        WORLD_MAP = 2,
        READY_GO = 3,
        GAME_PLAY = 4,
        GAME_COMPLETE = 5,
        NEW_ITEM = 6,
        GAME_PAUSE =7,
        ADD_LIFE=8
    }
    public GUI_TYPE type;
    public GameObject objGUI;
    public GameObject obj;
}

public class GUIController : MonoSingleton<GUIController> {

    public GUIReady readyGo;
    public GUIEffect guiEffect;

    public List<InfoGUI> listGUI;
    
    public GameObject objGUICurrent;
    public GameObject objCurrent;

    public SpriteRenderer imageChangeScreen;
    Holoville.HOTween.Core.TweenDelegate.TweenCallback callback;
	// Use this for initialization
	void Start () {
        //callback = () => JoinWorldMap();
        InfoGUI gameStart = GetItemGUI(InfoGUI.GUI_TYPE.GAME_PLAY);

        if(gameStart != null)
        {
            if(gameStart.objGUI != null)
            {
                objGUICurrent = gameStart.objGUI;
            }
            if(gameStart.obj != null)
            {
                objCurrent = gameStart.obj;
            }
        }
        GUINodeWorldMap.Instance.btJoinGamePlay();
	}
	
    //khi nhan button Play se vao Wordmap
    public void btJoinWordMap()
    {
        
        ChangeScreen(1.5f);
        StartCoroutine(JoinWorldMap());
    }
    IEnumerator JoinWorldMap()
    {
        yield return new WaitForSeconds(1.5f);
       
        InfoGUI worldMap = GetItemGUI(InfoGUI.GUI_TYPE.WORLD_MAP);

        if (objGUICurrent != null)
        {
            objGUICurrent.SetActive(false);
        }
        if (objCurrent != null)
        {
            objCurrent.SetActive(false);
        }

        if (worldMap != null)
        {
            if (worldMap.objGUI != null)
            {
                worldMap.objGUI.SetActive(true);
                objGUICurrent = worldMap.objGUI;
            }
            if (worldMap.obj != null)
            {
                worldMap.obj.SetActive(true);
                objCurrent = worldMap.obj;
            }
        }
    }
    //khi nhan Button nay se Join vao Game Play
    public void btJoinGamePlay()
    {
        //ChangeScreen(1.5f);
        StartCoroutine(JoinGamePlay());
        
    }
    IEnumerator JoinGamePlay()
    {
        yield return new WaitForSeconds(0);
        //if (ListPositionCtrl.Instance.isMove == false)
        {
            InfoGUI gamePlay = GetItemGUI(InfoGUI.GUI_TYPE.GAME_PLAY);

            if (objGUICurrent != null)
            {
                objGUICurrent.SetActive(false);
            }
            if (objCurrent != null)
            {
                objCurrent.SetActive(false);
            }

            if (gamePlay != null)
            {
                if (gamePlay.objGUI != null)
                {
                    gamePlay.objGUI.SetActive(true);
                    objGUICurrent = gamePlay.objGUI;
                }
                if (gamePlay.obj != null)
                {
                    gamePlay.obj.SetActive(true);
                    objCurrent = gamePlay.obj;
                }
            }
            guiEffect.ResetPosition();
            StartReadyGo(true);
            GameController.Instance.ResetGame();
            readyGo.SetGoldTarget(GameData.Instance.listLevel[GameController.Instance.CurrentLevel].oneStar.ToString());
            readyGo.MoveIn();
            LiveManager.Instance.Live -= 1; 
        }
    
    }
    public void StartReadyGo(bool isActive)
    {
        InfoGUI readyGo = GetItemGUI(InfoGUI.GUI_TYPE.READY_GO);
        if (readyGo != null)
        {
            if (readyGo.objGUI != null)
            {
                readyGo.objGUI.SetActive(isActive);
            }
            if (readyGo.obj != null)
            {
                readyGo.obj.SetActive(isActive);

            }
        }
        //imageChangeScreen.color = new Color(imageChangeScreen.color.r, imageChangeScreen.color.g, imageChangeScreen.color.b, 0);
    }
    
    public void btNewItem()
    {
        InfoGUI newItem = GetItemGUI(InfoGUI.GUI_TYPE.NEW_ITEM);

        if (objGUICurrent != null)
        {
            objGUICurrent.SetActive(false);
        }
        if (objCurrent != null)
        {
            objCurrent.SetActive(false);
        }

        if (newItem != null)
        {
            if (newItem.objGUI != null)
            {
                newItem.objGUI.SetActive(true);
                objGUICurrent = newItem.objGUI;
            }
            if (newItem.obj != null)
            {
                newItem.obj.SetActive(true);
                objCurrent = newItem.obj;
            }
        }
    }
    [ContextMenu("OnComplete")]
    public void btOnComplete()
    {
        ChangeScreen(0.5f);
        StartCoroutine(OnCompete());
        
    }
    IEnumerator OnCompete()
    {
        yield return new WaitForSeconds(0.5f);
        InfoGUI onComplete = GetItemGUI(InfoGUI.GUI_TYPE.GAME_COMPLETE);

        if (objGUICurrent != null)
        {
            objGUICurrent.SetActive(false);
        }
        if (objCurrent != null)
        {
            objCurrent.SetActive(false);
        }

        if (onComplete != null)
        {
            if (onComplete.objGUI != null)
            {
                onComplete.objGUI.SetActive(true);
                objGUICurrent = onComplete.objGUI;
            }
            if (onComplete.obj != null)
            {
                onComplete.obj.SetActive(true);
                objCurrent = onComplete.obj;
            }
        }

        guiEffect.StartLevelComplete();
        yield return new WaitForSeconds(2f);

        if (LiveManager.Instance.Live < 1)
        {
            InfoGUI addLife = GetItemGUI(InfoGUI.GUI_TYPE.ADD_LIFE);
            addLife.objGUI.SetActive(true);
        }
    }
    public void btHome()
    {
        ChangeScreen(1.5f);
        StartCoroutine(Home());
    }
     IEnumerator Home()
    {
        yield return new WaitForSeconds(1.5f);
        InfoGUI home = GetItemGUI(InfoGUI.GUI_TYPE.GAME_START);
        InfoGUI pause = GetItemGUI(InfoGUI.GUI_TYPE.GAME_PAUSE);
        if (pause != null)
        {
            //pause.obj.SetActive(true);
            pause.objGUI.SetActive(false);
        }
        if (objGUICurrent != null)
        {
            objGUICurrent.SetActive(false);
        }
        if (objCurrent != null)
        {
            objCurrent.SetActive(false);
        }

        if (home != null)
        {
            if (home.objGUI != null)
            {
                home.objGUI.SetActive(true);
                objGUICurrent = home.objGUI;
            }
            if (home.obj != null)
            {
                home.obj.SetActive(true);
                objCurrent = home.obj;
            }
        }
    }
    public void btGamePause()
    {
        if (Clock.Instance.IsCounting == true)
        {
            InfoGUI pause = GetItemGUI(InfoGUI.GUI_TYPE.GAME_PAUSE);
            if (pause != null)
            {
                //pause.obj.SetActive(true);
                Clock.Instance.IsCounting = false;
                pause.objGUI.SetActive(true);
            }
        }
    }
    public void btResumne()
    {
        InfoGUI pause = GetItemGUI(InfoGUI.GUI_TYPE.GAME_PAUSE);
        if (pause != null)
        {
            //pause.obj.SetActive(true);
            Clock.Instance.IsCounting = true;
            pause.objGUI.SetActive(false);
        }
    }
    public void ChangeScreen(float time)
    {
        Color colorTo = imageChangeScreen.color;
        colorTo.a = 1;

        HOTween.To(imageChangeScreen, time, new TweenParms()
            .Prop("color", colorTo, false)
            .Loops(2, LoopType.Yoyo)
            //.OnComplete(() => Finish(1.5f))
            );
    }

    InfoGUI GetItemGUI(InfoGUI.GUI_TYPE type)
    {
        for(int i = 0; i<listGUI.Count;i++)
        {
            if(listGUI[i].type == type)
            {
                return listGUI[i];
            }
        }
        return null;
    }

    public void btRate()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.fuky.burger2");    
    }
}
