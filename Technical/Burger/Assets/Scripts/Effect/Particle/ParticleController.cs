using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TypeParticle
{
    NONE = 0,
    HOA_ROI_NHIEU = 1,
    HOA_ROI_IT = 2,
    PHAO_HOA_1 = 3,
    PHAO_HOA_2 = 4,
    PHAO_HOA_3 = 5,

    PHAO_BONG_1 = 6,
    PHAO_BONG_2 = 7,
    PHAO_BONG_3 = 8,
    PHAO_BONG_4 = 9,
    PHAO_BONG_5 = 10,

    STAR_1 = 11,
    STAR_2 = 12,
    SMILE_1 = 13,
    SMILE_2 = 14,
    FROWM_1 = 13,

    

}
[System.Serializable]
public class ParticleInfo
{
    public TypeParticle type;
    public GameObject particle;
}

public class ParticleController : MonoSingleton<ParticleController>
{

    public List<ParticleInfo> listParticle;

    public TypeParticle type;

    private float t = 0;
    [ContextMenu("Test")]
    void test()
    {
        RenderParticle(type, "Particle", Vector3.zero);
    }
    void Start()
    {
        ParticleScreenStart();
    }
    void Update()
    {
        if(t > 1.5f)
        {
            ParticleScreenWorldMap();
            t = 0;
        }
        t += Time.deltaTime;
    }
    
    public void ParticleScreenStart()
    {
        RenderParticle(TypeParticle.HOA_ROI_IT, "Particle", Vector3.zero);
        RenderParticle(TypeParticle.HOA_ROI_NHIEU, "Particle", Vector3.zero);
    }
    public void DeParticleScreenStart()
    { }

    [ContextMenu("World Map")]
    public void ParticleScreenWorldMap()
    {
        int type = Random.Range(3, 5);
        Vector3 pos = new Vector3(Random.Range(-6, 6), Random.Range(-3, 4), 0);
        RenderParticle((TypeParticle)type, "Particle", pos);
    }

    //tao Particle o vi tri pos va Scale
    public void RenderParticle(TypeParticle type,string name, Vector3 pos, Vector3 scale)
    {
        GameObject obj = GetParticle(type);
        if(obj != null)
        {
            PoolObject.Instance.SpawnObject(name, obj, pos, scale);
        }
        else
        {
            Debug.Log("K Get dc Particle");
        }
    }
    //tao Particle o vi tri pos
    public void RenderParticle(TypeParticle type, string name, Vector3 pos)
    {
        GameObject obj = GetParticle(type);
        if (obj != null)
        {
            PoolObject.Instance.SpawnObject(name, obj, pos);
            //PoolObject.Instance.SpawnObject(name, obj);
        }
        else
        {
            Debug.Log("K Get dc Particle");
        }
    }

    public GameObject GetParticle(TypeParticle _type)
    {
        for (int i = 0; i < listParticle.Count; i++)
        {
            if (listParticle[i].type == _type)
            {
                return listParticle[i].particle;
            }
        }
        return null;
    }
}
