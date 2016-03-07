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
            foreach (var drink in DrinkManager.Instance.ListOrder)
            {
                if (BtnId == drink.ID)
                {
                    DrinkManager.Instance.PlayerDrink[DrinkManager.Instance.Corrected].GetComponent<SpriteRenderer>()
                        .sprite = drink.sprite;
                    DrinkManager.Instance.Corrected++;
                    DrinkManager.Instance.CheckCorrect();
                    AudioController.Instance.PlayAudioTick(TypeAudio.TICK);
                    Debug.Log("Correct");
                    return;
                }
            }
            AudioController.Instance.PlayAudioTick(TypeAudio.FAIL);
            Debug.Log("False");
            DrinkManager.Instance.WrongDrink();
        }
    }
}
