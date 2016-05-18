using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Xsl;
using Holoville.HOTween;
using UnityEngine.UI;

public class GUIArrow : MonoSingleton<GUIArrow>
{
    public GameObject parents;
    public List<Image> list;
    //public Vector3 pos;

    void Start()
    {
        //pos = new Vector3(-80,-60);
        ChangePosition();
    }
    void Update()
    {
        ChangePosition();
    }


    //public void ChangePosition()
    //{
    //    int index;
    //    if (CakeManager.Instance.PlayerBurger.Count < CakeManager.Instance.Burger.Count)
    //    {
    //        index = CakeManager.Instance.PlayerBurger.Count;
    //    }
    //    else
    //    {
    //        index = CakeManager.Instance.PlayerBurger.Count - 1;
    //    }
    //    gameObject.transform.parent = parents.transform;
    //    gameObject.transform.localPosition = new Vector3(-100, index * 40f + -40f, 0f);
    //    //pos = new Vector3(-80, gameObject.transform.localPosition.y, 0f);
    //}
    [ContextMenu("test")]
    public void ChangePosition()
    {
        var cake = GameObject.FindWithTag("Cake").GetComponentsInChildren<Image>();
        list = cake.ToList();
        list.Reverse();
        if (list.Count != 0)    
        {
            if (CakeManager.Instance.PlayerBurger.Count < list.Count)
            {
                gameObject.transform.position = new Vector3(list[CakeManager.Instance.PlayerBurger.Count].sprite.bounds.size.x + 0.7f, list[CakeManager.Instance.PlayerBurger.Count].transform.position.y, 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(list[CakeManager.Instance.PlayerBurger.Count-1].sprite.bounds.size.x + 0.7f, list[CakeManager.Instance.PlayerBurger.Count-1].transform.position.y, 0);
            }
        }
    }
}
