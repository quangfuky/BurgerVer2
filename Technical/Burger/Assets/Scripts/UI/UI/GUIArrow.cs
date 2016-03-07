using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GUIArrow : MonoSingleton<GUIArrow>
{
    public GameObject parents;
    //public Vector3 pos;

    void Start()
    {
        //pos = new Vector3(-80,-60);
    }
    void Update()
    {
       ChangePosition();
    }
    [ContextMenu("test")]
    public void ChangePosition()
    {
        int index;
        if (CakeManager.Instance.PlayerBurger.Count < CakeManager.Instance.Burger.Count)
        {
            index = CakeManager.Instance.PlayerBurger.Count;
        }
        else
        {
            index = CakeManager.Instance.PlayerBurger.Count - 1;
        }
        gameObject.transform.parent = parents.transform;
        gameObject.transform.localPosition = new Vector3(-100, index * 40f + -95f, 0f);
        //pos = new Vector3(-80, gameObject.transform.localPosition.y, 0f);
    }
}
