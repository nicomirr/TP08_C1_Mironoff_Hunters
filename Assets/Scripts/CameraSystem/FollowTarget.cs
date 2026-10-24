using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _playerOne;
    [SerializeField] private Transform _playerTwo;

    private void LateUpdate()
    {
        Vector3 midpoint = (_playerOne.position + _playerTwo.position) / 2f;
        this.transform.position = midpoint;
    }
}
