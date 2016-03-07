using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Drink
{
    public Sprite sprite;
    public int ID;
}


public class DrinkManager : MonoSingleton<DrinkManager>
{
    public List<GameObject> ListPosOrder;
    public List<GameObject> PlayerDrink;
    public List<Drink> ListOrder; 
    public List<Drink> ListDrinks;
    public GameController controller;

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
            Clear = false;
            ListOrder.Clear(); // Clear List Order trc khi tao moi
            int a = Random.Range(1, GameData.Instance.listLevel[controller.CurrentLevel].maxPieceFruit -1); //
            for (int i = 0; i < a; i++)
            {
                int randomDrink = Random.Range(1, ListDrinks.Count);
                ListOrder.Add(ListDrinks[randomDrink]);
            }
        }
        ShowDrink();
    }

    public void ShowDrink()
    {
        for (int i = 0; i < ListOrder.Count; i++)
        {
            ListPosOrder[i].GetComponent<SpriteRenderer>().sprite = ListOrder[i].sprite;
        }
    }

    public void HideDrink()
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
        PlayerDrink.Clear();
    }

    public void CheckCorrect()
    {
        if (Corrected == ListOrder.Count)
        {
            ResetOrder();
        }
    }
}
