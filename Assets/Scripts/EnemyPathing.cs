using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;


    // Use this for initialization
    void Start()
    {
        //waveConfig = FindObjectOfType<WaveConfig>();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void Move()
    {
        //Debug.Log("WaypontIndex: " + waypointIndex.ToString());
        //Debug.Log("WaypontCount: " + waypoints.Count.ToString());
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].position;

            //Debug.Log("target pos: " + targetPosition.ToString());
            //Debug.Log("GetMoveSpeed: " + waveConfig.GetMoveSpeed().ToString());
            //Debug.Log("deltaTime: " + Time.deltaTime.ToString());
            //Debug.Log("old pos: " + transform.position.ToString());

            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            //Debug.Log("new pos: " + transform.position.ToString());

            if (transform.position == targetPosition)
                waypointIndex++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
