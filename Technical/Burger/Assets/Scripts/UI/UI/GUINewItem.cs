using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUINewItem : MonoSingleton<GUINewItem>
{

    public Image imageItem;
    public Text txtItem;
    public List<string> listNameCake;
    public List<string> listNameFruit;

    public List<Sprite> listItemCake;
    public List<Sprite> listItemFruit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [ContextMenu("Test")]
    void test()
    {
        SetImageNewItem(5, true);
    }
    public void SetImageNewItem(int id, bool isCake)
    {
        if (isCake)
        {
            imageItem.sprite = listItemCake[id - 1];
            txtItem.text = listNameCake[id - 1];
        }
        else
        {
            imageItem.sprite = listItemFruit[id - 1];
            txtItem.text = listNameFruit[id - 1];
        }
    }
}
