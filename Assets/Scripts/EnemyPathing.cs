using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    int waypointIndex = 0;
    List<Transform> waypoints;
    float moveSpeed;
   

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig; 
        // 'this.' Implies the waveConfig from the EnemyPathing Class, is set to the waveConfig passed in by caller of SetWaveConfig
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            //var currentPosition = transform.position; //Was used for debugging
            //Debug.Log("Position " + currentPosition.ToString());
            //Debug.Log("Waypoint " + targetPosition.ToString());
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
