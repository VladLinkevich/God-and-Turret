using System;
using System.Collections.Generic;
using Turret;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthHandler : MonoBehaviour
    {
        public static HealthHandler Instance;
        
        public List<Image> Healths;
        public TurretBulletHandler BulletHandler;
        
        public float invulnerabilityTime = 2f;

        private float lastHitTime;
        
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
        }

        public void RemoveHealth()
        {
            if (lastHitTime + invulnerabilityTime < Time.time)
            {
                lastHitTime = Time.time;
                
                Image image = Healths[Healths.Count - 1];
                image.gameObject.SetActive(false);

                Healths.Remove(image);
                BulletHandler.TakeDamage();
                
                if (Healths.Count == 0)
                {
                    Messenger.Broadcast(GameEvent.GAMEOVER);
                }
            }
        }
    }
}