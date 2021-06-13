using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyFactory factory;
        
        public int maxEnemyCount;
        public int minEnemyCount;

        public float maxDurationBetweenSpawn;
        public float minDurationBetweenSpawn;

        private int _enemyCount;
        private float _durationBetweenSpawn;
        
        
        public void StartSpawn(Room.Room room)
        {
            _enemyCount = Random.Range(maxEnemyCount, minEnemyCount);
            _durationBetweenSpawn = Random.Range(maxDurationBetweenSpawn, minDurationBetweenSpawn);

            StartCoroutine(SpawnEnemy(room));
        }

        private IEnumerator SpawnEnemy(Room.Room room)
        {
            for (int i = 0; i < _enemyCount; ++i)
            {
                Enemy enemy = factory.GetFreeEnemy();
                enemy.gameObject.SetActive(true);
                enemy.transform.position =
                    room.SpawnPosition[Random.Range(0, room.SpawnPosition.Count)].position;
                
                yield return new WaitForSeconds(_durationBetweenSpawn);
            }
        }
    }
}
