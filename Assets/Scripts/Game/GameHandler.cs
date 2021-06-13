using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameHandler : MonoBehaviour
    {
        public static GameHandler Instance = null;

        private List<Enemy.Enemy> _currentEnemies;
        private bool _isAttack;

        public bool IsAttack => _isAttack; 
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _currentEnemies = new List<Enemy.Enemy>();
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddActiveEnemy(Enemy.Enemy enemy)
        {
            _isAttack = true;
            
            _currentEnemies.Add(enemy);
        }

        public void RemoveActiveEnemy(Enemy.Enemy enemy)
        {
            _currentEnemies.Remove(enemy);

            if (_currentEnemies.Count == 0)
            {
                _isAttack = false;
            }
        }
        
    }
}
