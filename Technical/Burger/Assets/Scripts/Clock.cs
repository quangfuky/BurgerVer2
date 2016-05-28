using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Clock : MonoSingleton<Clock>
{
    public float Time;
    public float CakeTime;
    public float CountTime;
    public float CountCakeTime;
    public bool IsCounting = false;

    public Image Timer;
    void Start()
    {
        Time = GameData.Instance.listLevel[GameController.Instance.CurrentLevel].timeGame;
        CountTime = Time;
    }
    void Update()
    {
        if (IsCounting)
        {
            IncreaseTime();
            CheckTime();
        }
    }

    public void IncreaseTime()
    {
        CountTime -= UnityEngine.Time.deltaTime;
        CountCakeTime += UnityEngine.Time.deltaTime;
        Timer.fillAmount = 1 - (CountCakeTime/20f);
        if (CountCakeTime > 20)
        {
            //FruitManager.Instance.correct = 5;
            DrinkManager.Instance.ResetOrder();
            GUIEffect.Instance.CompleteOrder();
            Reset();
        }
        TextManager.Instance.SetTextType(TypeText.TIME_GAME, ((int)CountTime).ToString());
    }

    public void CheckTime()
    {
        if (CountTime <= 11)
        {
            AudioController.Instance.PlayAudioEffect(TypeAudio.COUNTDOWN);
        }
        if (CountTime <=0)
        {
            AudioController.Instance.StopAudioEffect();
            AudioController.Instance.PlayAudioEffect(TypeAudio.END);
            CountTime = 60;

            IsCounting = false;

            
            //GameController.Instance.GetStar();
            ShowResult();
        }
        //if (CountCakeTime >= CakeTime)
        //{
        //    CountCakeTime = 0;
        //    StartCoroutine(CakeManager.Instance.NewBurger(true));
        //}
    }

    public void Reset()
    {
        CountCakeTime = 0;
    }

    public void ShowResult()
    {
        //FruitManager.Instance.ClearFruit();
        //Debug.Log("Chon worldmap");
        GUIController.Instance.btOnComplete();
        TextManager.Instance.SetTextType(TypeText.SCORE_COMPLETE, GameController.Instance._burgerScore.ToString());
        // replay in tesst mode
//#if UNITY_STANDALONE && UNITY_EDITOR
        TestTool.Instance.Replay();
//#endif
    }
}
