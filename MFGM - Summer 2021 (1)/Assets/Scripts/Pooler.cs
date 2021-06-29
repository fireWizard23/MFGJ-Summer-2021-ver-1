using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    private static Pooler instance;
    public static Pooler Instance { 
        get
        {
            if (instance == null) instance = new Pooler();
            return instance;
        } 
    }

    [System.Serializable]
    private class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    [SerializeField]
    private List<Pool> AllPools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
        
    
    void Start()
    {
        instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in AllPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool); 
            
        }
    }

    public GameObject Get(string tag)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Tag not found.");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        objToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }



}
