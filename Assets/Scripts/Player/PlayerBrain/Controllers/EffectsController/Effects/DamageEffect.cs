using UnityEngine;
using System.Collections;
using Game.Core;
using Game.ObjectPooling;
using Game.Data;

namespace Game.Player
{
    public class DamageEffect : ItemEffect
    {
        private readonly DamageEffectDataSo _data;

        private readonly PlayerCombatController _combatController;
        private readonly Transform _userTransform;

        public DamageEffect(DamageEffectDataSo data, PlayerCombatController combatController, Transform userTransform)
        {
            _data = data;

            _combatController = combatController;
            _userTransform = userTransform;
        }

        public override IEnumerator UseItemRoutine()
        {
            ObjectPool.Instance.DisableDamageEffects();           

            GameObject damageEffect = ObjectPool.Instance.GetPoolObject(_data.PoolObjectType);

            damageEffect.transform.position = _userTransform.position;
            damageEffect.SetActive(true);

            ParticleSystem damageEffectParticles = damageEffect.GetComponent<ParticleSystem>();

            damageEffectParticles.Play();

            _combatController.ChangeDamageType(_data.DamageType);

            float totalTime = _data.Time;

            while (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                damageEffectParticles.transform.position = new Vector2(_userTransform.position.x, _userTransform.position.y + _data.YOffset);

                yield return null;
            }

            _combatController.ChangeDamageType(DamageType.Normal);

            damageEffectParticles.transform.position = _userTransform.position;
            damageEffectParticles.Stop();
            damageEffectParticles.gameObject.SetActive(false);
        }
    }

}
