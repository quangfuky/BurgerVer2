using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Quan ly texture va ID
/// </summary>
[System.Serializable]
public class CakePart
{
    public Texture2D Textture;
    public int ID;
}

public class CakeManager : MonoBehaviour
{
    public GameObject BurgerObj;
    public GameObject PlayerBurgerObj;
    public List<CakePart> CakeParts;
    public List<int> CakeIngridients;
    public List<int> Burger;
    public List<int> PlayerBurger;

    private bool _burgerlock = false;
    public int PlayerBurgerLayer;
    // Use this for initialization
    void Start()
    {
        PlayerBurgerLayer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Clock.Instance.IsCounting)
        {
            if (CheckBurgersMatch() && DrinkManager.Instance.Clear == true)
            {
                _burgerlock = true;
                GameController.Instance.CalculateScore();
                TextManager.Instance.SetTextType(TypeText.GOLD ,GameController.Instance.GetBurgerScore().ToString());

                //StartCoroutine(NewBurger(true));
                GUIEffect.Instance.CompleteOrder();
                //FruitManager.Instance.RandomFruit();
            }

            if (Burger.Count > 8)
            {
                ClearPlayerBurger();
            }
        }
    }

    [ContextMenu("Generate Burger")]
    public void GenerateBurger()
    {
        //GUIArrow.Instance.ChangePosition();
        PlayerBurgerLayer = 1;
        int level = GameController.Instance.CurrentLevel;
        List<LevelInfo> listLevel = GameData.Instance.listLevel;
        int numParts = Random.Range(1, GameData.Instance.listLevel[GameController.Instance.CurrentLevel].maxPieceCake - 1);
        Debug.Log(numParts);
        GameController.Instance.PrePieceNum = numParts;
        int nhan = Random.Range(0, 2);
        if (nhan == 0)
            Burger.Add(101);
        else
        {
            Burger.Add(102);
        }

        for (int i = 0; i < numParts; i++)
        {
            int ingredient = CakeIngridients[Random.Range(2, GameData.Instance.listLevel[GameController.Instance.CurrentLevel].maxUnlockCake)];
            while (ingredient == Burger[Burger.Count - 1])
            {
                ingredient = CakeIngridients[Random.Range(2, GameData.Instance.listLevel[GameController.Instance.CurrentLevel].maxUnlockCake)];
            }
            Burger.Add(ingredient);
        }

        if (nhan == 0)
            Burger.Add(101);
        else
        {
            Burger.Add(102);
        } 
        DisplayGeneratedBurger();
    }

    public void DisplayGeneratedBurger()
    {
        int index = 1;

        foreach (int burg_ing in Burger)
        {
            var texture = GetTexture(burg_ing);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            GameObject go = new GameObject(burg_ing.ToString());
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;

            // TODO: make sure layering of toppings looks correct
            go.transform.parent = BurgerObj.transform;
            go.transform.localPosition = new Vector3(0f, index * 0.3f - 0.9f, 0f);
            sr.sortingOrder = index + 4;
            go.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            
            index++;
        }
    }

    public Texture2D GetTexture(int ID)
    {
        foreach (var cakePart in CakeParts)
        {
            if (cakePart.ID == ID)
            {
                return cakePart.Textture;
            }
        }
        return null;
    }
    [ContextMenu("clear")]
    public void ClearBurger()
    {
        Burger.Clear();
        foreach (Transform child in BurgerObj.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ClearPlayerBurger()
    {
        PlayerBurgerLayer = 1;
        PlayerBurger.Clear();
        if (PlayerBurgerObj != null)
        {
            foreach (Transform child in PlayerBurgerObj.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public IEnumerator NewBurger(bool correct)
    {
        yield return new WaitForSeconds(0.5f);
        ClearPlayerBurger();
        ClearBurger();
        GenerateBurger();
        _burgerlock = false;
    }
    public void NewBurger()
    {
        ClearPlayerBurger();
        ClearBurger();
        GenerateBurger();
        if (GameData.Instance.listLevel[GameController.Instance.CurrentLevel].maxUnlockFruit != 0)
        {
            int i = Random.Range(1, 100);
            if (i % 3 == 0)
            {
                GameController.Instance.RandomFruit = true;
            }
            else
            {
                GameController.Instance.RandomFruit = false;
            }
            DrinkManager.Instance.RandomDrink();
        }
        _burgerlock = false;
    }

    public bool CheckBurgersMatch()
    {
        if (_burgerlock == true)
        {
            return false;
        }
        if (PlayerBurger.Count != Burger.Count)
        {
            return false;
        }
        for (int i = 0; i < Burger.Count; i++)
        {
            if (!Burger[i].Equals(PlayerBurger[i]))
            {
                return false;
            }
        }
        return true;
    }

    private static CakeManager _instance;

    public static CakeManager Instance
    {
        get
        {
            if (CakeManager._instance == null)
            {
                var go = GameObject.Find("CakeManager");
                if (go == null)
                {
                    go = new GameObject("CakeManager");
                    CakeManager._instance = go.AddComponent<CakeManager>();
                }
                else {
                    CakeManager._instance = go.GetComponent<CakeManager>();
                }
            }
            return CakeManager._instance;
        }
    }
}
