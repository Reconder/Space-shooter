using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] bool random = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        do
        {
            yield return StartCoroutine(SpawnAllWavesRandomly());
        } while (random);
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private static readonly Random rnd = new Random();

    private IEnumerator SpawnAllWavesRandomly()
    {
        int waveIndex = rnd.Next(0, waveConfigs.Count);
        var currentWave = waveConfigs[waveIndex];
        yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {

        for (int enemiesIndex = 0; enemiesIndex < waveConfig.GetNumberOfEnemies(); enemiesIndex++) { 
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(), 
                waveConfig.GetWaypoints()[0].transform.position, 
                Quaternion.identity);
            newEnemy.GetComponent<IEnemyPathing>().SetWaveConfig(waveConfig); 
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
    }

    }
    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex <= waveConfigs.Count; waveIndex++)
        {
            Debug.Log(waveIndex);
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    
}
