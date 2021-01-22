
using UnityEngine;
using UnityEngine.AI;

public class MovementSlime
{
    private Character _slime;
    private Animator _animator;
    private Rigidbody _rigidbody;
    //private NavMeshAgent _navAgent;
    private GameObject _target;
    private NavMeshPath _path = new NavMeshPath();

    public MovementSlime(Character slime)
    {
        _slime = slime;
        _animator = slime.Avatar.GetComponent<Animator>();
        _rigidbody = slime.Avatar.GetComponent<Rigidbody>();
        //_navAgent = slime.Avatar.GetComponent<NavMeshAgent>();

        //_navAgent.updatePosition = false;
        //_navAgent.updateRotation = false;



        var collider = slime.Avatar.GetComponent<Collider>();
        collider.material = new PhysicMaterial()
        {
            frictionCombine = PhysicMaterialCombine.Minimum,
            staticFriction = 0,
            dynamicFriction = 0
        };
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
        //_navAgent.SetDestination(target.transform.position);
    }

    public void OnAnimatorMove()
    {
        //if (_target != null)
        //_navAgent.SetDestination(_target.transform.position);
        if (_target != null && Vector3.Distance(_target.transform.position, _slime.Avatar.transform.position) > 1.0f)
        {
            NavMesh.CalculatePath(_slime.Avatar.transform.position, _target.transform.position, NavMesh.AllAreas, _path);
            _animator.SetFloat("MoveSpeed", 1);
            var nextCornerPos = _path.corners[1];
            var dir = (nextCornerPos - _slime.Avatar.transform.position).normalized;
            _rigidbody.rotation = Quaternion.LookRotation(dir);
            var speed = _animator.deltaPosition.magnitude / Time.deltaTime;
            _rigidbody.velocity = _slime.Avatar.transform.forward * speed;
        }
        else
            _animator.SetFloat("MoveSpeed", 0);
    }

    public void OnDrawGizmos()
    {
        if (_path.corners.Length > 1)
        {
            var nextCornerPos = _path.corners[1];
            nextCornerPos.y = 0;
            var dir = (nextCornerPos - _slime.Avatar.transform.position).normalized;
            Gizmos.DrawLine(_slime.Avatar.transform.position, _slime.Avatar.transform.position + dir);
        }
        for (int i = 0; i < _path.corners.Length; i++)
        {
            Gizmos.color = i == 1 ? Color.red : Color.white;
            if (i > 0)
                Gizmos.DrawSphere(_path.corners[i], 0.1f);
            Gizmos.color = Color.cyan;
            if (i < _path.corners.Length - 1)
                Gizmos.DrawLine(_path.corners[i], _path.corners[i + 1]);
        }

        Gizmos.color = Color.white;
    }
}
