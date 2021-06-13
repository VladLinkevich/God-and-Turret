using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Room
{
    public class Room : MonoBehaviour
    {
        [FormerlySerializedAs("SpawnPosirion")] public List<Transform> SpawnPosition; 
        
        public GameObject DoorUp;
        public GameObject DoorDown;
        public GameObject DoorLeft;
        public GameObject DoorRight;

        public float durationCloseDoor = 1f;
        public Ease ease = Ease.Linear;
        public bool isAttacked = true;
        
        private bool[] _openDoor = new bool[4];

        public void CloseDoor()
        {
            _openDoor[0] = DoorUp.gameObject.activeSelf;
            _openDoor[1] = DoorDown.gameObject.activeSelf;
            _openDoor[2] = DoorLeft.gameObject.activeSelf;
            _openDoor[3] = DoorRight.gameObject.activeSelf;

            SelectDoorForClose();
        }

        public void OpenDoor()
        {
            if (_openDoor[0] == false)
            {
                var p = DoorUp.transform.position;
                var offset = Vector3.up * 1f;

                DoorUp.transform.DOMove(p + offset, durationCloseDoor)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        DoorUp.SetActive(false);
                        DoorUp.transform.position = p;
                    });
            }

            if (_openDoor[1] == false)
            {
                var p = DoorDown.transform.position;
                var offset = Vector3.down * 1f;

                DoorDown.transform.DOMove(p + offset, durationCloseDoor)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        DoorDown.SetActive(false);
                        DoorDown.transform.position = p;
                    });
            }

            if (_openDoor[2] == false)
            {
                var p = DoorLeft.transform.position;
                var offset = Vector3.left * 1f;

                DoorLeft.transform.DOMove(p + offset, durationCloseDoor)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        DoorLeft.SetActive(false);
                        DoorLeft.transform.position = p;
                    });
            }

            if (_openDoor[3] == false)
            {
                var p = DoorRight.transform.position;
                var offset = Vector3.right * 1f;

                DoorRight.transform.DOMove(p + offset, durationCloseDoor)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        DoorRight.SetActive(false);
                        DoorRight.transform.position = p;
                    });
            }
        }
        
        private void SelectDoorForClose()
        {
            if (_openDoor[0] == false)
            {
                var p = DoorUp.transform.position;
                var offset = Vector3.up * 2f;

                DoorUp.SetActive(true);
                DoorUp.transform.position = p + offset;

                DoorUp.transform.DOMove(p, durationCloseDoor)
                    .SetEase(ease);
            }

            if (_openDoor[1] == false)
            {
                var p = DoorDown.transform.position;
                var offset = Vector3.down * 2f;

                DoorDown.SetActive(true);
                DoorDown.transform.position = p + offset;

                DoorDown.transform.DOMove(p, durationCloseDoor)
                    .SetEase(ease);
            }

            if (_openDoor[2] == false)
            {
                var p = DoorLeft.transform.position;
                var offset = Vector3.left * 2f;

                DoorLeft.SetActive(true);
                DoorLeft.transform.position = p + offset;

                DoorLeft.transform.DOMove(p, durationCloseDoor)
                    .SetEase(ease);
            }

            if (_openDoor[3] == false)
            {
                var p = DoorRight.transform.position;
                var offset = Vector3.right * 2f;

                DoorRight.SetActive(true);
                DoorRight.transform.position = p + offset;

                DoorRight.transform.DOMove(p, durationCloseDoor)
                    .SetEase(ease);
            }
        }
    }
}