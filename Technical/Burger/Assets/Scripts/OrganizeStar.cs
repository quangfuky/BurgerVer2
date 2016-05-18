using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrganizeStar : MonoSingleton<OrganizeStar>
{
    public GameObject Parent;
    public List<GameObject> Star;
    public List<GameObject> GoldStar;
    public GameObject Bar1;
    public GameObject Bar2;

    public int One, Two, Three;
    // Use this for initialization
    void Start()
    {
        One = GameData.Instance.listLevel[GameController.Instance.CurrentLevel].oneStar;
        Two = GameData.Instance.listLevel[GameController.Instance.CurrentLevel].twoStar;
        Three = GameData.Instance.listLevel[GameController.Instance.CurrentLevel].threeStar;

        //CalStarPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("test")]
    public void CalStarPosition()
    {
        Star[0].transform.localPosition = new Vector3((240*((float) One/Three)), Star[0].transform.localPosition.y, 0);
        Star[1].transform.localPosition = new Vector3((240*((float) Two/Three)), Star[0].transform.localPosition.y, 0);
        Star[2].transform.localPosition = new Vector3(240, Star[0].transform.localPosition.y, 0);
        GoldStar[0].transform.localPosition = new Vector3((240*((float) One/Three)),
            GoldStar[0].transform.localPosition.y, 0);
        GoldStar[1].transform.localPosition = new Vector3((240*((float) Two/Three)),
            GoldStar[0].transform.localPosition.y, 0);
        GoldStar[2].transform.localPosition = new Vector3(240, GoldStar[0].transform.localPosition.y, 0);
        Bar1.transform.localPosition = new Vector3((240*((float) One/Three)), Bar1.transform.localPosition.y, 0);
        Bar2.transform.localPosition = new Vector3((240*((float) Two/Three)), Bar2.transform.localPosition.y, 0);
    }
}
