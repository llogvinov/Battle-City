using System;
using System.Collections;
using UnityEngine;

public class EnemyMover : Movable
{
    [SerializeField] private float _minTime = 1.5f;
    [SerializeField] private float _maxTime = 4f;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _isMoving = true;

        StartCoroutine(ChangeDirection());
    }

    private void FixedUpdate() => MoveEnemy();

    private IEnumerator ChangeDirection()
    {
        while (!_gameManager.IsGameOver)
        {
            _direction = GenerateRandomDirection();

            yield return new WaitForSeconds(GenerateRandomTime());
        }
    }

    private GameManager.Direction GenerateRandomDirection()
    {
        var v = Enum.GetValues(typeof(GameManager.Direction));
        return (GameManager.Direction)v.GetValue(new System.Random().Next(v.Length));
    }

    private float GenerateRandomTime()
    {
        return UnityEngine.Random.Range(_minTime, _maxTime);
    }

    private void MoveEnemy()
    {
        switch (_direction)
        {
            case GameManager.Direction.Up:
                MoveDirection(0f, Vector2.up);
                break;
            case GameManager.Direction.Down:
                MoveDirection(180f, Vector2.down);
                break;
            case GameManager.Direction.Left:
                MoveDirection(90f, Vector2.left);
                break;
            case GameManager.Direction.Right:
                MoveDirection(-90f, Vector2.right);
                break;
            default:
                break;
        }
    }

    private void MoveDirection(float zRotation, Vector2 direction)
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);
        _rigidbody.MovePosition(_rigidbody.position +
            direction * _moveSpeed * Time.fixedDeltaTime);
    }

}
