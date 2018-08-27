﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;


    void Start()
    {
        //waypoints = waveConfig.GetWaypoints();
        //transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        if (waypoints != null)
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
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
                waypointIndex++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
