using Game.Enemy;
using UnityEngine;

namespace Game.UI
{
    public class UIBossDefeatedWhiteScreen : MonoBehaviour
    {
        private readonly int WIN_FLASH_TRIGGER_HASH = Animator.StringToHash("WinFlash");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            BossBrain.OnBossDefeated += DisplayFlash;
        }

        private void OnDisable()
        {
            BossBrain.OnBossDefeated -= DisplayFlash;
        }

        private void DisplayFlash()
        {
            _animator.SetTrigger(WIN_FLASH_TRIGGER_HASH);
        }

    }

}

