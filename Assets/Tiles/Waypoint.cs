using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isNotPlaceable;
    [SerializeField] Tower tower;
    public bool IsNotPlaceable { get{ return isNotPlaceable; }}

    public bool GetIsNotPlaceable()
    {
        return isNotPlaceable;
    }

    private void OnMouseDown()
    {
        if (!isNotPlaceable)
        {
            bool isPlaced;
            isPlaced = tower.CreateTower(tower, transform.position);
            isNotPlaceable = isPlaced;
        }        
    }
}
