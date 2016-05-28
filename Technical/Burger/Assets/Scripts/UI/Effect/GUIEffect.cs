using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using MadLevelManager;

[System.Serializable]

public class InfoDotween
{
    public enum TYPE
    {
        NONE = 0,
        TRAY = 1,
        DISH_BURGER = 2,
        DISH_FRUIT = 3,
        BURGER = 4,
        FRUIT = 5,
        ORDER = 6,

        LEVEL_COMPLETE_BG =7,
        LEVEL_COMPLETE_LEVEL = 8,
        LEVEL_COMPLETE_GET_FREE = 9,

        STAR_GOLD_1 = 10,
        STAR_GOLD_2 = 11,
        STAR_GOLD_3 = 12
    }
    public Transform transf;
    public TYPE type;
    public Vector3 posIn;
    public Vector3 posOut;
    public Vector3 posActive;
    public Vector3 scaleIn;
    public Vector3 scaleOut;
}
public class GUIEffect : MonoSingleton<GUIEffect>
{

    public GUIController guiController;
    public List<InfoDotween> listEffect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ResetPosition()
    {
        InfoDotween order = GetInItem(InfoDotween.TYPE.ORDER);
        order.transf.localPosition = order.posIn;

        InfoDotween tray = GetInItem(InfoDotween.TYPE.TRAY);
        tray.transf.localPosition = tray.posIn;

        InfoDotween itemBurger = GetInItem(InfoDotween.TYPE.DISH_BURGER);
        itemBurger.transf.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        InfoDotween itemFruit = GetInItem(InfoDotween.TYPE.DISH_FRUIT);
        itemFruit.transf.localScale = new Vector3(0.0f, 0.0f, 0.0f);

    }
    //Reset Order ve vi tri cu
    [ContextMenu("Reset")]
    void  Reset()
    {
        InfoDotween order = GetInItem(InfoDotween.TYPE.ORDER);
        order.transf.localPosition = order.posIn;

        InfoDotween tray = GetInItem(InfoDotween.TYPE.TRAY);
        tray.transf.localPosition = tray.posIn;
        CakeManager.Instance.NewBurger();

        //InfoDotween itemBurger = GetInItem(InfoDotween.TYPE.DISH_BURGER);
        //itemBurger.transf.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        //InfoDotween itemFruit = GetInItem(InfoDotween.TYPE.DISH_FRUIT);
        //itemFruit.transf.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        //FruitManager.Instance.HideFruit(); TODO: Clear drink
        DrinkManager.Instance.HideDrinkPlayer();

        //Debug
        //Debug.LogError("goi Order tu GUIEffect");
        Order();
    }
    //Show Order ra
    //Khay chay vao
    [ContextMenu("Order")]
    public void Order()
    {
        InfoDotween order = GetInItem(InfoDotween.TYPE.ORDER);
        order.transf.localPosition = order.posIn;
        HOTween.To(order.transf, 0.7f, new TweenParms()
           .Prop("localPosition", order.posActive, false)
           .Ease(EaseType.EaseInOutCubic)
           .OnComplete(()=>OnCompleteOrder())
           );

        InfoDotween tray = GetInItem(InfoDotween.TYPE.TRAY);
        tray.transf.localPosition = tray.posIn;
        HOTween.To(tray.transf, 0.7f, new TweenParms()
          .Prop("localPosition", tray.posActive, false)
          .Ease(EaseType.EaseInOutCubic)
          .OnComplete(()=>OnCommleteTray())
          );
    }
    
