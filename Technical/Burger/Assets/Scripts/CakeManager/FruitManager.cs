using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Fruit
{
    public Sprite fruit;
    public int id;
}

public class FruitManager : MonoSingleton<FruitManager>
{
    public List<Fruit> Fruits;
    public List<Fruit> RandomFruits;
    public List<GameObject> Position; 
    public int correct = 5;
    GameController controller;
    void Start()
    {
        controller = GameController.Instance;
    }

    public void ShowFruit()
    {
        for (int i = 0; i < 5; i++)
        {
            //Debug.Log(i );
            GameObject temp = Position[i];
            temp.GetComponent<SpriteRenderer>().sprite = RandomFruits[i].fruit;
            temp.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }
    public void HideFruit()
    {
        for (int i = 0; i < 5; i++)
        {
            //Debug.Log(i);
            GameObject temp = Position[i];
            temp.GetComponent<SpriteRenderer>().sprite =null;
            temp.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    [ContextMenu("test")]
    public void RandomFruit()
    {
        if (GameData.Instance.listLevel[controller.CurrentLevel].maxUnlockFruit != 0 && controller.RandomFruit == true)
        {
            correct = 0;
            RandomFruits.Clear();
            int a = Random.Range(0, 2);
            RandomFruits.Add(Fruits[a]);
            for (int i = 1; i < 5; i++)
            {
                a = Random.Range(2, GameData.Instance.listLevel[GameController.Instance.CurrentLevel].maxUnlockFruit);
                while (RandomFruits.Contains(Fruits[a]))
                {
                    a = Random.Range(2, GameData.Instance.listLevel[GameController.Instance.CurrentLevel].maxUnlockFruit);
                }
                RandomFruits.Add(Fruits[a]);
            }
            ShowFruit();
        }
    }

    public void WrongFruit()
    {
        for (int i = 0; i < 5; i++)
        {
            SpriteRenderer sr =Position[i].GetComponent<SpriteRenderer>();
            sr.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        correct = 0;
    }

    public void ClearFruit()
    {
        correct = 5;
        RandomFruits.Clear();
    }
}
