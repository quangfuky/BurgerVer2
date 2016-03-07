using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    public GameObject AddLife;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScenes(string Name)
    {
        Application.LoadLevel(Name);
    }

    public void LoadLv()
    {
        Application.LoadLevel(gameObject.transform.name);
    }

    public void AddLifeHome(string Name)
    {
        if (AddLife != null)
        {
            AddLife.SetActive(false);
        }
        Application.LoadLevel(Name);
    }
}