    //Khi finish xong Order, Order, Khay chay ve vi tri ban dau
    //
    [ContextMenu("Reset")]
    public void CompleteOrder()
    {
        InfoDotween order = GetInItem(InfoDotween.TYPE.ORDER);
        HOTween.To(order.transf, 0.7f, new TweenParms()
           .Prop("localPosition", order.posOut, false)
           .Ease(EaseType.EaseInOutCubic)
           //.OnComplete(() => Reset())
           );

        InfoDotween tray = GetInItem(InfoDotween.TYPE.TRAY);
        HOTween.To(tray.transf, 0.7f, new TweenParms()
          .Prop("localPosition", tray.posOut, false)
          .Ease(EaseType.EaseInOutCubic)
          .OnComplete(()=>Reset())
          );

        //InfoDotween item = GetInItem(InfoDotween.TYPE.DISH_BURGER);       
        //HOTween.To(item.transf, 0.7f, new TweenParms()
        //    .Prop("localPosition", item.posOut, false)
        //    .Ease(EaseType.EaseInOutCubic)
        //    );
        //InfoDotween itemFruit = GetInItem(InfoDotween.TYPE.DISH_FRUIT);
        //HOTween.To(itemFruit.transf, 0.7f, new TweenParms()
        //    .Prop("localPosition", itemFruit.posOut, false)
        //    .Ease(EaseType.EaseInOutCubic)
        //    );

        
    }
    //Khi them 1 Mieng banh vao List Burger
    [ContextMenu("AddItem")]
    public void AddItem()
    {
        InfoDotween item = GetInItem(InfoDotween.TYPE.BURGER);

        item.transf.localScale = item.scaleIn;// new Vector3(0.3f, 0.55f, 0.0f);
        HOTween.To(item.transf, 0.15f, new TweenParms()
            .Prop("localScale", item.scaleOut, false)
            .Ease(EaseType.EaseInOutBack)
            );
    }

    public void AddDrink()
    {
        InfoDotween item = GetInItem(InfoDotween.TYPE.DISH_FRUIT);

        item.transf.localScale = item.scaleIn;// new Vector3(0.3f, 0.55f, 0.0f);
        HOTween.To(item.transf, 0.15f, new TweenParms()
            .Prop("localScale", item.scaleOut, false)
            .Ease(EaseType.EaseInOutBack)
            );
    }

    //Hien cai Dia cua Burger
    void ShowDishBurger()
    {
        InfoDotween item = GetInItem(InfoDotween.TYPE.DISH_BURGER);
        item.transf.localPosition = item.posActive;
        item.transf.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        HOTween.To(item.transf, 0.25f, new TweenParms()
            .Prop("localScale", item.scaleIn, false)
            .Ease(EaseType.EaseOutQuad)
            );
    }
    //Hien cai Dia cua mam ngu qua
    void ShowDishFruit()
    {
        

        InfoDotween item = GetInItem(InfoDotween.TYPE.DISH_FRUIT);
        item.transf.localPosition = item.posActive;
        item.transf.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        HOTween.To(item.transf, 0.25f, new TweenParms()
            .Prop("localScale", item.scaleIn, false)
            .Ease(EaseType.EaseOutQuad)
            );

        InfoDotween itemFruit = GetInItem(InfoDotween.TYPE.FRUIT);
        itemFruit.transf.localScale = itemFruit.scaleIn;
        HOTween.To(itemFruit.transf, 0.25f, new TweenParms()
           .Prop("localScale", itemFruit.scaleOut, false)
           .Ease(EaseType.EaseOutQuad)
           );
    }    

