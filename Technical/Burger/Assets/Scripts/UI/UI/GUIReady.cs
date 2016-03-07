using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using UnityEngine.UI;
public class GUIReady : MonoBehaviour {

	// Use this for initialization
    public InfoDotween info;
    public GUIController guiController;
    public Text txtGoldTarget;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [ContextMenu("Move In")]
    public void MoveIn()
    {
        HOTween.To(gameObject.transform, 1.0f, new TweenParms()
            .Prop("localPosition", info.posActive, false)
            .Ease(EaseType.EaseInOutCubic)
            .Delay(0.5f)
            .OnComplete(()=>Zoom())
            );
    }
    void Zoom()
    {
        HOTween.To(gameObject.transform, 1.0f, new TweenParms()
            .Prop("localScale", info.scaleOut, false)
            .Ease(EaseType.EaseInOutCubic)
            .Loops(2, LoopType.Yoyo)
            .OnComplete(()=>MoveOut())
            );
    }
    public void MoveOut()
    {
        HOTween.To(gameObject.transform, 1.0f, new TweenParms()
            .Prop("localPosition", info.posOut, false)
            .Ease(EaseType.EaseInOutCubic)
            .OnComplete(()=>Reset())
            );
    }
    void Reset()
    {
        gameObject.transform.localPosition = info.posIn;

        guiController.StartReadyGo(false);
        GameController.Instance.StartGame();
        GUIEffect.Instance.Order();
    }
    public void SetGoldTarget(string gold)
    {
        txtGoldTarget.text = gold;
    }
}
