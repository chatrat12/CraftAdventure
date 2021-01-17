
using UnityEngine;

public class PlayerMovement
{
    private Player _player;
    private Rigidbody _rigidbody;
    private Animator _animator;

    public PlayerMovement(Player player)
    {
        _player = player;
        _rigidbody = player.Avatar.GetComponent<Rigidbody>();
        _animator = player.Avatar.GetComponent<Animator>();
        _animator.applyRootMotion = false;
        var collider = player.Avatar.GetComponent<Collider>();
        collider.material = new PhysicMaterial() 
        { 
            frictionCombine = PhysicMaterialCombine.Minimum, 
            staticFriction = 0, 
            dynamicFriction = 0 
        };
    }

    public void Update()
    {
        var speed = _player.Input.MovementVector.magnitude;
        if (_player.Input.Sprint)
            speed *= 2;
        _animator.SetFloat("MoveSpeed", speed);
    }

    public void OnAnimatorMove()
    {
        var speed = _animator.deltaPosition.magnitude;
        var translationVector = new Vector3(_player.Input.MovementVector.x, 0, _player.Input.MovementVector.y);
        if (translationVector.magnitude > 0.1f)
        {
            //transform.position += translationVector * speed;
            var newVel = translationVector * speed / Time.deltaTime;
            newVel.y = _rigidbody.velocity.y;
            _rigidbody.velocity = newVel;
            _rigidbody.rotation = Quaternion.LookRotation(translationVector);
        }
        else
            _rigidbody.velocity = Vector3.zero;
    }
}
