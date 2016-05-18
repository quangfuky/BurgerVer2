using UnityEngine;
using System.Collections;
using PathologicalGames;
using UnityEngine.EventSystems;

public class AddDrink : MonoBehaviour, IPointerDownHandler
{
    public int BtnId;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (DrinkManager.Instance.Clear == false)
        {
            for (int index = 0; index < DrinkManager.Instance.ListOrder.Count; index++)
            {
                var drink = DrinkManager.Instance.ListOrder[index];
                if (BtnId == drink.ID && drink.check == false)
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
}
