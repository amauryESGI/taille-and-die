using UnityEngine;

public class FollowSmoothlyTarget : MonoBehaviour {
    private Vector2 _velocity;

    [SerializeField] private GameObject _player;

    [SerializeField] private float _smothTimeX;
    [SerializeField] private float _smothTimeY;

    [SerializeField] private bool _bounds;

    [SerializeField] private Vector2 _minPos;
    [SerializeField] private Vector2 _maxPos;

    void FixedUpdate() {
        float posX = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x, ref _velocity.x, _smothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y, ref _velocity.x, _smothTimeY);

        if (_bounds) {
            posX = Mathf.Clamp(posX, _minPos.x, _maxPos.x);
            posY = Mathf.Clamp(posY, _minPos.y, _maxPos.y);
        }

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    public bool IsBoundable() {
        return _bounds;
    }

    public void SetMinPos() {
        _minPos = transform.position;
    }

    public void SetMaxPos() {
        _maxPos = transform.position;
    }
}