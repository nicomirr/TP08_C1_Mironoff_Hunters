using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Checkpoints;
using Game.Player.Brain;

namespace Game.Respawn
{
    public class PlayerRespawnManager : MonoBehaviour
    {
        [SerializeField] private CheckpointsManager _checkpointsManager;

        [SerializeField] private List<GameObject> _fadeScreens;

        [SerializeField] private PlayerBrain _player;

        private Coroutine _respawnRoutine;

        private void Update()
        {
            if (_player.IsAlive) return;

            if (_respawnRoutine == null)
                _respawnRoutine = StartCoroutine(RespawnPlayerRoutine());
        }

        private IEnumerator RespawnPlayerRoutine()
        {
            yield return new WaitForSeconds(1f);

            foreach (GameObject fadeScreen in _fadeScreens)
                fadeScreen.SetActive(true);

            yield return new WaitForSeconds(0.7f);

            if (_checkpointsManager.CurrentCheckpointNumber == 0)
            {
                _player.ResetPlayer(false);

                foreach (GameObject fadeScreen in _fadeScreens)
                    fadeScreen.SetActive(false);

                _respawnRoutine = null;

                yield break;
            }

            List<Checkpoint> checkpoints = _checkpointsManager.Checkpoints;

            foreach (Checkpoint checkpoint in checkpoints)
            {
                if(checkpoint.CheckpointNumber == _checkpointsManager.CurrentCheckpointNumber)
                {
                    _player.transform.position = checkpoint.transform.position;
                    break;
                }
            }

            _player.ResetPlayer(true);

            yield return new WaitForSeconds(0.5f);

            foreach (GameObject fadeScreen in _fadeScreens)
                fadeScreen.SetActive(false);

            _respawnRoutine = null;
        }
    }
}

