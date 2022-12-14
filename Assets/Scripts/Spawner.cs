using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave; //현재웨이프의 레퍼런스
    int currentWaveNumber; //현재 웨이브 휫수

    int enemiesRemainingToSpawn; //남아있는 스폰할 적
    float nextSpawnTime; //스폰시간

    void Start()
    {
        NextWave(); 
    }
    void Update()
    {
         if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy; //적 프리펩 인스턴스화
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];

        enemiesRemainingToSpawn = currentWave.enemyCount;
    }
    [System.Serializable] //인스펙터에 표시
    public class Wave {
        public int enemyCount;
        public float timeBetweenSpawns;
        }

}
