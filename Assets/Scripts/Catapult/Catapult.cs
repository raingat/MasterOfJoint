using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class Catapult : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;

    [SerializeField] private float _maxDamper;
    [SerializeField] private float _maxSpring;
    [SerializeField] private float _minDamper;
    [SerializeField] private float _minSpring;

    [SerializeField] private float _springForReload;
    [SerializeField] private float _damperForReload;

    [SerializeField] private Vector3 _startAnchorPosition;
    [SerializeField] private Vector3 _finishAnchorPosition;

    [SerializeField] private float _timeToStop;
    [SerializeField] private float _timeToShoot;

    private SpringJoint _springJoint;

    private Rigidbody _connectedBody;

    private InputReader _inputReader = new();

    private bool _canShoot;
    private bool _canReload;

    private void Awake()
    {
        _springJoint = GetComponent<SpringJoint>();
        _connectedBody = _springJoint.connectedBody;

        _connectedBody.isKinematic = true;

        _canShoot = true;
    }

    private void Update()
    {
        if (_inputReader.IsRightMouseButton() && _canShoot)
        {
            _connectedBody.isKinematic = false;
            _canShoot = false;
            StartCoroutine(StoppingCatapult());
        }

        if (_inputReader.IsReloadButton() && _canReload)
        {
            _canReload = false;
            StartCoroutine(ReloadCatapult());
        }
    }

    private void ChangeParameterSpring(Vector3 anchor, float spring, float damper)
    {
        _springJoint.anchor = anchor;
        _springJoint.spring = spring;
        _springJoint.damper = damper;
    }

    private IEnumerator StoppingCatapult()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeToStop);

        yield return waitForSeconds;

        _springJoint.damper = _maxDamper;
        _springJoint.spring = _minSpring;

        _canReload = true;
    }

    private IEnumerator ReloadCatapult()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeToShoot);

        ChangeParameterSpring(_startAnchorPosition, _springForReload, _damperForReload);

        _connectedBody.WakeUp();

        yield return waitForSeconds;

        _connectedBody.isKinematic = true;

        ChangeParameterSpring(_finishAnchorPosition, _maxSpring, _minDamper);

        _projectile.ResetVelocity();
        _projectile.ResetTransform();

        _canShoot = true;
    }
}
