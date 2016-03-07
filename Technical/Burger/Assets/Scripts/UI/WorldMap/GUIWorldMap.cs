using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum TypeStar
{
    NONE = 0,
    ONE_STAR = 1,
    TWO_STAR = 2,
    THREE_STAR = 3
}
public class StarInfo
{
    public TypeStar type;
    public Sprite sprite;
}
public class GUIWorldMap : MonoSingleton<GUIWorldMap>
{

    public List<StarInfo> listStarInfo;
    public GUINodeWorldMap nodeCurrent;
    public List<GUINodeWorldMap> listNode;
    public List<int> listScoreLevel = new List<int>();
    private string listScore = "";
    void Awake()
    {
        GetScore();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [ContextMenu("SetUnlook")]
    public void SetUnlook(int score)
    {
        
        listScoreLevel.Add(score);
        listScore += score.ToString() + ";";

        PlayerPrefs.SetString("Score", listScore);
        PlayerPrefs.Save();

    }
    [ContextMenu("Test")]
    void Test()
    {
        //PlayerPrefs.DeleteAll();
        string str = PlayerPrefs.GetString("Score");
        Debug.Log("score : " + str);
    }
    [ContextMenu("Reset")]
    void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
    void GetScore()
    {
        listScoreLevel.Clear();
        string str = PlayerPrefs.GetString("Score");
        string[] strScore = str.Split(new char[]{';'});
        if (strScore.Length > 0)
        {
            for (int i = 0; i < strScore.Length; i++)
            {
                if (strScore[i] != "")
                {
                    int _score = int.Parse(strScore[i]);
                    listScoreLevel.Add(_score);
                }
                
            }
        }
    }
    public GUINodeWorldMap GetNodeCurrent()
    {
        for(int i = 0; i<listNode.Count ;i++)
        {
            int content = int.Parse(listNode[i].listbox.content.text.ToString());
            int contentCur = int.Parse(nodeCurrent.listbox.content.text.ToString());
            if(content == contentCur + 1)
            {
                return listNode[i];
            }
        }
        return null;
    }
    public int GetIdLevel(ListBox listBox)
    {
        int level = int.Parse(listBox.content.text);
        return level;
    }

    public void SetNodeCurrent(GUINodeWorldMap node)
    {
        nodeCurrent = node;
    }
    public void SetImageStar(int star)
    {
        StarInfo info = GetInfoStar(star);
        nodeCurrent.imageStar.sprite = info.sprite;
       
    }
    public void SetTextGoldTaget(string txtGoldTaget)
    {
        nodeCurrent.txtGoldTaget.text = txtGoldTaget;
    }
    StarInfo GetInfoStar(int star)
    {
        for(int i = 0; i< listStarInfo.Count; i++)
        {
            if(star == (int)listStarInfo[i].type)
            {
                return listStarInfo[i];
            }
        }
        return null;
    }


}
