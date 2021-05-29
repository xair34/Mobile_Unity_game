using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves
{
    public string waveName;
    public Transform enemy;
    public int count;
    public float rate;
}
public class WaveSpawner : MonoBehaviour
{
    public Waves[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public enum SpawnState { Spawning, Waiting, Counting};
    private SpawnState state = SpawnState.Counting;

    private float searchCountdown = 1f;

    public Transform[] spawnPoints;
    private void Start()
    {
        waveCountDown = timeBetweenWaves;
    }
    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            if(state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }

        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        state = SpawnState.Counting;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Waves _wave)
    {
        Debug.Log("spawning wave" + _wave.waveName);
        state = SpawnState.Spawning;

        for(int i =0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;
        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        if(spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points ");
        }
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
    }

}
