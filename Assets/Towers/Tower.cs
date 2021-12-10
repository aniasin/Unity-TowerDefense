using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] [Range(0.1f, 10f)] float buildTime = 1f;

    void Start()
    {
        StartCoroutine(Build());
    }
    public bool CreateTower(Tower tower, Vector3 posittion)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (!bank) return false;

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, posittion, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        return false;
    }

    IEnumerator Build()
    {
        for (int i = 0; i < 3; i++)
        {
            print("TOWER: BUILD..." + i);
            yield return new WaitForSeconds(buildTime);
        }        
    }
}
