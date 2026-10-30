using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Game.Data;
using Game.Marker;
using Game.Core;
using Game.Persistence;
using Game.SceneTransitions;

namespace Game.Player.Brain
{    
    [RequireComponent (typeof(Rigidbody2D))]
    [RequireComponent (typeof(PlayerInteractionController))]
        
    public class PlayerBrain : MonoBehaviour, IPlayerIdentity, IDamageProvider
    {
        [SerializeField] private PlayerBrainDataSo _data;

        [SerializeField] private PlayerDeviceManager _deviceManager;

        private PlayerInputController _inputController;
        private PlayerAnimationsController _animationsController;
        private PlayerLocomotionController _locomotionController;
        private PlayerInteractionController _interactionController;
        private PlayerCurrencyController _currencyController;
        private PlayerInventoryController _inventoryController;
        private PlayerCombatController _combatController;
        private PlayerHealthController _healthController;
        private PlayerDamageController _damageController;

        public PlayerCurrencyController CurrencyController => _currencyController;
        public PlayerInventoryController InventoryController => _inventoryController;

        public event Action<InputDevice> OnCurrentDeviceChanged;
        public event Action<InputDevice> OnInteractableInRangeReceived;
        public event Action OnInteractableOutOfRangeReceived;
        public event Action<int> OnCoinsValueChanged;
        public event Action<Sprite, int> OnSelectedSlotChanged;
        public event Action OnInventoryEmptied;
        public event Action<int> OnHealthChanged;

        public PlayerType PlayerType => _data.InputData.playerType;
        public int Damage => _combatController.Damage;


        private void Awake()
        {
            _inputController = new PlayerInputController(_data.InputData);

            _animationsController = new PlayerAnimationsController(_data.AnimationsData, GetComponentInChildren<Animator>());
            
            _locomotionController = new PlayerLocomotionController(_data.LocomotionData, GetComponent<Rigidbody2D>(), this.transform,
                GetComponentInChildren<GroundCheckMarker>().transform);

            _interactionController = GetComponent<PlayerInteractionController>();            
            
            _combatController = new PlayerCombatController(_data.CombatData, 
                GetComponentInChildren<PlayerAttackAreaMarker>().GetComponent<Collider2D>());

            if(GameSessionManager.Instance.HasSavedState)
                LoadSessionData();
            else
                InitializeDefaultData();
        }

        private void OnEnable()
        {
            SceneTransitionManager.OnSceneTransitionStarted += SaveSessionDataAndDisable;

            _deviceManager.OnPlayerDeviceReconnected += _inputController.PlayerInputs.ReconnectGamepad;

            _deviceManager.OnKeyboardKeyPressed += _inputController.PlayerInputs.IsPlayerKeyboardKey;

            _inputController.PlayerInputs.OnKeyboardValidated += _deviceManager.ConfirmKeyboardConnection;

            _locomotionController.OnJustLanded += HandleLand;

            _inputController.PlayerInputs.OnCurrentDeviceChanged += NotifyDeviceChanged;

            _interactionController.OnInteractableInRange += NotifyInteractableInRange;
            _interactionController.OnInteractableOutOfRange += NotifyInteractableOutOfRange;

            _currencyController.OnCoinsValueChanged += NotifyCoinsValueChanged;

            _inventoryController.OnSelectedSlotChanged += NotifySelectedSlotChanged;

            _inventoryController.OnInventoryEmptied += NotifyInventoryEmptied;

            _healthController.OnHealthChanged += NotifyHealthChanged;
           
        }

        private void Start()
        {
            _currencyController.RaiseCoinsValueChanged();
            _healthController.RaiseHealhChanged();
            _inventoryController.RaiseSelectedSlotChanged();
        }

        private void FixedUpdate()
        {
            if (_interactionController.IsInteracting)
                return;            

            _locomotionController.UpdateMovement(_inputController.PlayerInputs.Direction);
        }

        private void Update()
        {
            if (_interactionController.IsInteracting)
            {
                _locomotionController.StopMovement();
                _animationsController.SetMoveAnimation(false);
                _animationsController.SetJumpAnimation(false);
                return;
            }

            _animationsController.SetMoveAnimation(_inputController.PlayerInputs.Direction != 0);
            
            HandleJumpUpdate();
            HandleAttack();
            HandleInteractions();
            HandleInventoryInputs();
        }

