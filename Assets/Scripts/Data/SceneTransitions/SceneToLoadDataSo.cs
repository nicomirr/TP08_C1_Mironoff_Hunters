using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "SceneToLoadData", menuName = "Scriptable Objects/SceneToLoadData")]
    public class SceneToLoadDataSo : ScriptableObject
    {
        [SerializeField] private string _sceneToLoadName;
        public string sceneToLoadName => _sceneToLoadName;

        [SerializeField] private float _transitionTime;
        public float TransitionTime => _transitionTime;
    }
}


