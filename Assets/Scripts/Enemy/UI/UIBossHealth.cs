using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Data;

namespace Game.UI
{
    public class UIBossHealth : MonoBehaviour
    {
        [SerializeField] private UIEnemyHealthDataSo _data;

        [SerializeField] private GameObject _healthChangedNotifierGameObject;

        [SerializeField] private Image _fillImage;

        private IHealthChangedNotifier _healthChangeNotifier;

        private CanvasGroup _canvasGroup;

        private Coroutine _displayHealthRoutine;

        private void Awake()
        {
            _healthChangeNotifier = _healthChangedNotifierGameObject.GetComponent<IHealthChangedNotifier>();

            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _healthChangeNotifier.OnHealthChanged += UpdateHealth;
        }

        private void Start()
        {
            _canvasGroup.alpha = 0;

            _fillImage.fillAmount = 1;
        }

        private void OnDisable()
        {
            _healthChangeNotifier.OnHealthChanged -= UpdateHealth;
        }

        

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            _fillImage.fillAmount = currentHealth / maxHealth;           
        }
       
    }

}
