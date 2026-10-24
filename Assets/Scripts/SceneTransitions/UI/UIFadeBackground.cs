using UnityEngine;

namespace Game.UI
{
    public class UIFadeBackground : MonoBehaviour
    {
        private static readonly int FADE_OUT_TRIGGER = Animator.StringToHash("fadeOut");
        private static readonly int FADE_IN_TRIGGER = Animator.StringToHash("fadeIn");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }                
        
        public void FadeIn()
        {
            _animator.SetTrigger(FADE_IN_TRIGGER);
        }

        public void FadeOut()
        {
            _animator.SetTrigger(FADE_OUT_TRIGGER);
        }
    }
}

