using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public GameObject Parent;
        public List<EnemyCreateInfo> Infos;
        
        public void Awake()
        {
            foreach (var info in Infos)
            {
                for (int i = 0; i < info.Count; ++i)
                {
                    //Instantiate()
                }
            }
        }
    }

    [Serializable]
    public struct EnemyCreateInfo
    {
        public int Count;
        public Enemy prefab;
    }
}
