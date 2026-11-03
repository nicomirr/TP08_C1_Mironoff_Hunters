using UnityEngine;
using System.Collections;
using Game.Core;
using Game.ObjectPooling;

namespace Game.Player
{
    public class PoisonDamageEffect : ItemEffect
    {
        private readonly PlayerCombatController _combatController;
        private readonly Transform _userTransform;

        private readonly float _time;
        private readonly float _yOffset;

        public PoisonDamageEffect(PlayerCombatController combatController, Transform userTransform, float time, float yOffset)
        {
            _combatController = combatController;
            _userTransform = userTransform;

            _time = time;
            _yOffset = yOffset;
        }

        public override IEnumerator UseItemRoutine()
        {
            GameObject posionDamageEffect = ObjectPool.Instance.GetPoolObject(PoolObjectType.PoisonDamageEffect);

            posionDamageEffect.transform.position = _userTransform.position;
            posionDamageEffect.SetActive(true);

            ParticleSystem fireDamageEffectParticles = posionDamageEffect.GetComponent<ParticleSystem>();

            fireDamageEffectParticles.Play();

            _combatController.ChangeDamageType(DamageType.Poison);

            float totalTime = _time;

            while (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                fireDamageEffectParticles.transform.position = new Vector2(_userTransform.position.x, _userTransform.position.y + _yOffset);

                yield return null;
            }

            _combatController.ChangeDamageType(DamageType.Normal);

            fireDamageEffectParticles.transform.position = _userTransform.position;
            fireDamageEffectParticles.Stop();
            fireDamageEffectParticles.gameObject.SetActive(false);
        }
    }

}
