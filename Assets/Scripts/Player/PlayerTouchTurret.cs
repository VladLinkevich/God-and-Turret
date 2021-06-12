using System;
using DG.Tweening;
using Turret;
using UnityEngine;

namespace Player
{
    public class PlayerTouchTurret : MonoBehaviour
    {
        public GameObject PlayerImage;
        public GameObject PlayerWithTurretImage;

        public TurretMoveHandler Turret;

        public float TouchDuration = 0.3f;
        public float TimeToTouch = 0.1f;

        private float _lastTouchTime = 0.0f;
        private bool _isPossibleTouch = false;
        private bool _isTouchTurret = false;

        public bool IsTouchTurret => _isTouchTurret;

        private void OnEnable()
        {
            Messenger.AddListener(GameEvent.READYTOTOUCHTURRET, EnterTouch);
            Messenger.AddListener(GameEvent.STOPTOTOUCHTURRET, ExitTouch);
        }
        
        private void OnDisable()
        {
            Messenger.RemoveListener(GameEvent.READYTOTOUCHTURRET, EnterTouch);
            Messenger.RemoveListener(GameEvent.STOPTOTOUCHTURRET, ExitTouch);
        }

        private void EnterTouch()
        {
            _isPossibleTouch = true;
        }

        private void ExitTouch()
        {
            _isPossibleTouch = false;
        }
        
        public void Update()
        {
            if (_isTouchTurret == true &&
                Input.GetKeyUp(KeyCode.E) &&
                _lastTouchTime + TouchDuration < Time.time)
            {
                _lastTouchTime = Time.time;
                
                SwapSkin();
                
                Messenger.Broadcast(GameEvent.PLAYERSTOPTOUCHTURRET);
                
                Turret.SetColliderIsTrigger(false);

                var position = transform.position;
                
                Turret.transform.position = position;
                Turret.gameObject.SetActive(true);
                
            }
            
            if (_isPossibleTouch == true &&
                _isTouchTurret == false &&
                Input.GetKeyUp(KeyCode.E) &&
                _lastTouchTime + TouchDuration < Time.time)
            {
                _lastTouchTime = Time.time;
                
                Debug.Log("Touch");
                
                Messenger.Broadcast(GameEvent.PLAYERTOUCHTURRET);
                
                Turret.SetColliderIsTrigger(true);
                Turret.transform.DOMove(transform.position, TimeToTouch)
                    .OnComplete(() =>
                    {
                        SwapSkin();
                        Turret.gameObject.SetActive(false);
                    });
            }
        }

        private void SwapSkin()
        {
            _isTouchTurret = !_isTouchTurret;
            
            PlayerImage.SetActive(!_isTouchTurret);
            PlayerWithTurretImage.SetActive(_isTouchTurret);
        }
    }
}