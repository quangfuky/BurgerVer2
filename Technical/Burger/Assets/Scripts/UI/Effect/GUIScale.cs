using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GUIScale : MonoBehaviour {

    public InfoDotween info;
    private float timeDelay = 0;
    public float delayRange;
    public float duration;
    public int loops = -1;
	// Use this for initialization
	void Start () {
        timeDelay = Random.Range(0, delayRange);
        //Scale();
	}

    void OnEnable()
    {
        Scale();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Scale()
    {
        HOTween.To(gameObject.transform, duration, new TweenParms()
            .Prop("localScale", info.scaleOut, false)
            .Ease(EaseType.EaseInOutCubic)
            .Loops(loops, LoopType.Yoyo)
            .Delay(timeDelay)          
            );
    }
}
