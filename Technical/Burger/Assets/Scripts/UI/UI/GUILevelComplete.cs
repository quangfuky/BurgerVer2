using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUILevelComplete : MonoSingleton<GUILevelComplete> {

    public Image imageName;
    public Text txt;
    public List<Sprite> listImageName;
    public GameObject objBtHome;
    public GameObject objBtRepllayLose;

    public GameObject objBtNext;
    public GameObject objBtReplay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetGameComplete(bool isWin)
    {
        if(isWin)
        {
            objBtNext.SetActive(true);
            objBtReplay.SetActive(true);

            objBtHome.SetActive(false);
            objBtRepllayLose.SetActive(false);

            imageName.sprite = listImageName[0];
            txt.text = "Chế làm bánh giỏi lắm nha, chịu";
        }
        else
        {
            objBtNext.SetActive(false);
            objBtReplay.SetActive(false);

            objBtHome.SetActive(true);
            objBtRepllayLose.SetActive(true);

            imageName.sprite = listImageName[1];
            txt.text = "Nhanh tay thêm chút nữa nha";
        }
    }
}
