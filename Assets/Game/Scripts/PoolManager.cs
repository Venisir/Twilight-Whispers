using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public static bool isActive { get { return instance != null; } }

    public static GameObject Spawn(string prefabName)
    {
        if (instance)
        {
            return instance._Spawn(prefabName);
        }

        return null;
    }

    public static GameObject Spawn(GameObject prefab)
    {
        if (instance)
        {
            return instance._Spawn(prefab);
        }

        return null;
    }

    public static void Despawn(GameObject spawnedObject)
    {
        if (instance)
        {
            instance._Despawn(spawnedObject);
        }
    }

    public List<Pool> pools = new List<Pool>();

    Dictionary<GameObject, Pool> prefabPools = new Dictionary<GameObject, Pool>();

    void Awake()
    {
        if (!instance)
        {
            instance = this;

            for (int i = 0; i < pools.Count; i++)
            {
                Pool pool = pools[i];
                pool.PreloadInstances(transform);
                if (pool.prefab)
                {
                    prefabPools.Add(pool.prefab, pool);
                }
            }

        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ReloadPrefabs()
    {
        prefabPools.Clear();

        for (int i = 0; i < pools.Count; i++)
        {
            Pool pool = pools[i];
            pool.PreloadInstances(transform);
            if (pool.prefab)
            {
                prefabPools.Add(pool.prefab, pool);
            }
        }

    }

    GameObject _Spawn(string objectName)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            Pool pool = pools[i];
            if (pool.prefab.name == objectName)
            {
                return pool.Spawn();
            }
        }

        return null;
    }

    GameObject _Spawn(GameObject prefab)
    {
        if (prefabPools.ContainsKey(prefab))
        {
            return prefabPools[prefab].Spawn();
        }

        return null;
    }

    void _Despawn(GameObject spawnedObject)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            Pool pool = pools[i];
            if (pool.Contains(spawnedObject))
            {
                pool.Despawn(spawnedObject);
                return;
            }
        }
    }
}