using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector2[] Point;
    [SerializeField] float speed = 2f;
    [SerializeField] Transform Platform;
    private int currentWaypointIndex;
    private bool reverseDirection = false;

    // Transform target;

    void Start()
    {
        currentWaypointIndex = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance((Vector2)transform.position + Point[currentWaypointIndex], Platform.position) < 0.1f)
        {
            currentWaypointIndex += reverseDirection ? -1 : 1;

            if (currentWaypointIndex >= Point.Length)
            {
                currentWaypointIndex = Point.Length - 1;
                reverseDirection = true;
            }
            else if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 0;
                reverseDirection = false;
            }
        }
        Platform.position = Vector2.MoveTowards(Platform.position, (Vector2)transform.position + Point[currentWaypointIndex], Time.deltaTime * speed);
    }
   
    private void OnDrawGizmos()
    {
        if (Point != null && Point.Length > 0)
        {
            for (int i = 0; i < Point.Length; i++)
            {
                if (i + 1 < Point.Length)
                {
                    Vector3 position1 = new Vector3(Point[i].x, Point[i].y, 0f);
                    Vector3 position2 = new Vector3(Point[i + 1].x, Point[i + 1].y, 0f);
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(transform.position + position1, 0.1f);
                    Gizmos.DrawSphere(transform.position + position2, 0.1f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position + position1, transform.position + position2);
                }
            }
        }
    }
}
