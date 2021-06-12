using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public Transform Parent;
        public Transform PlayerTransform;
        public Transform TurretTransform;
        
        public List<EnemyCreateInfo> Infos;

        private List<Enemy> _enemies;

        public void Awake()
        {
            _enemies = new List<Enemy>();
            
            foreach (var info in Infos)
            {
                for (int i = 0; i < info.Count; ++i)
                {
                    Enemy enemy = Instantiate(info.Prefab, Parent)
                        .GetComponent<Enemy>();
                    
                    enemy.gameObject.SetActive(false);

                    enemy.PlayerTransform = PlayerTransform;
                    enemy.TurretTransform = TurretTransform;
                    
                    _enemies.Add(enemy);
                }
            }
        }
    }

    [Serializable]
    public struct EnemyCreateInfo
    {
        public int Count;
        public Enemy Prefab;
    }
}