    //Khi Qua man hieu ung Victory
    //
    [ContextMenu("Start Level Complete")]
    public void StartLevelComplete()
    {
        int star = GameController.Instance.GetStar();
        
        TextManager.Instance.SetTextType(TypeText.GOLD, "0");
        if(star > 0)
        {
            if (star == 1)
            {
                MadLevelProfile.SetLevelBoolean((GameController.Instance.CurrentLevel + 1).ToString(), "star_1", true);
                
            }
            if (star == 2)
            {
                MadLevelProfile.SetLevelBoolean((GameController.Instance.CurrentLevel + 1).ToString(), "star_1", true);
                MadLevelProfile.SetLevelBoolean((GameController.Instance.CurrentLevel + 1).ToString(), "star_2", true);

            }
            if (star == 3)
            {
                MadLevelProfile.SetLevelBoolean((GameController.Instance.CurrentLevel + 1).ToString(), "star_1", true);
                MadLevelProfile.SetLevelBoolean((GameController.Instance.CurrentLevel + 1).ToString(), "star_2", true);
                MadLevelProfile.SetLevelBoolean((GameController.Instance.CurrentLevel + 1).ToString(), "star_3", true);
            }
            Debug.Log("Level unlock: " + GameController.Instance.CurrentLevel.ToString());
            MadLevelProfile.SetCompleted((GameController.Instance.CurrentLevel + 1).ToString(), true);
            GUILevelComplete.Instance.SetGameComplete(true);
            GUIWorldMap.Instance.SetUnlook(GameController.Instance._burgerScore);
            GameController.Instance.CurrentLevel += 1;
            MyApplycation.Instance.analytics.LogEvent("GameEnd", "Win", " " + GameController.Instance._burgerScore, (int)Time.fixedTime);
            MyApplycation.Instance.analytics.LogEvent("PlayLevel", "Level " + MadLevel.currentLevelName, "Win", (int)Time.fixedTime);
        }
        else
        {
            MyApplycation.Instance.analytics.LogEvent("PlayLevel", "Level " + MadLevel.currentLevelName, "Lose", (int)Time.fixedTime);
        }
      
        //InfoDotween itemBG = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_BG);
        //itemBG.transf.position = itemBG.posIn;
        //HOTween.To(itemBG.transf, 0.7f, new TweenParms()
        //    .Prop("localPosition", itemBG.posActive, false)
        //    .Ease(EaseType.EaseOutQuad)
        //    );
        ResetPosStar();
        InfoDotween itemLv = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_LEVEL);
        itemLv.transf.localPosition = itemLv.posIn;
        HOTween.To(itemLv.transf, 0.5f, new TweenParms()
            .Prop("localPosition", itemLv.posActive, false)
            .Ease(EaseType.EaseOutQuad)
            .Delay(1f)
            );

