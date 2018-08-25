using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    WaveConfig currentWave;
    int currentWaveIndex = 0;
    int startingWaveIndex = 0;


	// Use this for initialization
	void Start () {
        currentWave = waveConfigs[startingWaveIndex];
        StartCoroutine(SpawnAllEnemiesInWave());
	}

    private IEnumerator SpawnAllEnemiesInWave()
    {
        int enemiesNumber = currentWave.GetNumberOfEnemies();
        for (int i = 0; i < enemiesNumber; i++)
        {
            Instantiate(currentWave.GetEnemyPrefab());
            yield return new WaitForSeconds(currentWave.GetSpawnPeriod());
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
