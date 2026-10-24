using UnityEngine;

public class PlayerBoundsController
{
    private readonly Transform _playerTransform;
    private readonly Camera _camera;

    public PlayerBoundsController(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _camera = Camera.main;
    }

    public bool PlayerInHorizontalBoundsLimit()
    {
        Vector3 playerViewportPoint = _camera.WorldToViewportPoint(_playerTransform.position);

        return playerViewportPoint.x <= 0.05f || playerViewportPoint.x >= 0.95f;
    }

    public bool PlayerInVerticalBoundsLimit()
    {
        Vector3 playerViewportPoint = _camera.WorldToViewportPoint(_playerTransform.position);

        return playerViewportPoint.y <= 0.05f || playerViewportPoint.y >= 0.95f;
    }
}
