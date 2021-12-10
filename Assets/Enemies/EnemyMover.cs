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
        FindPath();
        ReturnToStart();
        StartCoroutine(MoveAlongPath());
    }

    void FindPath()
    {
        path.Clear();
        path = pathFinder.GetNewPath();
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(path[0].coordinates);
    }

    IEnumerator MoveAlongPath()
    {
        foreach (Node node in path)
        {
            Vector3 startPos = transform.position;
            Vector3 EndPos = gridManager.GetPositionFromCoordinates(node.coordinates);
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
