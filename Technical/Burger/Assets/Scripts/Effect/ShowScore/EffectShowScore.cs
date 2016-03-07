using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class EffectShowScore : MonoBehaviour {

    public int score;
    public Vector3 speed;
    public Vector3 scale;
    public List<Sprite> listNumber;
    public SpriteRenderer spriteHead;
    public SpriteRenderer spriteTail;
	// Use this for initialization
    void Update()
    {
        Move();
    }
    [ContextMenu("Init")]
    public void Init(int _score)
    {
        speed = new Vector3(0, 1.0f, 0);
        CaculaterSprite(_score);
        MoveScale();
        PoolObject.Instance.DeSpawnObjectTime(gameObject.transform, "Number", 2.0f);

    }
    void Move()
    {
        transform.localPosition += speed * Time.deltaTime;
    }
    [ContextMenu("Scale")]
    void MoveScale()
    {
        HOTween.To(gameObject.transform, 0.3f, new TweenParms()
            .Prop("localScale", scale, false)
            .Ease(EaseType.EaseOutBack)
            .Loops(2, LoopType.Yoyo)
            );
    }
    [ContextMenu("Test")]
    public void ShowScore()
    {
        CaculaterSprite(score);
    }
    public void CaculaterSprite(int _score)
    {
        int hangchuc = 0;
        int hangdonvi = 0;

        hangchuc = (int)_score / 10;
        hangdonvi = (int)_score % 10;

        if(hangchuc > 0)
        {
            spriteHead.sprite = listNumber[hangchuc];
        }
        else
        {
            spriteHead.sprite = null;
        }
        
        spriteTail.sprite = listNumber[hangdonvi];
    }
}
