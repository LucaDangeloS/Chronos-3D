using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMoveWaypoints : MonoBehaviour
{

    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 4f;
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;

    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f) {
            currentWaypointIndex = (++currentWaypointIndex) % waypoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}
