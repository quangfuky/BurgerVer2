using UnityEngine;
using System.Collections;

public class GUIRotaition : MonoBehaviour {

    public float speedRotation;
    private float rotation = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rotation += speedRotation * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation));
	}
}
