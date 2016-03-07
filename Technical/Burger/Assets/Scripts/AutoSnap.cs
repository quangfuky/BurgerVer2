using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoSnap : MonoBehaviour
{
    public RectTransform ScrollRect;
    public List<GameObject> ListPage;
    public RectTransform Center;

    private float[] Distance;
    private bool Dragging = false;
    private int pageDistance;
    private int minPageNum;
    private float minDistance;

	// Use this for initialization
	void Awake ()
	{
        //var List = GameObject.FindGameObjectsWithTag("Page");
        //foreach (var a in List)
        //{
        //    ListPage.Add(a);
        //}
        int pageLength = ListPage.Count;
        Distance = new float[pageLength];
	    pageDistance =
	       (int) Mathf.Abs(ListPage[1].GetComponent<RectTransform>().anchoredPosition.x -
	                  ListPage[0].GetComponent<RectTransform>().anchoredPosition.x);
	}
	
	// Update is called once per frame
	void Update () {
	    CheckDistanceToCenter();
	}

    void CheckDistanceToCenter()
    {
        for (int i = 0; i < ListPage.Count; i++)
        {
            Distance[i] = Mathf.Abs(Center.position.x - ListPage[i].transform.position.x);
        }

        minDistance = Mathf.Min(Distance);

        for (int i = 0; i < ListPage.Count; i++)
        {
            if ((int)minDistance == (int) Distance[i])
            {
                minPageNum = i;
            }
        }

        if (!Dragging)
        {
            LerpToPage(minPageNum * -pageDistance);
        }
    }

    void LerpToPage(int Pos)
    {
        float newX = Mathf.Lerp(ScrollRect.anchoredPosition.x, Pos, Time.deltaTime*10f);
        Vector2 newPosition = new Vector2(newX, ScrollRect.anchoredPosition.y);

        ScrollRect.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        Dragging = true;
    }

    public void EndDrag()
    {
        Dragging = false;
    }
}
