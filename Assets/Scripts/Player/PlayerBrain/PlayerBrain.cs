using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using Game.Data;
using Game.Marker;
using Game.Core;
using Game.Persistence;
using Game.SceneTransitions;
using Game.Animations;
using Game.Inputs;
using Game.Checkpoints;
using Game.Pickables;
using Game.Environment;

namespace Game.Player.Brain
{    
    [RequireComponent (typeof(Rigidbody2D))]
    [RequireComponent (typeof(PlayerInteractionController))]
        
    public class PlayerBrain : MonoBehaviour, IPlayerIdentity, ICoroutineRunner, IMovementProviderHolder
    {
        [SerializeField] private PlayerBrainDataSo _data;

        [SerializeField] private PlayerDeviceManager _deviceManager;

        private AnimationsController _animationsController;

        private PlayerInputController _inputController;
        private PlayerLocomotionController _locomotionController;
        private PlayerInteractionController _interactionController;
        private PlayerCurrencyController _currencyController;
        private PlayerInventoryController _inventoryController;
        private PlayerCombatController _combatController;
        private PlayerHealthController _healthController;
        private PlayerDamageFeedbackController _damageFeedbackController;
        private PlayerEffectsController _effectsController;

        private PlayerPickablesHandler _pickablesHandler;

        public PlayerCurrencyController CurrencyController => _currencyController;
        public PlayerInventoryController InventoryController => _inventoryController;

        public event Action<InputDevice> OnCurrentDeviceChanged;
        public event Action<InputDevice> OnInteractableInRangeReceived;
        public event Action OnInteractableOutOfRangeReceived;
        public event Action<int> OnCoinsValueChanged;
        public event Action<Sprite, int> OnSelectedSlotChanged;
        public event Action OnInventoryEmptied;
        public event Action<int> OnHealthChanged;
        
        public IMovementProvider MovementProvider => _locomotionController.MovementProvider;
        public PlayerType PlayerType => _data.InputData.playerType;
        public bool IsAlive => _healthController.IsAlive;


        private Vector2 _initialPos;

        private void Awake()
        {
            InitializeNonPersistentData();

            if(GameSessionManager.Instance.HasSavedState)
                LoadSessionData();
            else
                InitializeDefaultData();

            _effectsController = new PlayerEffectsController(_data.EffectsData, this, _healthController, _combatController, this.transform, 
                GetComponent<Rigidbody2D>());

            _pickablesHandler = new PlayerPickablesHandler(_currencyController);
        }

        private void OnEnable()
        {
            SceneTransitionManager.OnSceneTransitionStarted += SaveSessionDataAndDisable;
            Checkpoint.OnCheckpointUsed += _healthController.RestoreFullHealth;

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

            _inventoryController.OnItemUsed += _effectsController.HandleItemUsed;

            _healthController.OnHealthChanged += NotifyHealthChanged;           
        }

        private void Start()
        {
            _initialPos = this.transform.position;

            _currencyController.RaiseCoinsValueChanged();
            _healthController.RaiseHealhChanged();
            _inventoryController.RaiseSelectedSlotChanged();
        }

        private void FixedUpdate()
        {
            if (!_healthController.IsAlive) return;

            if (_interactionController.IsInteracting || _damageFeedbackController.IsKnockedback) return;            

            _locomotionController.UpdateMovement(_inputController.PlayerInputs.Direction);
        }

        private void Update()
        {
            if (!_healthController.IsAlive)
            {
                _locomotionController.StopMovement();
                _animationsController.ChangeAnimationState(AnimationStateType.Dead);
                return;
            }

            if (_interactionController.IsInteracting)
            {
                _locomotionController.StopMovement();
                _animationsController.ChangeAnimationState(Core.AnimationStateType.Idle);

                return;
            }

            HandleGroundedState();
            HandleJumpUpdate();
            HandleAttack();
            HandleInteractions();
            HandleInventoryInputs();
            HandleAnimations();
        }       

