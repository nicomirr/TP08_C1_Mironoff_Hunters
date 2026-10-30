using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.UI;

namespace Game.SceneTransitions
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _sceneObjectsToDisableOnTransition = new List<GameObject>();

        [SerializeField] private UIFadeBackground _fadeBackground;

        private TransitionObjectsDisabler _transitionObjectsDisabler;

        public static event Action OnSceneTransitionStarted;

        private void Awake()
        {
            _transitionObjectsDisabler = new TransitionObjectsDisabler();
        }        

        public void ChangeScene(SceneToLoadDataSo sceneToLoad)
        {
            OnSceneTransitionStarted?.Invoke();
            StartCoroutine(ChangeSceneRoutine(sceneToLoad.sceneToLoadName, sceneToLoad.TransitionTime));
        }

        private IEnumerator ChangeSceneRoutine(string sceneName, float transitionTime)
        {
            Time.timeScale = 1f;

            _fadeBackground.FadeOut();
            _transitionObjectsDisabler.DisableObjects(_sceneObjectsToDisableOnTransition);
                        
            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}

