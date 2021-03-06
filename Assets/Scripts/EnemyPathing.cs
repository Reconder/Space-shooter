﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour, IEnemyPathing
{

    WaveConfig waveConfig;
    List<Transform> waypoints;
    float moveSpeed;

    int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
        moveSpeed = waveConfig.MoveSpeed;
    }

    // Update is called once per frame
    void Update() => Move();

    public void SetWaveConfig(WaveConfig waveConfig) => this.waveConfig = waveConfig;

    public void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