        private void OnDisable()
        {
            SceneTransitionManager.OnSceneTransitionStarted -= SaveSessionDataAndDisable;
            Checkpoint.OnCheckpointUsed -= _healthController.RestoreFullHealth;

            _inputController.PlayerInputs.Deinitialize();

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

            _inventoryController.OnItemUsed -= _effectsController.HandleItemUsed;

            _healthController.OnHealthChanged -= NotifyHealthChanged;
        }

        private void InitializeNonPersistentData()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();   

            _animationsController = new AnimationsController(_data.AnimationsData, GetComponentInChildren<Animator>(), AnimationStateType.Idle);

            _inputController = new PlayerInputController(_data.InputData);

            _locomotionController = new PlayerLocomotionController(_data.LocomotionData, rb, this.transform,
                GetComponentInChildren<GroundCheckMarker>().transform);

            _interactionController = GetComponent<PlayerInteractionController>();

            _combatController = new PlayerCombatController(_data.CombatData, GetComponentInChildren<PlayerAttackArea>());

            _damageFeedbackController = new PlayerDamageFeedbackController(_data.DamageHandlerData, rb, GetComponentInChildren<PlayerImageMarker>().GetComponent<SpriteRenderer>());
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

        private void HandleGroundedState()
        {
            _locomotionController.UpdateGroundState();
            _locomotionController.UpdateLocomotionBySurfaceType();
        }

        private void HandleJumpUpdate()
        {
            _locomotionController.UpdateCoyoteTime();
            _locomotionController.UpdateGravity();

            if (_inputController.PlayerInputs.JumpReleased)
                _locomotionController.CutJump();

            if (_inputController.PlayerInputs.JumpPressed)
            {
                bool jumpPerformed = _locomotionController.HandleJump();            
            }
        }

        private void HandleLand()
        {
            _locomotionController.HandleLand();
        }

        private void HandleAttack()
        {
            _combatController.UpdateAttackCooldown();

            if (!_combatController.CanAttack) return;

            if (_inputController.PlayerInputs.AttackPressed)            
                StartCoroutine(_combatController.AttackRoutine());            
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
                   
        private void HandleAnimations()
        {
            if (_combatController.IsAttacking)
            {
                _animationsController.ChangeAnimationState(Core.AnimationStateType.Attacking);
                return;
            }

            if (!_locomotionController.IsGrounded)
            {
                _animationsController.ChangeAnimationState(Core.AnimationStateType.Jumping);
                return;
            }

            if (_inputController.PlayerInputs.Direction != 0)
                _animationsController.ChangeAnimationState(Core.AnimationStateType.Moving);
            else
                _animationsController.ChangeAnimationState(Core.AnimationStateType.Idle);                
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

        public void ResetPlayer(bool movedToCheckpoint)
        {
            if (!movedToCheckpoint)
                this.transform.position = _initialPos;

            _healthController.Reset();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent(out IMovementProvider movementProvider))
                _locomotionController.HandleOutsideMovement(movementProvider);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IMovementProvider _))
                _locomotionController.StopOutsideMovement();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_healthController.IsAlive) return;

            if (collision.TryGetComponent(out Pickable pickable))
            {
                _pickablesHandler.HandleCollectedPickable(pickable.PickableType, pickable.Amount);
                pickable.gameObject.SetActive(false);
            }

            if (collision.TryGetComponent(out ThornTileMarker _))
            {
                _healthController.Die();

            }                    

            if (!collision.TryGetComponent(out EnemyAttackAreaMarker _) && !collision.TryGetComponent(out ThornTileMarker _)) return;
            
            if(collision.TryGetComponent(out EnemyAttackAreaMarker _))
                _healthController.TakeDamage();
            else
                _healthController.Die();

            float hitDirection = this.transform.position.x - collision.transform.position.x;

            if(!_damageFeedbackController.IsKnockedback)
                StartCoroutine(_damageFeedbackController.ApplyKnockbackRoutine(hitDirection));

            if (_healthController.IsInvulnerable) return;

            if(!_damageFeedbackController.IsFlickering)
                StartCoroutine(_damageFeedbackController.FlickRoutine);

        }
        public Coroutine RunRoutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}

