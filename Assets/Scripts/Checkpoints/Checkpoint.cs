using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using TMPro;
using System;
using Game.Animations;
using Game.Core;
using Game.Data;
using Game.Persistence;
using Game.SceneTransitions;

namespace Game.Checkpoints
{
    public class Checkpoint : MonoBehaviour, IInteractable
    {
        [SerializeField] private CheckpointDataSo _data;

        [SerializeField] private int _checkpointNumber;

        [SerializeField] private Transform _spriteTransform;

        [SerializeField] private TMP_Text _textbox;

        public int CheckpointNumber => _checkpointNumber;

        private AnimationsController _animationsController;

        private Light2D _light;

        private AudioSource _audioSource;

        private bool _canInteract;
        public bool CanInteract => _canInteract;

        public bool HasInteractorInRange { get; set; }

        public static event Action<int, bool, PlayerType> OnCheckpointUsed;
                
        private bool _checkpointReached;
        public bool CheckpointReached => _checkpointReached;    


        private void Awake()
        {
            _animationsController = new AnimationsController(_data.AnimationData, GetComponentInChildren<Animator>(), AnimationStateType.Disabled);
            _light = GetComponentInChildren<Light2D>();
            _audioSource = GetComponent<AudioSource>();

            if (GameSessionManager.Instance.HasSavedState)
                LoadSessionData();
        }

        private void OnEnable()
        {
            SceneTransitionManager.OnSceneTransitionStarted += SaveSessionData;
        }

        private void Start()
        {
            _canInteract = true;

            if(_checkpointReached)
            {
                _animationsController.ChangeAnimationState(AnimationStateType.Enabled);
                _light.enabled = true;
            }

        }

        private void Update()
        {
            if (GameMode.CurrentGameModeType == GameModeType.SinglePlayer)
            {
                if (PlayerNearby())
                    _textbox.text = _data.AvailableCheckpointText;
                else
                    _textbox.text = "";

                return;
            }

            if (PlayerNearby())
            {
                if (BothPlayersNearby())
                    _textbox.text = _data.AvailableCheckpointText;
                else
                    _textbox.text = _data.UnavailableCheckpointText;
            }
            else
                _textbox.text = "";
        }

        private void OnDisable()
        {
            SceneTransitionManager.OnSceneTransitionStarted -= SaveSessionData;
        }

        private void LoadSessionData()
        {
            CheckpointSessionData data = new();

            switch (_checkpointNumber)
            {
                case 1:

                    data = GameSessionManager.Instance.Checkpoint1Data;
                    break;

                case 2:

                    data = GameSessionManager.Instance.Checkpoint2Data;
                    break;

                case 3:

                    data = GameSessionManager.Instance.Checkpoint3Data;
                    break;

                case 4:

                    data = GameSessionManager.Instance.Checkpoint4Data;
                    break;

                case 5:

                    data = GameSessionManager.Instance.Checkpoint5Data;
                    break;

                case 6:

                    data = GameSessionManager.Instance.Checkpoint6Data;
                    break;

                case 7:

                    data = GameSessionManager.Instance.Checkpoint7Data;
                    break;
            }

            _checkpointReached = data.CheckpointReached;

        }

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {
            if (GameMode.CurrentGameModeType == GameModeType.Coop && !BothPlayersNearby())
            {
                HasInteractorInRange = false;
                return;
            }

            _audioSource.Play();

            OnCheckpointUsed?.Invoke(_checkpointNumber, _checkpointReached, playerType);

            _checkpointReached = true;

            _animationsController.ChangeAnimationState(AnimationStateType.Enabled);
            _light.enabled = true;

        }

        private bool PlayerNearby()
        {
            return Physics2D.OverlapCircle(_spriteTransform.position, _data.InteractionRadius, _data.PlayerLayer) != null;
        }

        private bool BothPlayersNearby()
        {
            Collider2D[] players = Physics2D.OverlapCircleAll(this.transform.position, _data.CloseEnoughRadius, _data.PlayerLayer);

            return players.Length >= 2;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_spriteTransform.position, _data.InteractionRadius);
        }

        private void SaveSessionData()
        {
            CheckpointSessionData data =  new CheckpointSessionData(_checkpointReached);

            GameSessionManager.Instance.SaveCheckpointData(data, _checkpointNumber);
        }
    }
}
