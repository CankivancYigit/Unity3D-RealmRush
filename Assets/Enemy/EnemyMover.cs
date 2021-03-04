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

    void OnEnable()
    {
        ReturnToStartPosition();
        RecalculatePath(true);       
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();

        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    private void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    void ReturnToStartPosition()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }
}
