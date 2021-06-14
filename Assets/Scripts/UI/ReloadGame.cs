using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ReloadGame : MonoBehaviour
    {
        private void OnEnable()
        {
            Messenger.AddListener(GameEvent.GAMEOVER, ShowButton);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener(GameEvent.GAMEOVER, ShowButton);
        }

        private void ShowButton()
        {
            GetComponent<Image>().enabled = true;
            Time.timeScale = 0f;
        }
        
        public void ReloadScene()
        {
            Time.timeScale = 1f;
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
