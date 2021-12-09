using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinatesHandler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;
    Vector3 lastPos;
    Vector2Int coordinates;
    TextMeshPro text;
    Waypoint waypoint;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        text.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying && transform.parent.position != lastPos)
        {
            DisplayCoordinates();
            UpdateTileName();
            lastPos = transform.parent.position;
        }
        SetTextColor();
        ToggleTexts();
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
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
        if (waypoint.IsNotPlaceable)
        {
            text.color = blockedColor;
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
