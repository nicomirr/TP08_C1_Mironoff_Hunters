using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game.Core;
using Game.Data;

namespace Game.UI
{
    public class UIEnemyHealth : MonoBehaviour
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
            if(_canvasGroup != null) 
                _canvasGroup.alpha = 0;

            _fillImage.fillAmount = 1;
        }

        private void OnDisable()
        {
            _healthChangeNotifier.OnHealthChanged -= UpdateHealth;
        }

        private void UpdateHealth(float currentHealth, float maxHealth)
        {
            _fillImage.fillAmount = currentHealth / maxHealth;

            if (_canvasGroup == null) return; 

            if (_displayHealthRoutine != null)
            {
                StopCoroutine(_displayHealthRoutine);
                _displayHealthRoutine = null;
            }


            _displayHealthRoutine = StartCoroutine(DisplayHealthUI());
        }

        private IEnumerator DisplayHealthUI()
        {
            _canvasGroup.alpha = 1;

            yield return new WaitForSeconds(_data.DisplayTime);

            _canvasGroup.alpha = 0;
        }
    }
}

