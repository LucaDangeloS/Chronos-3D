using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePathway : MonoBehaviour
{
    GameObject[] enemies;
    GameObject[] rocks;
    GameObject[] waypoints;
    public RotateRockSet rocksAxis;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get all enemies in the scene
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        rocks = GameObject.FindGameObjectsWithTag("Rock");
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
            return;
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        Debug.Log(enemies.Length);
        if (enemies.Length  == 0 )
        {
            AssignWaypointsToRocks();
        }
    }

    void AssignWaypointsToRocks()
    {
        int n_waypoints = waypoints.Length;
        rocksAxis.Stop();
        for (int i = 0; i < rocks.Length; i++)
        {
            if (i < n_waypoints)
            {
                TransitionSmoothWaypointMovement rockMovement = rocks[i].GetComponent<TransitionSmoothWaypointMovement>();
                rockMovement.waypoints.Add(waypoints[i]);
                rockMovement.ForceMove(waypoints[i].transform.position);
            }
            else {
                rocks[i].GetComponent<Rigidbody>().useGravity = true;
            }
        }
        triggered = true;
    }
}
