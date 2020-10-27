using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPathing : MonoBehaviour
{

    WaveConfig waveConfig;
    List<Transform> waypoints;
    float moveSpeed;

    int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        waypoints = waveConfig.GetWaypoints();
        waypointIndex = Random.Range(0, waypoints.Count);
        transform.position = waypoints[waypointIndex].position;
        moveSpeed = waveConfig.GetMoveSpeed();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        var targetPosition = waypoints[waypointIndex].transform.position + new Vector3(0 , -18.5f, 0);
        Debug.Log(targetPosition);
        if (waypoints[waypointIndex].position != targetPosition)
        {
            
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
