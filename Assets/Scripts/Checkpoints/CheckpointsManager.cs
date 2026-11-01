using Game.Animations;
using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Checkpoints
{
    public class CheckpointsManager : MonoBehaviour
    {
        [SerializeField] List<Checkpoint> _checkpoints;
        [SerializeField] private List<GameObject> _checkpointFlashes;
        
        private int _currentCheckpoint;

        private void OnEnable()
        {
            Checkpoint.OnCheckpointUsed += UpdateCurrentCheckpoint;            
        }

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= UpdateCurrentCheckpoint;
        }

        private void UpdateCurrentCheckpoint(int checkpointNumber)
        {
            _currentCheckpoint = checkpointNumber;

            foreach (Checkpoint checkpoint in _checkpoints)
            {
                if (checkpoint.CheckpointNumber != checkpointNumber)
                    checkpoint.Reset();
            }

            StartCoroutine(DisplayFlashRoutine());
        }

        private IEnumerator DisplayFlashRoutine()
        {
            foreach (GameObject checkPointFlash in _checkpointFlashes)
                checkPointFlash.SetActive(true);

            yield return new WaitForSeconds(1f);

            foreach (GameObject checkPointFlash in _checkpointFlashes)
                checkPointFlash.SetActive(false);
        }

    }

}
