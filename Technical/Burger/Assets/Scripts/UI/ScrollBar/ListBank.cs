/* Store the contents for ListBoxes to display.
 */
using UnityEngine;
using System.Collections;
using System;

public class ListBank : MonoBehaviour
{
	public static ListBank Instance;
    public int levelPlayer = 3;
    private int[] list = {
                             50, 49, 48, 47, 46, 45, 44, 43, 42, 41, 40, 39, 38, 37, 36, 35, 34, 33, 32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1
                         };
    private int[] contents = new int[50];
    //{
    //    2, 1 , 10, 9, 8, 7, 6, 5, 4, 3
    //};

	void Awake()
	{
		Instance = this;
        int lv = GUIWorldMap.Instance.listScoreLevel.Count;
        levelPlayer = lv;
	}
    void Update()
    {
        //levelPlayer = GameController.Instance.CurrentLevel;
    }
    void Start()
    {
        
        //CreateListContent();
    }
	public int getListContent( int index )
	{
        int lv = GUIWorldMap.Instance.listScoreLevel.Count;
        levelPlayer = lv ;
        CreateListContent();
		return contents[ index ];
	}

	public int getListLength()
	{
        
		return contents.Length;
	}
    [ContextMenu("Coppy List")]
    void CreateListContent()
    {
        contents = new int[50];
        int[] list1 = new int[levelPlayer + 1];
        int[] list2 = new int[list.Length - levelPlayer - 1];
        Array.Copy(list, list.Length - levelPlayer - 1, list1, 0, levelPlayer + 1);
        Array.Copy(list, 0, list2, 0, list.Length - levelPlayer - 1);


        list1.CopyTo(contents, 0);
        list2.CopyTo(contents, list1.Length);
    }
}
