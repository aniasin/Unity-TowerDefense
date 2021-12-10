using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinatesHandler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.grey;
    [SerializeField] Color pathColor = Color.green;
    Vector2Int coordinates;
    GridManager gridManager;

    TextMeshPro text;


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        text = GetComponent<TextMeshPro>();
        text.enabled = true;
        DisplayCoordinates();        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
        }
        SetTextColor();
        ToggleTexts();
    }

    void DisplayCoordinates()
    {
        if (!gridManager) return;

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        text.text = coordinates.x + ", " + coordinates.y;
    }

    void ToggleTexts()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            text.enabled = !text.enabled;
        }        
    }

    void SetTextColor()
    {
        if (!gridManager) return;

        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;

        if (!node.isWalkable)
        {
            text.color = blockedColor;
        }
        else if (node.isPath)
        {
            text.color = pathColor;
        }
        else if (node.isExplored)
        {
            text.color = exploredColor;
        }
        else
        {
            text.color = defaultColor;
        }
    }

    void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
