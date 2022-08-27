using UnityEngine;

[RequireComponent(typeof(PlayerController))]

public class PlayerKeyboard : MonoBehaviour
{
    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _controller.MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            _controller.MoveRight();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _controller.Jump();
        }
    }
}
