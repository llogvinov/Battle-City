using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Movable : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    
    public GameManager.Direction _direction;
    protected Rigidbody2D _rigidbody;

    protected bool _isMoving;
    
    protected void MovePlayer()
    {
        if (_isMoving)
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
    }

    private void MoveDirection(float zRotation, Vector2 direction)
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);
        _rigidbody.MovePosition(_rigidbody.position +
            direction * _moveSpeed * Time.fixedDeltaTime);
    }
}
