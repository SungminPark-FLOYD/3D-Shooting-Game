using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave; //����������� ���۷���
    int currentWaveNumber; //���� ���̺� �ܼ�

    int enemiesRemainingToSpawn; //�����ִ� ������ ��
    float nextSpawnTime; //�����ð�

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

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy; //�� ������ �ν��Ͻ�ȭ
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];

        enemiesRemainingToSpawn = currentWave.enemyCount;
    }
    [System.Serializable] //�ν����Ϳ� ǥ��
    public class Wave {
        public int enemyCount;
        public float timeBetweenSpawns;
        }

}
