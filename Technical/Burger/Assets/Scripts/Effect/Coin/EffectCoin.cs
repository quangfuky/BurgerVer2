using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectCoin : MonoBehaviour {

    public GameObject coinprefabs;

    public List<GameObject> listCoin = new List<GameObject>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void RenderCoin(TypeParticle type, string name, Vector3 pos, Vector3 scale)
    {
        if (coinprefabs != null)
        {
            GameObject coin =PoolObject.Instance.SpawnObject(name, coinprefabs, pos, scale);
            if(!listCoin.Contains(coin))
            {
                listCoin.Add(coin);
            }
        }
        else
        {
            Debug.Log("K Get dc Particle");
        }
    }
}
