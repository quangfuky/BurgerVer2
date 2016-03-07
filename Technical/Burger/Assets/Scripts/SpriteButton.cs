using UnityEngine;
using System.Collections;
using MadLevelManager;

public class SpriteButton : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	    var sprite = GetComponent<MadSprite>();
	    sprite.onTap += (s) =>
        {
            Debug.Log("clicked");
            UnityAds.Instance.ShowRewardAds();
        };
        sprite.onMouseDown += (s) =>
	    {
	        Debug.Log("clicked");
            UnityAds.Instance.ShowRewardAds();
	    };
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
