using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    int currentWaveIndex = 0;
    int startingWaveIndex = 0;


	// Use this for initialization
	void Start () {
        WaveConfig currentWave = waveConfigs[startingWaveIndex];
        StartCoroutine(SpawnAllWaves());
	}

    private IEnumerator SpawnAllWaves()
    {
        for (int i = 0; i < waveConfigs.Count; i++)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[i]));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        int enemiesNumber = currentWave.GetNumberOfEnemies();
        for (int i = 0; i < enemiesNumber; i++)
        {
            var newEnemy = Instantiate(currentWave.GetEnemyPrefab(), currentWave.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);
            yield return new WaitForSeconds(currentWave.GetSpawnPeriod());
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
