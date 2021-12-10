using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isNotPlaceable;
    [SerializeField] Tower tower;

    Vector2Int coordinates;
    PathFinder pathFinder;
    GridManager gridManager;
    public bool IsNotPlaceable { get{ return isNotPlaceable; }}

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if (gridManager)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (isNotPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }            
        }
    }

    public bool GetIsNotPlaceable()
    {
        return isNotPlaceable;
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = tower.CreateTower(tower, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }        
    }
}
