using UnityEngine;
using System.Collections;
using MadLevelManager;

public class MadAlpha : MonoBehaviour
{
    public GameObject Draggable;
	// Use this for initialization
	void Start ()
	{
	    Draggable.GetComponent<MadDragStopDraggable>().enabled = false;
        var sprite = GetComponent<MadSprite>();
        sprite.onTap += (s) =>
        {
            Debug.Log("clicked");
            Draggable.GetComponent<MadDragStopDraggable>().enabled = true;
            gameObject.SetActive(false);
        };
        sprite.onMouseDown += (s) =>
        {
            Debug.Log("clicked");
            Draggable.GetComponent<MadDragStopDraggable>().enabled = true;
            gameObject.SetActive(false);
        };
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
