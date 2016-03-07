using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GUIMove : MonoBehaviour
{
    void OnEnable()
    {
        MoveIn();
    }
    public void MoveIn()
    {
        HOTween.To(gameObject.transform, 1f, new TweenParms()
            //.NewProp("localPositionX", GUIArrow.Instance.pos, false)
            .Loops(-1, LoopType.Yoyo)
            .Id("MoveArrow")
            );
    }
}
    