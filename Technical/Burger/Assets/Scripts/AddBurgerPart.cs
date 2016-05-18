using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class AddBurgerPart : MonoBehaviour, IPointerDownHandler
{
    public AudioSource clickSound;
    public int ID;

    public void OnPointerDown(PointerEventData eventData)
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
}
