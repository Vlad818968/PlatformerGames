using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationHashes))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _playerRigidbody2D;
    private bool _isFacingRight = false;
    private bool _isGrounded;
    private AnimationHashes _animationHash;
    private MoveState _moveState = MoveState.Idle;
    private float _walkTime = 0, _walkCooldown = 0.05f;

    public void Die()
    {
        _animator.SetBool(_animationHash.IsDead, true);
        _playerRigidbody2D.velocity = new Vector3(0, 0, 0);
        _speed = 0;
    }

    public void MoveRight()
    {
        if (_isFacingRight == false)
        {
            Flip();
            _isFacingRight = true;
        }

        Move();
    }

    public void MoveLeft()
    {
        if (_isFacingRight == true)
        {
            Flip();
            _isFacingRight = false;
        }

        Move();
    }

    private void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _animationHash = GetComponent<AnimationHashes>();
    }

    private void Update()
    {
        if (_playerRigidbody2D.velocity.y == 0)
        {
            _isGrounded = true;
            _animator.SetBool(_animationHash.IsJump, false);
        }

        _walkTime-= Time.deltaTime;

        if (_walkTime <= 0)
        {
            Idle();
        }
    }

    private void Move()
    {
        _walkTime = _walkCooldown;

        if (_isGrounded == true)
        {
            _animator.SetBool(_animationHash.IsWalk, true);
            _moveState = MoveState.Walk;
        }

        transform.Translate((_isFacingRight == true ? Vector2.right : Vector2.left) * _speed * Time.deltaTime);

    }

    private void Idle()
    {
        _animator.SetBool(_animationHash.IsWalk, false);
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Jump()
    {
        if (_isGrounded == true)
        {
            _animator.SetBool(_animationHash.IsJump, true);
            _moveState = MoveState.Jump;
            _playerRigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    private enum MoveState
    {
        Idle,
        Walk,
        Jump
    }
}
