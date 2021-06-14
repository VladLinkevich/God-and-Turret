using System;
using Room;
using UnityEngine;

namespace UI
{
    public class TutorialHandler : MonoBehaviour
    {
        public GameObject Tutorial;
        
        private void OnEnable()
        {            
            Messenger<Direction>.AddListener(GameEvent.STARTCHANGEROOM, CloseTutorial);
        }

        private void OnDisable()
        {
            Messenger<Direction>.RemoveListener(GameEvent.STARTCHANGEROOM, CloseTutorial);
        }

        private void CloseTutorial(Direction trash)
        {
            Tutorial.SetActive(false);
        }
    }
}
