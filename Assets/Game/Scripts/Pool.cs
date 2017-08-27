using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Pool
{
    public GameObject prefab;
    public int amount;

    GameObject root;
    Transform mParent;

    private class Instance
    {
        public GameObject go;
        public bool spawned;

        public Instance(GameObject i)
        {
            spawned = false;
            go = i;
        }
    }

    List<Instance> mInstances = new List<Instance>();
    Dictionary<GameObject, Instance> mGameObjectToInstance = new Dictionary<GameObject, Instance>();

    public int instanceIndex { get; private set; }

    void CreateRoot()
    {
        if (!root)
        {
            root = new GameObject(ToString());

            if (mParent)
            {
                root.transform.parent = mParent;
            }
        }
    }

    Instance CreateInstance()
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        Instance instance = new Instance(go);
        mInstances.Add(instance);
        mGameObjectToInstance.Add(go, instance);

        Despawn(go);

        return instance;
    }

    public void PreloadInstances(Transform parent)
    {
        mParent = parent;
        CreateRoot();

        mInstances.Clear();
        mGameObjectToInstance.Clear();

        if (prefab && amount > 0)
        {
            for (int i = 0; i < amount; ++i)
            {
                CreateInstance();
            }
        }
    }

    public GameObject Spawn()
    {
        Instance instance = null;

        for (int i = 0; i < mInstances.Count; i++)
        {
            Instance inst = mInstances[i];
            if (!inst.spawned)
            {
                instance = inst;
                break;
            }
        }

        if (instance == null)
        {
            instance = CreateInstance();
        }

        GameObject spawnedObject = instance.go;
        instance.spawned = true;

        spawnedObject.SetActive(true);
        spawnedObject.transform.SetParent(null);
        spawnedObject.transform.localPosition = Vector3.zero;
        spawnedObject.transform.localRotation = Quaternion.identity;
        spawnedObject.transform.localScale = Vector3.one;

        spawnedObject.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

        return spawnedObject;
    }

    public void Despawn(GameObject spawnedObject)
    {
        if (mGameObjectToInstance.ContainsKey(spawnedObject))
        {
            if (mGameObjectToInstance[spawnedObject].spawned)
            {
                spawnedObject.SendMessage("OnDespawn", SendMessageOptions.DontRequireReceiver);
            }

            mGameObjectToInstance[spawnedObject].spawned = false;
            spawnedObject.SetActive(false);
            spawnedObject.transform.SetParent(root.transform);
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.transform.position = Vector3.zero;
            spawnedObject.transform.localScale = Vector3.one;
        }
    }

    public bool IsSpawned(GameObject go)
    {
        if (Contains(go))
        {
            return mGameObjectToInstance[go].spawned;
        }

        return false;
    }

    public bool Contains(GameObject spawnedObject)
    {
        return mGameObjectToInstance.ContainsKey(spawnedObject);
    }

    override public string ToString()
    {
        if (prefab)
        {
            return prefab.name;
        }

        return "pool";
    }
}