using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float width = 1136f;
    public float height =  640f;
    // Use this for initialization
    void Start()
    {
        //width = Screen.width;
        //height = Screen.height;
        Camera.main.aspect = width / height;
    }
}
