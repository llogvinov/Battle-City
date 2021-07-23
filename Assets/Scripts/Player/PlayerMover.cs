using UnityEngine;

public class PlayerMover : Movable
{
    private Animator _playerAnimator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update() => GetPlayerInput();

    private void FixedUpdate() => MovePlayer();

    private void GetPlayerInput()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            ChangeDirection(GameManager.Direction.Up);
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            ChangeDirection(GameManager.Direction.Down);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            ChangeDirection(GameManager.Direction.Left);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            ChangeDirection(GameManager.Direction.Right);
        }
        else
        {
            _isMoving = false;
            _playerAnimator.SetBool("isMoving", _isMoving);
        }
    }

    private void ChangeDirection(GameManager.Direction direction)
    {
        _direction = direction;
        _isMoving = true;
        _playerAnimator.SetBool("isMoving", _isMoving);
    }

}
