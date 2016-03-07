using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using UnityEngine.UI;

public class GUIChangleScreen : MonoBehaviour
{

    public Vector3 posX;
    [ContextMenu("Test Change Pos X")]
    public void MoveIn()
    {
        HOTween.To(gameObject.transform, 1f, new TweenParms()
            .Prop("positionx", posX, false)
            );
    }
}
