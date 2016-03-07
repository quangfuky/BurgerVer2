using UnityEngine;
using System.Collections;

public class Page : MonoSingleton<Page>
{
    public GameObject sprite;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Disable()
    {
        if(sprite!=null)
        sprite.SetActive(false);
    }

    public void Enable()
    {
        if(sprite!=null)
        sprite.SetActive(true);
    }
}
