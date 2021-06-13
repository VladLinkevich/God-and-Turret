using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class BulletFactory : MonoBehaviour
    {
        public static BulletFactory Instance;
        
        public Transform Parent;
        public Bullet BulletPrefab;
        public int BulletCount;

        private List<Bullet> _bullets;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            _bullets = new List<Bullet>();
            
            for (int i = 0; i < BulletCount; ++i)
            {
                Bullet bullet = Instantiate(BulletPrefab, Parent).GetComponent<Bullet>();
                bullet.gameObject.SetActive(false);
                _bullets.Add(bullet);
            }
        }

        public Bullet GetFreeBullet()
        {
            return _bullets.Find(bullet => bullet.gameObject.activeSelf == false);
        }
    }
}
