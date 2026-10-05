using UnityEngine;
using System.Collections;
using PB.SYSTEM;
using System;
using System.Collections.Generic;

public class PParticlePoolManager : PMonoSingleton<PParticlePoolManager>
{
    [Serializable]
    public class Pool
    {
        public string Key;
        public ParticleSystem Effect;
    }

    [SerializeField] List<Pool> pools;
    [SerializeField] Transform container;
    Dictionary<string, Queue<GameObject>> poolsDict;

    protected override void Awake()
    {
        base.Awake();
        poolsDict = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();
            poolsDict.Add(pool.Key, objectPool);
        }
    }

    public void Show(string key, Vector3 position, Vector3 size)
    {
        var particle = Get(key, position, size);
        particle.Clear();
        particle.Play();
        if (particle.main.loop == false)
            StartCoroutine(ReleaseAfterDelayCoroutine(key, particle.gameObject, particle.main.duration));
    }

    public void Show(string key, Vector3 position, float size)
    {
        Show(key, position, new Vector3(size, size, size));
    }

    public void Hide(string key, GameObject obj)
    {
        Release(key, obj);
    }

    public ParticleSystem Get(string key, Vector3 position, Vector3 size)
    {
        if (HasPool(key) == false) return null;

        var objectPool = poolsDict[key];
        if (objectPool == null || objectPool.Count == 0)
        {
            CreateNewObj(key);
        }

        var obj = objectPool.Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.localScale = size;
        return obj.GetComponent<ParticleSystem>();
    }

    public void Release(string key, GameObject obj)
    {
        if (HasPool(key) == false) return;

        obj.SetActive(false);
        poolsDict[key].Enqueue(obj);
    }


    protected IEnumerator ReleaseAfterDelayCoroutine(string key, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Release(key, obj);
    }

    private void CreateNewObj(string key)
    {
        var pool = pools.Find(x => x.Key == key);
        var prefab = pool.Effect.gameObject;
        var obj = Instantiate(prefab, container);
        poolsDict[key].Enqueue(obj);
        obj.SetActive(false);
    }

    private bool HasPool(string key)
    {
        if (poolsDict.ContainsKey(key))
        {
            return true;
        }
        else
        {
            PLog.Error($"PoolDictionary doesn't contain key: {key}");
            return false;
        }
    }
}