using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        Application.LoadLevel(gameObject.transform.name);
    }
}
