using System.Collections.Generic;
using MadLevelManager;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int CurrentLevel = 0;

    public bool Randomizing = false;

    public int _burgerScore = 0;

    //Calcalate score
    public List<int> PieceList = new List<int>();
    public List<int> TimeList = new List<int>();
    public List<int> ScoreList = new List<int>();
    public List<int> MinScore = new List<int>();

    public List<int> DrinkNumber = new List<int>(); 
    public List<int> DrinkScore = new List<int>(); 
    public List<int> DrinkMin = new List<int>();
    public List<int> DrinkTime = new List<int>(); 

    public int PrePieceNum = 0;
    private int score = 0;

    public GameObject number;

    public bool RandomFruit = false;
    public Image OneStar;
    public Image TwoStar;
    public Image ThreeStar;
    public Image ProgressBar;

    public GameObject OneGold;
    public GameObject TwoGold;
    public GameObject ThreeGold;

    public bool One = false;
    public bool Two = false;
    public bool Three = false;

    public GameObject life;

    void Awake()
    {
        int a;
        if (int.TryParse(MadLevel.currentLevelName, out a))
        {
            CurrentLevel = a;
        }
    }

    void Start()
    {
       // MyApplycation.Instance.analytics.LogEvent("GamePlay", "PlayGame", "", (int)Time.fixedTime);
       //TestTool.Instance.SetLevel();
        LiveManager.Instance.Live = 10;
    }

    public void ResetGame()
    {
        TextManager.Instance.SetTextType(TypeText.GOLD, "0");
        if (GetStar() >= 1)
        {
            //CurrentLevel += 1;
        }
        _burgerScore = 0;
        Clock.Instance.IsCounting = false;
        Clock.Instance.CountTime = 0;
        Clock.Instance.CountCakeTime = 0;
        CakeManager.Instance.ClearBurger();
        CakeManager.Instance.ClearPlayerBurger();
        //FruitManager.Instance.HideFruit(); TODO: clear drink
        DrinkManager.Instance.ResetOrder();
        ButtonManager.Instance.LoadButton();
        OneStar.fillAmount = 0;
        TwoStar.fillAmount = 0;
        ThreeStar.fillAmount = 0;
        ProgressBar.fillAmount = 0;
        OneGold.SetActive(false);
        TwoGold.SetActive(false);
        ThreeGold.SetActive(false);
        One = false;
        Two = false;
        Three = false;
        life.transform.localPosition = new Vector3(0,0,0);
        life.SetActive(false);
        RandomFruit = false;

    }

    public void StartGame()
    {
        OrganizeStar.Instance.CalStarPosition();
        Clock.Instance.Time = GameData.Instance.listLevel[CurrentLevel].timeGame;
        Clock.Instance.CountTime = GameData.Instance.listLevel[CurrentLevel].timeGame;
        CakeManager.Instance.GenerateBurger();
        int i = Random.Range(1, 100);
        //if (GameData.Instance.listLevel[CurrentLevel].maxUnlockFruit != 0)
        //{
        //    if (i % 3 == 0)
        //    {
        //        RandomFruit = true;
        //    }
        //    else
        //    {
        //        RandomFruit = false;
        //    }
        //}
        //FruitManager.Instance.RandomFruit();
        ButtonManager.Instance.LoadButton();
        Clock.Instance.IsCounting = true;
        life.SetActive(true);
        //GUIArrow.Instance.ChangePosition();
    }

    // The following functions keep track of the number of burgers
    public void IncrementBurgerScore()
    {
        _burgerScore++;
        TextManager.Instance.SetTextType(TypeText.GOLD, _burgerScore.ToString());
    }

    public void IncrementBurgerScore(int n)
    {
        _burgerScore += n;
        TextManager.Instance.SetTextType(TypeText.GOLD, _burgerScore.ToString());
    }

    public int GetBurgerScore()
    {
        return _burgerScore;
    }

    public void ResetBurgerScore()
    {
        _burgerScore = 0;
    }

    public void ShowResults()
    {
        Application.LoadLevel("New UI");
    }

    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (GameController._instance == null)
            {
                var go = GameObject.Find("GameManager");
                if (go == null)
                {
                    go = new GameObject("GameManager");
                    GameController._instance = go.AddComponent<GameController>();
                }
                else {
                    GameController._instance = go.GetComponent<GameController>();
                }
            }
            return GameController._instance;
        }
    }

    public int GetStar()
    {
        if (_burgerScore >= GameData.Instance.listLevel[GameController.Instance.CurrentLevel].twoStar &&
            _burgerScore < GameData.Instance.listLevel[GameController.Instance.CurrentLevel].threeStar)
        {
            Debug.Log("Star: **");
            return 2;
        }
        if (_burgerScore >= GameData.Instance.listLevel[GameController.Instance.CurrentLevel].threeStar)
        {
            Debug.Log("Star: ***");
            return 3;
        }
        if (_burgerScore < GameData.Instance.listLevel[GameController.Instance.CurrentLevel].oneStar)
        {
            Debug.Log("star: 0");
            return 0;
        }
        //Debug.Log("Star: *");
        return 1;
    }

    public void CalculateScore()
    {
        if (!RandomFruit)
        {
            int temp = ScoreList[PieceList.IndexOf(PrePieceNum + 2)];
            int bonus = ((int)Clock.Instance.CountCakeTime - TimeList[PieceList.IndexOf(PrePieceNum + 2)]);

            if (bonus > 0)
            {
                score = temp - bonus;
            }
            else
            {
                score = temp;
            }

            if (score < MinScore[PieceList.IndexOf(PrePieceNum + 2)])
            {
                score = MinScore[PieceList.IndexOf(PrePieceNum + 2)];
            }
        }
        else
        {
            int a = DrinkManager.Instance.NumberDrink;
            int maxScore = ScoreList[PieceList.IndexOf(PrePieceNum + 2)] + DrinkScore[DrinkNumber.IndexOf(a)];
            int minScore = MinScore[PieceList.IndexOf(PrePieceNum + 2)] + DrinkMin[DrinkNumber.IndexOf(a)];
            int time = TimeList[PieceList.IndexOf(PrePieceNum + 2)] + DrinkTime[DrinkNumber.IndexOf(a)];
            if ((int) Clock.Instance.CountCakeTime < time)
            {
                score = maxScore;
            }
            else
            {
                int penalty = (int) Clock.Instance.CountCakeTime - time;
                score = maxScore - penalty > minScore ? maxScore - penalty : minScore;
            }
        }

        //int score2 = 0;
        //if (RandomFruit == true)
        //{
        //    int a = DrinkManager.Instance.NumberDrink;
        //    Debug.Log("a: " +a);
        //    int temp2 = DrinkScore[DrinkNumber.IndexOf(a)];
        //    int bonus2 = ((int) Clock.Instance.CountCakeTime - DrinkTime[a]);

        //    if (bonus2 > 0)
        //    {
        //        score2 = temp2 - bonus2;
        //    }
        //    else
        //    {
        //        score2 = temp2;
        //    }

        //    if (score2 < DrinkMin[DrinkNumber.IndexOf(a)])
        //    {
        //        score2 = DrinkMin[DrinkNumber.IndexOf(a)];
        //    }
        //}
        //Debug.Log("Score2: "+ score2);
        //score += score2;
        GameObject obj = PoolObject.Instance.SpawnObject("Number", number, new Vector3(-3, 1, 0));
        EffectShowScore showScore = obj.GetComponent<EffectShowScore>();
        showScore.Init(score);
        IncrementBurgerScore(score);
        score = 0;
        Clock.Instance.Reset();
        UpdateStar();
    }

    public void UpdateStar()
    {
        int oneStar = GameData.Instance.listLevel[CurrentLevel].oneStar;
        int twoStar = GameData.Instance.listLevel[CurrentLevel].twoStar;
        int threeStar = GameData.Instance.listLevel[CurrentLevel].threeStar;
        if (_burgerScore >= threeStar)
        {
            OneGold.SetActive(true);
            TwoGold.SetActive(true);
            ThreeGold.SetActive(true);
            if (Three == false)
            {
                Three = true;
            }
        }
        else if (_burgerScore >= twoStar)
        {
            OneGold.SetActive(true);
            OneStar.fillAmount = 1;
            TwoGold.SetActive(true);
            if (Two == false)
            {
                Two = true;
            }
        }
        else if (_burgerScore >= oneStar)
        {
            OneGold.SetActive(true);
            if (One == false)
            {
                One = true;
            }
        }
        ProgressBar.fillAmount = _burgerScore/(float) threeStar;
    }

    public void NextLV()
    {
        _burgerScore = 300;
        Clock.Instance.CountTime = 0.25f;
    }
}
