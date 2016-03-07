using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Coin : MonoBehaviour {

    public InfoDotween info; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void MoveTo()
    {
        HOTween.To(gameObject.transform, 1.0f, new TweenParms()
            .Prop("localPosition", info.posActive, false)
            .Ease(EaseType.EaseInOutCubic)
            .OnComplete(() => Finish())
            );
    }
    public void Finish()
    {

    }
}
