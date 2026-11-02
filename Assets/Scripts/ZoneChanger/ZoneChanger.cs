using UnityEngine;
using UnityEngine.InputSystem;
using Game.Data;
using Game.Core;
using TMPro;

namespace Game.SceneTransitions
{
    public class ZoneChanger : MonoBehaviour, IInteractable
    {
        [SerializeField] private ZoneChangerDataSo _data;

        [SerializeField] private SceneTransitionManager _sceneTransitionManager;

        private TMP_Text _textbox;

        private bool _canInteract;
        public bool CanInteract => _canInteract;

        public bool HasInteractorInRange { get; set; }

        private void Awake()
        {
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
                    _textbox.text = _data.AvailableTransitionText;
                else
                    _textbox.text = "";

                return;
            }

            if (PlayerNearby())
            {
                if (BothPlayersNearby())
                    _textbox.text = _data.AvailableTransitionText;
                else
                    _textbox.text = _data.UnavailableTransitionText;
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

            _canInteract = false;

            _sceneTransitionManager.ChangeScene(_data.SceneToLoad);
        }

        private bool PlayerNearby()
        {
            return Physics2D.OverlapCircle(this.transform.position, _data.InteractionRadius, _data.PlayerLayer) != null;
        }

        private bool BothPlayersNearby()
        {
            Collider2D[] players = Physics2D.OverlapCircleAll(this.transform.position, _data.CloseEnoughRadius, _data.PlayerLayer);

            return players.Length >= 2;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, _data.InteractionRadius);
        }

    }
}

