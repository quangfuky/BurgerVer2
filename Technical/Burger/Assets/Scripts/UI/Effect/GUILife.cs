using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GUILife : MonoBehaviour
{
    public Vector3 posIn;
    public Vector3 posOut;
	// Use this for initialization
    void OnEnable()
    {
        MoveIn();
    }

    public void MoveIn()
    {
        HOTween.To(gameObject.transform, 2.0f, new TweenParms()
            .Prop("localPosition", posOut, false)
            .Ease(EaseType.EaseInOutCubic)
            .Delay(0.5f)
            .OnComplete(()=>Reset())
            );
    }

    public void Reset()
    {
        gameObject.transform.localPosition = posIn;
        gameObject.SetActive(false);
    }
}
