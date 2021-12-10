using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;

    private void Awake()
    {

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
}
