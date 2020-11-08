﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] string type = "enemy";

    public GameObject EnemyPrefab => enemyPrefab;
    public float TimeBetweenSpawns => timeBetweenSpawns; 
    public float SpawnRandomFactor => spawnRandomFactor;
    public int NumberOfEnemies => numberOfEnemies; 
    public float MoveSpeed => moveSpeed;
    public string EnemyType => type;

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
}
