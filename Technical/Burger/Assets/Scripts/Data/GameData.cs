using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class GameData : MonoSingleton<GameData> {

    public List<LevelInfo> listLevel = new List<LevelInfo>();
	// Use this for initialization
	void Awake () {
        LoadData(listLevel, "Data/Data");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void LoadData<T>(List<T> listName, string assetPath)
    {
        if (listName != null)
        {
			listName.Clear();
		}

		TextAsset textAsset = Resources.Load<TextAsset> (assetPath);

		if (textAsset)
        {
			Type typeOfT = typeof(T);

			//cat dong
			string[] temp = textAsset.text.Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);

			//lay danh sach field cua lop
			Assembly a = Assembly.GetAssembly(typeOfT);
			FieldInfo[] fieldInfo = typeOfT.GetFields(BindingFlags.Public | BindingFlags.Instance);

			//bo dong dau tien, key
			for(int i = 1; i < temp.Length; i++) 
            {
				T newObject = (T)a.CreateInstance(typeOfT.FullName);

				//Debug.Log("Line " + i + " = " + temp[i]);
				string[] context = temp[i].Split(new char[]{'\t'});
				for(int j = 0; j < fieldInfo.Length; j++) 
                {
//					try {
//
//					}catch(Exception ex) {
//
//					}
					string collumnValue = context[j];
					if(fieldInfo[j].FieldType == typeof(String))
                    {
						fieldInfo[j].SetValue(newObject, collumnValue.Substring(1, context[j].Length - 2));
					}
                    else if (fieldInfo[j].FieldType == typeof(Int32)){

						int value = 0;
						if(collumnValue.Length > 0)
                        {
							value = Int32.Parse(collumnValue);
						}
						fieldInfo[j].SetValue(newObject, value);
					}
                    else if (fieldInfo[j].FieldType == typeof(float)) 
                    {
						float value = 0.0f;
						if(collumnValue.Length > 0) 
                        {
							value = float.Parse(collumnValue);
						}
						fieldInfo[j].SetValue(newObject, value);
					}
				}
				listName.Add(newObject);
			}

		}
	}
    private static GameData _instance;

    public static GameData Instance
    {
        get
        {
            if (GameData._instance == null)
            {
                var go = GameObject.Find("GameData");
                if (go == null)
                {
                    go = new GameObject("GameData");
                    GameData._instance = go.AddComponent<GameData>();
                }
                else {
                    GameData._instance = go.GetComponent<GameData>();
                }
            }
            return GameData._instance;
        }
    }
}