        private void OnDisable()
        {
            SceneTransitionManager.OnSceneTransitionStarted -= SaveSessionDataAndDisable;

            _deviceManager.OnPlayerDeviceReconnected -= _inputController.PlayerInputs.ReconnectGamepad;
            _deviceManager.OnKeyboardKeyPressed -= _inputController.PlayerInputs.IsPlayerKeyboardKey;

            _inputController.PlayerInputs.OnKeyboardValidated -= _deviceManager.ConfirmKeyboardConnection;

            _locomotionController.OnJustLanded -= HandleLand;

            _inputController.PlayerInputs.OnCurrentDeviceChanged -= NotifyDeviceChanged;

            _interactionController.OnInteractableInRange -= NotifyInteractableInRange;
            _interactionController.OnInteractableOutOfRange -= NotifyInteractableOutOfRange;

            _currencyController.OnCoinsValueChanged -= NotifyCoinsValueChanged;

            _inventoryController.OnSelectedSlotChanged -= NotifySelectedSlotChanged;

            _inventoryController.OnInventoryEmptied -= NotifyInventoryEmptied;

            _healthController.OnHealthChanged -= NotifyHealthChanged;
        }

        private void InitializeDefaultData()
        {
            _currencyController = new PlayerCurrencyController(_data.CurrencyData);

            _inventoryController = new PlayerInventoryController();

            _healthController = new PlayerHealthController(_data.HealthData);
        }

        private void LoadSessionData()
        {
            PlayerSessionData data = new();

            if (PlayerType == PlayerType.Player1)
                data = GameSessionManager.Instance.Player1Data;
            else
                data = GameSessionManager.Instance.Player2Data;

            _currencyController = new PlayerCurrencyController(data);

            _inventoryController = new PlayerInventoryController(data);

            _healthController = new PlayerHealthController(data);
        }

        private void SaveSessionDataAndDisable()
        {
            PlayerSessionData data = new PlayerSessionData(_healthController.CurrentHealth, _healthController.MaxHealth, _currencyController.CurrentCoins,
                _inventoryController.InventorySlots, _inventoryController.CurrentSlot);

            GameSessionManager.Instance.SavePlayerData(data, PlayerType);

            this.gameObject.SetActive(false);
        }

        private void HandleJumpUpdate()
        {
            _locomotionController.UpdateCoyoteTime();
            _locomotionController.UpdateGravity();
            _locomotionController.UpdateGroundState();

            if (_inputController.PlayerInputs.JumpReleased)
                _locomotionController.CutJump();

            if (_inputController.PlayerInputs.JumpPressed)
            {
                bool jumpPerformed = _locomotionController.HandleJump();
                _animationsController.SetJumpAnimation(jumpPerformed);                
            }
        }

        private void HandleLand()
        {
            _locomotionController.HandleLand();
            _animationsController.SetJumpAnimation(false);
        }

        private void HandleAttack()
        {
            _combatController.UpdateAttackCooldown();

            if (!_combatController.CanAttack) return;

            if (_inputController.PlayerInputs.AttackPressed)
            {
                StartCoroutine(_combatController.AttackRoutine());
                _animationsController.TriggerAttack();
            }
        }

        private void HandleInteractions()
        {           
            if (_inputController.PlayerInputs.InteractPressed)
                _interactionController.Interact(_inputController.PlayerInputs.InteractAction);
        }

        private void HandleInventoryInputs()
        {
            if(_inputController.PlayerInputs.NextItemPressed)
                _inventoryController.SelectNextInventorySlot(); 

            else if(_inputController.PlayerInputs.PreviousItemPressed)
                _inventoryController.SelectPreviousInventorySlot();

            if (_inputController.PlayerInputs.UseItemPressed)
                _inventoryController.UseCurrentSlotItem();
        }

        private void NotifyDeviceChanged(InputDevice device)
        {
            OnCurrentDeviceChanged?.Invoke(device);
        }

        private void NotifyInteractableInRange()
        {
            OnInteractableInRangeReceived?.Invoke(_inputController.PlayerInputs.CurrentDevice);
        }

        private void NotifyInteractableOutOfRange()
        {
            OnInteractableOutOfRangeReceived?.Invoke();
        }       

        private void NotifyCoinsValueChanged(int value) 
        {
            OnCoinsValueChanged?.Invoke(value);
        }

        private void NotifySelectedSlotChanged(Sprite image, int quantity)
        {
            OnSelectedSlotChanged?.Invoke(image, quantity);
        }

        private void NotifyInventoryEmptied()
        {
            OnInventoryEmptied?.Invoke();
        }

        private void NotifyHealthChanged(int value)
        {
            OnHealthChanged?.Invoke(value);
        }
    }
}

