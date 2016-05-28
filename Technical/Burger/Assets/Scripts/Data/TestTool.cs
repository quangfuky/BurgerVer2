using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoTest
{
    public int level;
    public float speed;
    public float miss;
    public int testTimes;
    public int score;
}

public class TestTool : MonoSingleton<TestTool>
{
    public int testlevel;
    public LevelInfo level;
    public int times;
    public int tested;
    public float missChance;
    public float chanceRandomDrink;
    public float speed;
    public float totalTime;
    public int score;
    public float cakeTime;
    public float drinkTime;
    private int cake;
    private int drink;
    public List<string> Output = new List<string>();

    public GameController scoreTest;
    private bool randomDrink;
    public string customPath;

    private int ID;
    public float roundDelay;

    public Text LEVEL;
    public Text TIMES;
    public Text SPEED;
    public Text MISS;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        randomDrink = false;
        tested = 0;
    }

    void SetInfo()
    {
        testlevel = int.Parse(LEVEL.text);
        speed = float.Parse(SPEED.text);
        missChance = float.Parse(MISS.text);
        times = int.Parse(TIMES.text);
    }

    public void ButtonStart()
    {
        SetInfo();
        Output = new List<string>();
        Output.Add("luot" + "-" + "speed" + "-" + "missChance" + "-" + "chanceRandomDrink" + "-" + "score" + " - Star");
        Debug.Log("Start Test");
        LiveManager.Instance.Live = 10;
        SceneManager.LoadScene("Game Play");
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void GetLevel()
    {
        level = GameData.Instance.listLevel[testlevel];
    }
    [ContextMenu("StartTest")]
    public void BeginTest()
    {
        GetLevel();
        Output.Add("luot" + "-" + "speed" + "-" + "missChance" + "-" + "chanceRandomDrink" + "-" + "score");
        for (int i = 0; i < times; i++)
        {
            // Reset time, reset score
            totalTime = 0;
            score = 0;

            while (totalTime < level.timeGame)
            {
                randomDrink = false;
                cakeTime = 0;
                drinkTime = 0;
                cake = Random.Range(3, level.maxPieceCake + 1);
                drink = Random.Range(3, level.maxPieceFruit + 1);
                for (int j = 0; j < cake; j++)
                {
                    int miss = Random.Range(0, 100);
                    if (miss < missChance)
                    {
                        j = 0;
                        continue;
                    }
                    drinkTime += speed;
                    cakeTime += speed;
                    totalTime += speed;
                }

                if (Random.Range(0, 100) < chanceRandomDrink)
                {
                    randomDrink = true;
                    for (int j = 0; j < drink; j++)
                    {
                        int miss = Random.Range(0, 100);
                        if (miss < missChance)
                        {
                            j = 0;
                            continue;
                        }
                        drinkTime += speed;
                        totalTime += speed;
                    }
                }

                score += CaculateScore();
                totalTime += 0.3f;
                //score += scoreTest.CalculateScore(cakeTime, drinkTime);
            }
            Debug.Log("Score: " + score);
            string output = i + " - " + speed + " - " + missChance + " - " + chanceRandomDrink + " - " + score + GameController.Instance.GetStar();
            Output.Add(output);
        }

        SaveOutput(Output);
    }

    public int CaculateScore()
    {
        int temp = scoreTest.ScoreList[scoreTest.PieceList.IndexOf(cake)];
        int bonus = (int)cakeTime - scoreTest.TimeList[scoreTest.PieceList.IndexOf(cake)];
        if (bonus > 0)
        {
            score = temp - bonus;
        }
        else
        {
            score = temp;
        }
        if (score < scoreTest.MinScore[scoreTest.PieceList.IndexOf(cake)])
        {
            score = scoreTest.MinScore[scoreTest.PieceList.IndexOf(cake)];
        }
        int score2 = 0;

        if (randomDrink == true)
        {
            int temp2 = scoreTest.DrinkScore[scoreTest.DrinkNumber.IndexOf(drink)];
            int bonus2 = ((int)drinkTime - scoreTest.DrinkTime[drink]);

            if (bonus2 > 0)
            {
                score2 = temp2 - bonus2;
            }
            else
            {
                score2 = temp2;
            }

            if (score2 < scoreTest.DrinkMin[scoreTest.DrinkNumber.IndexOf(drink)])
            {
                score2 = scoreTest.DrinkMin[scoreTest.DrinkNumber.IndexOf(drink)];
            }
        }
        score += score2;
        return score;
    }

    public void SaveOutput(List<string> output)
    {
        string path1 = Application.dataPath;
        string path = Path.Combine(path1,testlevel.ToString() + ".txt");
        Debug.Log(path);
        File.WriteAllLines(path, output.ToArray(), System.Text.Encoding.UTF8);
    }

    //Click button cake

    public void ChooseCake()
    {
        if (!GameController.Instance.Randomizing && CakeManager.Instance.Burger.Count != CakeManager.Instance.PlayerBurger.Count)
        {
            CakeManager cakeManager = CakeManager.Instance;
            GameController gameControl = GameController.Instance;
            cakeManager.PlayerBurger.Add(ID);

            if ((cakeManager.PlayerBurger.Count - 1) >= cakeManager.Burger.Count)
            {
                //StartCoroutine(gameControl.Clock.AddTime(gameControl.FailureTimePenalty));
                //gameControl.Streak = 0;
                StartCoroutine(CakeManager.Instance.NewBurger(false));
                //Debug.Log("xong");
            }
            else if (cakeManager.PlayerBurger.Last() != cakeManager.Burger[cakeManager.PlayerBurger.Count - 1])
            {
                //StartCoroutine(gameControl.Clock.AddTime(gameControl.FailureTimePenalty));
                cakeManager.ClearPlayerBurger();
                AudioController.Instance.PlayAudioTick(TypeAudio.FAIL);
                //gameControl.Streak = 0;
                return;
                //StartCoroutine(CakeManager.Instance.NewBurger(false));
            }

            var playerLen = cakeManager.PlayerBurger.Count;

            var playerBurger = cakeManager.PlayerBurgerObj;
            var texture = cakeManager.GetTexture(ID);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            GameObject go = new GameObject(ID.ToString());
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            //go.transform.localScale = new Vector3(100,100);
            sr.sprite = sprite;

            //this.clickSound.Play();
            AudioController.Instance.PlayAudioTick(TypeAudio.TICK);

            go.transform.parent = playerBurger.transform;
            go.transform.localPosition = new Vector3(0f, playerLen * 35f, 0f);
            //GUIEffect.Instance.AddItem(go, new Vector3(1f, 1f, 1f), 0.2f);
            GUIEffect.Instance.AddItem();
            sr.sortingOrder = cakeManager.PlayerBurgerLayer++;
            GUIArrow.Instance.ChangePosition();
        }
    }


    //Click Button Drink

    public void ChooseDrink()
    {
        if (DrinkManager.Instance.Clear == false)
        {
            for (int index = 0; index < DrinkManager.Instance.ListOrder.Count; index++)
            {
                var drink = DrinkManager.Instance.ListOrder[index];
                if (ID == drink.ID && drink.check == false)
                {
                    DrinkManager.Instance.PlayerDrink[DrinkManager.Instance.Corrected].GetComponent<SpriteRenderer>()
                        .sprite = drink.sprite;
                    DrinkManager.Instance.ListOrder[index].check = true;
                    DrinkManager.Instance.ListPosOrder[index].GetComponent<SpriteRenderer>().sprite = null;
                    DrinkManager.Instance.Corrected++;
                    DrinkManager.Instance.CheckCorrect();
                    AudioController.Instance.PlayAudioTick(TypeAudio.TICK);
                    Debug.Log("Correct");
                    GUIEffect.Instance.AddDrink();
                    return;
                }
            }
            AudioController.Instance.PlayAudioTick(TypeAudio.FAIL);
            Debug.Log("False");
            DrinkManager.Instance.WrongDrink();
        }
    }


    public IEnumerator PlayRound()
    {
        float miss = missChance;
        List<int> burger = CakeManager.Instance.Burger;
        List<Drink> drink = DrinkManager.Instance.ListOrder;
        //if (roundDelay > 0)
        //{
        //    roundDelay -= Time.deltaTime;
        //    return;
        //}
        for (int i = 0; i < burger.Count; i++)
        {
            yield return new WaitForSeconds(speed);
            if (Random.Range(0, 100) < miss)
            {
                ID = i > 0 ? burger[i - 1] : burger[i + 1];
                i = -1;
                Debug.LogError("Miss");
                miss = miss/2;
            }
            else
            {
                ID = burger[i];
            }
            ChooseCake();
            Debug.Log("CLick");
        }
        miss = missChance;
        for (int i = 0; i < drink.Count; i++)
        {
            yield return new WaitForSeconds(speed);
            if (Random.Range(0, 100) < miss)
            {
                ID = 208;
                i = -1;
                miss = miss/2;
            }
            else
            {
                ID = drink[i].ID;
            }
            ChooseDrink();
            Debug.Log("CLick 2");
        }
    }
    [ContextMenu("Test Cour")]
    public void TestEnum()
    {
        StartCoroutine(PlayRound());
    }


    public void SetLevel()
    {
        GameController.Instance.CurrentLevel = testlevel;
    }

    public void Replay()
    {
        tested ++;
        string output = tested + " - " + speed + " - " + missChance + " - " + chanceRandomDrink + " - " + GameController.Instance._burgerScore;
        Output.Add(output);
        Debug.LogError("Replay");
        LiveManager.Instance.Live = 10;
        if (tested < times)
        {
            GUIEffect.Instance.Replay();
        }
        else
        {   
            SaveOutput(Output);
            Debug.LogError("Save");
        }
    }
}
