using UnityEngine;
using System.Collections;
using PathologicalGames;
using System.Collections.Generic;

public class PoolObject : MonoSingleton<PoolObject>
{
    //pool Object
    public GameObject SpawnObject(string name, GameObject prefabs)
    {
        SpawnPool objectPool = PoolManager.Pools[name];
        Transform newObj = objectPool.Spawn(prefabs);
        return newObj.gameObject;
    }
    //pool object tai vi tri pos
    public GameObject SpawnObject(string name, GameObject prefabs, Vector3 pos)
    {
        SpawnPool objectPool = PoolManager.Pools[name];
        Transform newObj = objectPool.Spawn(prefabs, pos, Quaternion.identity);
        return newObj.gameObject;
    }
    //pool object tai vi tri pos va scale
    public GameObject SpawnObject(string name, GameObject prefabs, Vector3 pos, Vector3 scale)
    {
        SpawnPool objectPool = PoolManager.Pools[name];
        Transform newObj = objectPool.Spawn(prefabs, pos, Quaternion.identity);
        newObj.localScale = scale;
        return newObj.gameObject;
    }
    //DePool object theo thoi gian
    public void DeSpawnObjectTime(Transform transf, string name, float time)
    {
        SpawnPool objectPool = PoolManager.Pools[name];
        objectPool.Despawn(transf, time);
    }
    //Depool object
    public void DeSpawnObject(Transform transf, string name)
    {
        SpawnPool objectPool = PoolManager.Pools[name];
        objectPool.Despawn(transf);
    }
}
