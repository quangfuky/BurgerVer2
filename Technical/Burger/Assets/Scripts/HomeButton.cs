using UnityEngine;
using System.Collections;
using MadLevelManager;

public class HomeButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var sprite = GetComponent<MadSprite>();
        sprite.onTap += (s) =>
        {
            Debug.Log("clicked");
            Application.LoadLevel("Main Menu");
        };
        sprite.onMouseDown += (s) =>
        {
            Debug.Log("clicked");
            Application.LoadLevel("Main Menu");
        };
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
