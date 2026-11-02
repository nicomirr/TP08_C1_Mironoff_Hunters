using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using System;
using Game.Core;
using Game.Data;
using Game.Animations;
using TMPro;

namespace Game.Checkpoints
{
    public class Checkpoint : MonoBehaviour, IInteractable
    {
        [SerializeField] private CheckpointDataSo _data;

        [SerializeField] private int _checkpointNumber;

        [SerializeField] private Transform _spriteTransform;

        private TMP_Text _textbox;

        public int CheckpointNumber => _checkpointNumber;

        private AnimationsController _animationsController;

        private Light2D _light;

        private bool _canInteract;
        public bool CanInteract => _canInteract;

        public bool HasInteractorInRange { get; set; }

        public static event Action<int> OnCheckpointUsed;


        private void Awake()
        {
            _animationsController = new AnimationsController(_data.AnimationData, GetComponentInChildren<Animator>(), AnimationStateType.Disabled);
            _light = GetComponentInChildren<Light2D>();
            _textbox = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _canInteract = true;
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

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {
            if (GameMode.CurrentGameModeType == GameModeType.Coop && !BothPlayersNearby())
            {
                HasInteractorInRange = false;
                return;
            }

            OnCheckpointUsed?.Invoke(_checkpointNumber);

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

        public void Reset()
        {
            _animationsController.ChangeAnimationState(AnimationStateType.Disabled);
            _light.enabled = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_spriteTransform.position, _data.InteractionRadius);
        }

    }

}
