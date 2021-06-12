using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Player;
using Turret;
using UnityEngine;

namespace Particles
{
    public class HelpTouchTurret : MonoBehaviour
    {
        public PlayerMovement Player;
        public TurretMoveHandler Turret;

        public float Duration;

        private bool _isSubscribe = false;
        private Tween _tween;
        
        private void Start()
        {
            Subscribe();
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (_isSubscribe == false)
            {
                _isSubscribe = true;
                Messenger.AddListener(GameEvent.HELPSHOWTURRET, Help);
            }
        }

        private void Unsubscribe()
        {
            if (_isSubscribe == true)
            {
                _isSubscribe = false;
                Messenger.RemoveListener(GameEvent.HELPSHOWTURRET, Help);
            }
        }

        private void Help()
        {
            gameObject.SetActive(true);
            
            _tween.Kill();
            
            transform.position = Player.transform.position;

            _tween = transform.DOMove(Turret.transform.position, Duration)
                .OnComplete(() =>
                {
                    transform.DOShakePosition(Duration, 0.5f, 5)
                        .OnComplete(() => gameObject.SetActive(false));
                });
            
        }
    }
}