        InfoDotween itemGetFree = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_GET_FREE);
        itemGetFree.transf.localPosition = itemGetFree.posIn;
        HOTween.To(itemGetFree.transf, 0.5f, new TweenParms()
            .Prop("localPosition", itemGetFree.posActive, false)
            .Ease(EaseType.EaseOutQuad)
            .Delay(1.5f)
            .OnComplete(() => SetStarLevel(star))
            );

    }

    public void Replay()
    {
        GameController.Instance.CurrentLevel--;
        FinishLevelComplete();
    }

    [ContextMenu("Finish Level Complete")]
    public void FinishLevelComplete()
    {
        //InfoDotween itemBG = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_BG);
        //HOTween.To(itemBG.transf, 0.7f, new TweenParms()
        //    .Prop("localPosition", itemBG.posOut, false)
        //    .Ease(EaseType.EaseOutQuad)
        //    .Delay(1.0f)
        //    .OnComplete(() => ResetLevelComplete())
        //    );
       
        InfoDotween itemLv = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_LEVEL);
        HOTween.To(itemLv.transf, 0.5f, new TweenParms()
            .Prop("localScale", itemLv.posOut, false)
            .Ease(EaseType.EaseOutQuad)
            .Delay(0.2f)
            );

        InfoDotween itemGetFree = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_GET_FREE);
        HOTween.To(itemGetFree.transf, 0.5f, new TweenParms()
            .Prop("localScale", itemGetFree.posOut, false)
            .Ease(EaseType.EaseOutQuad)
            .Delay(0.5f)
           .OnComplete(() => ResetLevelComplete())
            );
    }
    public void ResetLevelComplete()
    {
        //InfoDotween itemBG = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_BG);
        //itemBG.transf.localPosition = itemBG.posIn;
        if (LiveManager.Instance.Live > 0)
        {
            InfoDotween itemLv = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_LEVEL);
            itemLv.transf.localPosition = itemLv.posIn;
            itemLv.transf.localScale = itemLv.scaleIn;

            InfoDotween itemGetFree = GetInItem(InfoDotween.TYPE.LEVEL_COMPLETE_GET_FREE);
            itemGetFree.transf.localPosition = itemGetFree.posIn;
            itemGetFree.transf.localScale = itemGetFree.scaleIn;

            ResetPosStar();
            if (GameController.Instance.CurrentLevel > 0)
            {
                LevelInfo levelCurrent = GameData.Instance.listLevel[GameController.Instance.CurrentLevel - 1];
                LevelInfo levelNext = GameData.Instance.listLevel[GameController.Instance.CurrentLevel];
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
                    guiController.btJoinGamePlay();
                }
            }
            else
            {
                    guiController.btJoinGamePlay();

            }
        }
    }
    [ContextMenu("Reder Star")]
    public void SetStarLevel(int star)
    {
        if (star >= 1)
        {
            InfoDotween star1 = GetInItem(InfoDotween.TYPE.STAR_GOLD_1);
            star1.transf.localPosition = star1.posIn;
            star1.transf.gameObject.SetActive(true);
            HOTween.To(star1.transf, 0.5f, new TweenParms()
                .Prop("localPosition", star1.posActive, false)
                .Ease(EaseType.EaseOutQuad)
                .Delay(0.2f)
                .OnComplete(() => ParticleStarLevel(new Vector3(1.76f, 1.64f, 0)))
                );
        }
        if (star >= 2)
        {
            InfoDotween star2 = GetInItem(InfoDotween.TYPE.STAR_GOLD_2);
            star2.transf.localPosition = star2.posIn;
            star2.transf.gameObject.SetActive(true);
            HOTween.To(star2.transf, 0.5f, new TweenParms()
                .Prop("localPosition", star2.posActive, false)
                .Ease(EaseType.EaseOutQuad)
                .Delay(0.8f)
                .OnComplete(() => ParticleStarLevel(new Vector3(3.0f, 1.64f, 0)))
                );
        }
        if (star >= 3)
        {
            InfoDotween star3 = GetInItem(InfoDotween.TYPE.STAR_GOLD_3);
            star3.transf.localPosition = star3.posIn;
            star3.transf.gameObject.SetActive(true);
            HOTween.To(star3.transf, 0.5f, new TweenParms()
                .Prop("localPosition", star3.posActive, false)
                .Ease(EaseType.EaseOutQuad)
                .Delay(1.4f)
                .OnComplete(() => ParticleStarLevel(new Vector3(4.15f, 1.64f, 0)))
                );
        }
    }
    [ContextMenu("Star")]
    void Star()
    {
        ParticleStarLevel(Vector3.zero);
    }

    //Render Particle khi xuat hien 1 Sao vao vi tri
    public void ParticleStarLevel(Vector3 pos)
    {
        Debug.Log("Phao Bong");
        //ParticleController.Instance.RenderParticle(TypeParticle.PHAO_BONG_2, "Particle", pos);
    }

    //reset cac ci tri pos cua Star lai vi tri ban dau
    [ContextMenu("ResetPosStar")]
    public void ResetPosStar()
    {
        InfoDotween star1 = GetInItem(InfoDotween.TYPE.STAR_GOLD_1);
        star1.transf.localPosition = star1.posIn;
        star1.transf.gameObject.SetActive(false);
        
        InfoDotween star2 = GetInItem(InfoDotween.TYPE.STAR_GOLD_2);
        star2.transf.localPosition = star2.posIn;
        star2.transf.gameObject.SetActive(false);

        InfoDotween star3 = GetInItem(InfoDotween.TYPE.STAR_GOLD_3);
        star3.transf.localPosition = star3.posIn;
        star3.transf.gameObject.SetActive(false);
    }
    //tra ve gia tri da Order xong, cho nguoi choi thuc hien choi
    public bool OnCompleteOrder()
    {
        //test too

        //TestTool.Instance.TestEnum();

        return false;
    }

    //Khi khay chay vao dung vi tri thi show cac dia ra
    public bool OnCommleteTray()
    {
        //ShowDishBurger();
        //ShowDishFruit();
        //FruitManager.Instance.RandomFruit();
        //DrinkManager.Instance.RandomDrink();

        return true;
    }

    //lay Item theo type tu List
    InfoDotween GetInItem(InfoDotween.TYPE _type)
    {
        InfoDotween info;
        for (int i = 0; i < listEffect.Count; i++ )
        {
            if(listEffect[i].type == _type)
            {
                info = listEffect[i];
                return info;
            }
        }
        return null;
    }
    
}
