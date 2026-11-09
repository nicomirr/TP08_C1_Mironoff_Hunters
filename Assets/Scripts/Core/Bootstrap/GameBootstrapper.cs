using UnityEngine;
using UnityEngine.SceneManagement;
using Game.UI;
using Game.Audio;
using Game.Settings;

namespace Game.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameAudioSettings _audioSettings;

        private static GameBootstrapper _instance;

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            _audioSettings = GetComponentInChildren<GameAudioSettings>();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach (GameObject root in scene.GetRootGameObjects())
            {
                AudioManager[] audioManagers = root.GetComponentsInChildren<AudioManager>(true);

                foreach (AudioManager audioManager in audioManagers)
                {
                    audioManager.Initialize(_audioSettings);
                }

                UIVolumeButtons[] volumeButtons = root.GetComponentsInChildren<UIVolumeButtons>(true);

                foreach (UIVolumeButtons volumeButton in volumeButtons)
                {
                    volumeButton.Initialize(_audioSettings);
                }                
            }            
        }
    }
}
