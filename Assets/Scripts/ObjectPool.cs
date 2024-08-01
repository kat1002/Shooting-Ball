using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class ObjectPool
{
    public static Dictionary<string, Component> poolLookup = new Dictionary<string, Component>();
    public static Dictionary<string, Queue<Component>> poolDictionary = new Dictionary<string, Queue<Component>>();
    public static void EnqueueObject<T>(T item, string name) where T : Component {
        if (!item.gameObject.activeSelf) { return; }

        item.transform.position = Vector2.zero;
        poolDictionary[name].Enqueue(item);
        item.gameObject.SetActive(false);
    }

    public static T DequeueObject<T>(string key) where T : Component {
        if (poolDictionary[key].TryDequeue(out var item)) {
            return (T)item;
        }
        return (T)EnqueueNewInstance(poolLookup[key], key);
    }

    public static T EnqueueNewInstance<T>(T item, string key) where T : Component {
        T newInstance = Object.Instantiate(item);
        newInstance.gameObject.SetActive(false);
        newInstance.transform.position = Vector2.zero;
        poolDictionary[key].Enqueue(newInstance);
        return newInstance;
    }

    public static void SetupPool<T>(T pooledItemPrefab, int poolSize, string dictionaryEntry) where T : Component {
        poolDictionary.Add(dictionaryEntry, new Queue<Component>());
        poolLookup.Add(dictionaryEntry, pooledItemPrefab);

        for (int i = 0; i < poolSize; ++i) {
            T poolInstance = Object.Instantiate(pooledItemPrefab);
            poolInstance.gameObject.SetActive(false);
            poolDictionary[dictionaryEntry].Enqueue(poolInstance);
        }
    }

    public static void ClearAllPools()
    {
        foreach (var pool in poolDictionary)
        {
            while (pool.Value.Count > 0)
            {
                Component item = pool.Value.Dequeue();
                if (item != null)
                {
                    Object.Destroy(item.gameObject);
                }
            }
        }
        poolDictionary.Clear();
        poolLookup.Clear();
    }
}