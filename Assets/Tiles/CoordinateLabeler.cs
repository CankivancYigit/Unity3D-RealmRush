using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
[SelectionBase]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    Waypoint waypoint;

    private void Awake()
    {
        label = GetComponentInChildren<TextMeshPro>();
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        DisplayCoordinates();
        ColorCoordinates();
        ToggleColor();
    }

    void ToggleColor()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void ColorCoordinates()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    private void DisplayCoordinates()
    {
        Vector3 coordinates;
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.z = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
        string labelText = coordinates.x  + "," + coordinates.z;

        label.text = labelText;    
        gameObject.name = labelText;    //Updates Object Name To Coordinates
    }
}
