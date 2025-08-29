using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Swing : MonoBehaviour
{
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minAngle;

    [SerializeField] private float _stepFactor;
    [SerializeField] private float _defaultStep;

    private InputReader _inputReader = new();

    private HingeJoint _hingeJoint;
    private JointSpring _springJoint;

    private float _targetPosition;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _springJoint = _hingeJoint.spring;
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
        StartCoroutine(ChangeTargetPosition());
    }

    private IEnumerator ChangeTargetPosition()
    {
        float step = _defaultStep;

        float angle = _maxAngle;

        _targetPosition = _springJoint.targetPosition;

        while (enabled)
        {
            if (_hingeJoint.angle > _maxAngle || _hingeJoint.angle < _minAngle)
            {
                step = _defaultStep;

                if (_hingeJoint.angle > _maxAngle)
                {
                    angle = _minAngle;
                }

                if (_hingeJoint.angle < _minAngle)
                {
                    angle = _maxAngle;
                }
            }

            step  += _stepFactor;

            _targetPosition = Mathf.MoveTowards(_targetPosition, angle, step * Time.deltaTime);

            _springJoint.targetPosition = _targetPosition;
            _hingeJoint.spring = _springJoint;

            yield return null;
        }
    }
}
