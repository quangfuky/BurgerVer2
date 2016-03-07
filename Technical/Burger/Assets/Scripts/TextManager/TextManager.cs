using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public enum TypeText
{
    NONE = 0,
    SCORE_PLAY = 1,
    STREAK = 2,
    TIME_GAME = 3,
    SCORE_COMPLETE = 4,
    HEALTH = 5,
    GOLD = 6,


}
[System.Serializable]

public class TextInfo
{
    public Text textObj;
    public TypeText type;
}

public class TextManager : MonoBehaviour
{
    public Text Score;
    public Text Streak;
    public Text Time;

    public List<TextInfo> listText;

    // Use this for initialization
    public void SetScore(string score)
    {
        if (Score != null)
            Score.text = score;
    }


    public void SetTextType(TypeText type, string text)
    {
        
        TextInfo textinfo = GetTextType(type);

        if(textinfo != null)
        {
            textinfo.textObj.text = text;
        }
        else
        {
            Debug.Log("K get dc Text");
        }
    }
    public TextInfo GetTextType(TypeText type)
    {
        for(int i = 0; i<listText.Count; i++)
        {
            if(listText[i].type == type)
            {
                return listText[i];
            }
        }
        return null;
    }
    private static TextManager _instance;

    public static TextManager Instance
    {
        get
        {
            if (TextManager._instance == null)
            {
                var go = GameObject.Find("TextManager");
                if (go == null)
                {
                    go = new GameObject("TextManager");
                    TextManager._instance = go.AddComponent<TextManager>();
                }
                else {
                    TextManager._instance = go.GetComponent<TextManager>();
                }
            }
            return TextManager._instance;
        }
    }
}
