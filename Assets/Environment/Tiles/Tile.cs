using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    PathFinder pathFinder;

    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } //property example (also getter can be used instead of this)

    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }

        }
    }
}
