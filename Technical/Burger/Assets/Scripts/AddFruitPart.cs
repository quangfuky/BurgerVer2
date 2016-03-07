using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AddFruitPart : MonoBehaviour, IPointerDownHandler
{
    public int ID;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameController.Instance.Randomizing && FruitManager.Instance.correct != 5)
        {
            foreach (var randomFruit in FruitManager.Instance.RandomFruits)
            {
                if (ID == randomFruit.id)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        string name = "Fruit"+ i.ToString();
                        SpriteRenderer sr = GameObject.Find(name).GetComponent<SpriteRenderer>();
                        if (sr.sprite == randomFruit.fruit && sr.color == new Color(0.5f, 0.5f, 0.5f, 1))
                        {
                            sr.color = new Color(1, 1, 1, 1);
                            FruitManager.Instance.correct++;
                            AudioController.Instance.PlayAudioTick(TypeAudio.TICK);
                            Debug.Log("correct");
                            return;
                        }
                    }
                }
            }
            AudioController.Instance.PlayAudioTick(TypeAudio.FAIL);
            FruitManager.Instance.WrongFruit();
        }
        
    }
}
