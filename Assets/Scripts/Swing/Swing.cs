using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Swing : MonoBehaviour
{
    [SerializeField] private float _force;

    private InputReader _inputReader = new();

    private HingeJoint _hingeJoint;
    private Rigidbody _connectedBody;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _connectedBody = _hingeJoint.connectedBody;
    }

    private void Update()
    {
        TryActivate();
    }

    private void TryActivate()
    {
        if (_inputReader.IsLeftMouseButton())
        {
            Move();
        }
    }

    private void Move()
    {
        _connectedBody.AddForce(transform.right * _force);
    }
}
