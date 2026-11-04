using UnityEngine;
using System.Collections;
using Game.Core;
using Game.ObjectPooling;

namespace Game.Player
{
    public class HealEffect : ItemEffect
    {
        private readonly PlayerHealthController _healthController;
        private readonly Transform _userTransform;

        public HealEffect(PlayerHealthController healtController, Transform userTransform)
        {
            _healthController = healtController;
            _userTransform = userTransform;
        }

        public override IEnumerator UseItemRoutine()
        {
            _healthController.Heal();
            GameObject healEffect = ObjectPool.Instance.GetPoolObject(PoolObjectType.HealEffect);

            healEffect.transform.position = this._userTransform.position;
            healEffect.SetActive(true);

            ParticleSystem healEffectParticles = healEffect.GetComponent<ParticleSystem>();
            healEffectParticles.Play();

            while(healEffectParticles.isPlaying)
            {
                healEffect.transform.position = _userTransform.position;
                yield return null;
            }
        }
    }

}
