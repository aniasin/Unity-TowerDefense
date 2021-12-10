using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        StopAllCoroutines();
        path.Clear();
        Vector2Int coordinates = new Vector2Int();
        if (!resetPath)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            path = pathFinder.GetNewPath(coordinates);
        }
        else
        {
            path = pathFinder.GetNewPath();
        }
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoords);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 EndPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelpercent = 0f;

            transform.LookAt(EndPos);

            while (travelpercent < 1f)
            {
                travelpercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, EndPos, travelpercent);
                yield return new WaitForEndOfFrame();
            }            
        }
        FinishPath();
    }

    void FinishPath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }
}
