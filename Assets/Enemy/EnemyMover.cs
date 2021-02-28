using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;
   
    void OnEnable()
    {
        FindPath();
        ReturnToStartPosition();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindPath()
    {
        path.Clear();

        GameObject way = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform waypoint in way.transform)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            } 
        }
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    void ReturnToStartPosition()
    {
        transform.position = path[0].transform.position;
    }
}
