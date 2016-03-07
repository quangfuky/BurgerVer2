using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelInfo 
{
    public int level;//level cua nguoi choi
    public int maxUnlockCake;//so luong banh duoc mo
    public int maxPieceCake;//so luong banh dc random toi da
    public int maxUnlockFruit;//so luong hoa qua duoc mo
    public int maxPieceFruit;//so luong trai cay toi da - luon luon = 5
    public int timeGame;//thoi gian choi cua level
    public int oneStar;//so diem toi thieu dat duoc 1 sao
    public int twoStar;//so diem toi thieu dat dc 2 sao
    public int threeStar;//so diem toi thieu dat dc 3 sao
}
