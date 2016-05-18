using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Drink
{
    public Sprite sprite;
    public int ID;
    public bool check = false;
}


public class DrinkManager : MonoSingleton<DrinkManager>
{
    public List<GameObject> ListPosOrder;
    public List<GameObject> PlayerDrink;
    public List<Drink> ListOrder;
    public List<Drink> ListDrinks;
    public GameController controller;

    public GameObject DrinkParents;
    public GameObject DrinkPrefabs;
    public int NumberDrink;

    public bool Clear;
    public int Corrected;

    // Use this for initialization
    void Start()
    {
        ResetOrder();
        controller = GameController.Instance;
        //RandomDrink();
    }

    public void ResetOrder()
    {
        foreach (var drink in ListPosOrder)
        {
            drink.GetComponent<SpriteRenderer>().sprite = null;
        }
        foreach (var listDrink in ListDrinks)
        {
            listDrink.check = false;
        }
        ListOrder.Clear();
        Clear = true;
        Corrected = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    [ContextMenu("Drink")]
    public void RandomDrink()
    {
        if (GameData.Instance.listLevel[controller.CurrentLevel].maxUnlockFruit != 0 && controller.RandomFruit == true)
        {
            DrinkParents.SetActive(true);
            Clear = false;
            ListOrder.Clear(); // Clear List Order trc khi tao moi
            NumberDrink = Random.Range(1, GameData.Instance.listLevel[controller.CurrentLevel].maxPieceFruit + 1); //
            for (int i = 0; i < NumberDrink; i++)
            {
                int randomDrink;
                do
                {
                    randomDrink = Random.Range(0, GameData.Instance.listLevel[controller.CurrentLevel].maxUnlockFruit);
                } while (ListOrder.Contains(ListDrinks[randomDrink]));
                ListOrder.Add(ListDrinks[randomDrink]);
            }
        }
        else
        {
            DrinkParents.SetActive(false);
        }
        ShowDrinkOrder();
    }

    public void ShowDrinkOrder()
    {
        foreach (var o in ListPosOrder)
        {
            o.GetComponent<SpriteRenderer>().sprite = null;
        }
        PoolObject.Instance.DespawnAllDrink();
        for (int i = 0; i < ListOrder.Count; i++)
        {
            //ListPosOrder[i].GetComponent<SpriteRenderer>().sprite = ListOrder[i].sprite;
            PoolObject.Instance.SpawnCake("Drink", DrinkPrefabs, DrinkParents, ListOrder[i].sprite);
        }
        Debug.Log("SHow order");
    }

    public void HideDrinkPlayer()
    {
        foreach (var o in PlayerDrink)
        {
            o.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    public void WrongDrink()
    {
        Corrected = 0;
        Clear = false;
        for (int index = 0; index < ListOrder.Count; index++)
        {
            var drink = ListOrder[index];
            drink.check = false;
            var drink2 = ListDrinks[index];
            drink2.check = false;
        }

        HideDrinkPlayer();
        ShowDrinkOrder();
    }

    public void CheckCorrect()
    {
        if (Corrected == ListOrder.Count)
        {
            ResetOrder();
        }
    }
}
