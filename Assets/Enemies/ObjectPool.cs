using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0.1f, 30f)] float timeBetweenSpawn = 1f;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] bool isActive;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (isActive)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(timeBetweenSpawn);
        }        
    }

    void EnableObjectInPool()
    {
        foreach(GameObject newEnemy in pool)
        {
            if (!newEnemy.activeInHierarchy)
            {
                newEnemy.SetActive(true);
                break;
            }
        }
    }
}
