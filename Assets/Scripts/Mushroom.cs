using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationHashes))]
[RequireComponent(typeof(Rigidbody2D))]

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float _patroolRange;
    [SerializeField] private float _speed;
    [SerializeField] private UnityEvent _reached;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _diedTime = 1f;

    private float _leftPosition, _rightPosition;
    private AnimationHashes _animationHash;
    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _animationHash = GetComponent<AnimationHashes>();
        _leftPosition = transform.position.x - _patroolRange;
        _rightPosition = transform.position.x + _patroolRange;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_speed * Time.deltaTime, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _leftPosition, _rightPosition), transform.position.y, transform.position.z);

        if (transform.position.x == _rightPosition || transform.position.x == _leftPosition)
        {
            _speed = _speed * -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            if (collision.gameObject.transform.position.y > transform.position.y)
            {
                StartCoroutine(Die());
            }
            else
            {
                _reached.Invoke();
            }
        }
    }

    private IEnumerator Die()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        _rigidbody2D.isKinematic = true;
        _speed = 0;
        _animator.SetBool(_animationHash.MushroomDead, true);
        yield return new WaitForSeconds(_diedTime);
        Destroy(gameObject);
    }
}
