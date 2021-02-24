using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CoordinateLabeler : MonoBehaviour
{
    TextMesh textMesh;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        DisplayCoordinates();
    }

    private void DisplayCoordinates()
    {
        Vector3 coordinates;
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.z = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
        string labelText = coordinates.x  + "," + coordinates.z;

        textMesh.text = labelText;    
        gameObject.name = labelText;    //Updates Object Name To Coordinates
    }
}
