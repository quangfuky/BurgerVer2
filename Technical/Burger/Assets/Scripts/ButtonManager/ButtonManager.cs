using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoSingleton<ButtonManager>
{
    public List<GameObject> CakeButtons;
    public List<GameObject> FruitButtons;
    void Start()
    {
       
    }

    public void ShowCakeButton()
    {
        int index = GameData.Instance.listLevel[GameController.Instance.CurrentLevel].maxUnlockCake;

        for (int i = 0; i < index; i++)
        {
            CakeButtons[i].SetActive(true);
        }
        for (int i = index; i < CakeButtons.Count; i++)
        {
            CakeButtons[i].SetActive(false);
        }
    }

    public void ShowFruitButton()
    {
        int index = GameData.Instance.listLevel[GameController.Instance.CurrentLevel].maxUnlockFruit;

        for (int i = 0; i < index; i++)
        {
            FruitButtons[i].SetActive(true);
        }
        for (int i = index; i < FruitButtons.Count; i++)
        {
            FruitButtons[i].SetActive(false);
        }
    }

    public void LoadButton()
    {
        ShowFruitButton();
        ShowCakeButton();
    }
}
