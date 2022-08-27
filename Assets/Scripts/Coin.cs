using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent (typeof(SpriteRenderer))]

public class Coin : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached;

    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            _reached.Invoke();
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
            Destroy(gameObject,1);
        }
    }
}
