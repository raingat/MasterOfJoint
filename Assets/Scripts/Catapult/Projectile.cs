using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Transform _transform;

    private Rigidbody _rigidbody;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _transform = transform;
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    public void ResetTransform()
    {
        _transform.position = _startPosition;
        _transform.rotation = _startRotation;
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
