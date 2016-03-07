using UnityEngine;
using System.Collections;
using System.ComponentModel;
using Holoville.HOTween;

public class GUIRotate : MonoBehaviour
{
    private float timeDelay;
    void Start()
    {
        timeDelay = Random.Range(1, 3f);
        ShakeRight();
    }

    void ShakeRight()
    {
        HOTween.To(gameObject.transform, 0.2f, new TweenParms()
            .Prop("rotation", new Vector3(0, 0, 20), false)
            .Loops(2, LoopType.Yoyo)
            .OnComplete(()=>ShakeLeft())
            );
    }

    void ShakeLeft()
    {
        HOTween.To(gameObject.transform, 0.2f, new TweenParms()
           .Prop("rotation", new Vector3(0, 0, -20), false)
           .Loops(2, LoopType.Yoyo)
           .Delay(timeDelay)
           .OnComplete(() => ShakeRight())
           );
    }
}
