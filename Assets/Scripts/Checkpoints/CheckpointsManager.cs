using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Data;

namespace Game.Checkpoints
{
    public class CheckpointsManager : MonoBehaviour
    {
        [SerializeField] private CheckpointManagerDataSo _data;

        [SerializeField] private List<Checkpoint> _checkpoints;
        public List<Checkpoint> Checkpoints => _checkpoints;

        [SerializeField] private List<GameObject> _checkpointFlashes;
        
        private int _currentCheckpointNumber;
        public int CurrentCheckpointNumber => _currentCheckpointNumber;

        private void OnEnable()
        {
            Checkpoint.OnCheckpointUsed += UpdateCurrentCheckpoint;            
        }

        private void Start()
        {
            _currentCheckpointNumber = 0;
        }

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= UpdateCurrentCheckpoint;
        }

        private void UpdateCurrentCheckpoint(int checkpointNumber)
        {
            _currentCheckpointNumber = checkpointNumber;

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

            yield return new WaitForSeconds(_data.DisplayFlashTime);

            foreach (GameObject checkPointFlash in _checkpointFlashes)
                checkPointFlash.SetActive(false);
        }

    }

}
