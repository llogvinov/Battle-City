using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Explosion Animations")]
    [SerializeField] private ExplosionDestroyer _smallExplosion;
    [SerializeField] private ExplosionDestroyer _bigExplosion;
    [Space]

    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private bool _enemyBullet;

    private Rigidbody2D _bulletRigidbody;

    private GameManager.Direction _direction;

    private void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();

        if (!_enemyBullet)
            _direction = transform.parent.parent.GetComponent<PlayerMover>()._direction;
        else
            _direction = transform.parent.parent.GetComponent<EnemyMover>()._direction;

        // detach the transform from its parent
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        switch (_direction)
        {
            case GameManager.Direction.Up:
                MoveBullet(Vector2.up);
                break;
            case GameManager.Direction.Down:
                MoveBullet(Vector2.down);
                break;
            case GameManager.Direction.Left:
                MoveBullet(Vector2.left);
                break;
            case GameManager.Direction.Right:
                MoveBullet(Vector2.right);
                break;
            default:
                break;
        }
    }

    private void MoveBullet(Vector2 direction)
    {
        _bulletRigidbody.MovePosition(_bulletRigidbody.position +
            direction * _moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            ExplosionAndDestroy(_smallExplosion);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.TryGetComponent(out EnemyHealth enemyHealth) && !_enemyBullet)
        {
            // apply damage
            enemyHealth.ApplyDamage(_damage);

            ExplosionAndDestroy(_bigExplosion);
        }
        else if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth) && _enemyBullet)
        {
            // apply damage
            playerHealth.ApplyDamage(_damage);

            ExplosionAndDestroy(_bigExplosion);
        }
        else if (collision.gameObject.CompareTag("Environment"))
        {
            Vector3 hitPosition = Vector3.zero;
            Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
            if (tilemap)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                }
            }

            ExplosionAndDestroy(_smallExplosion);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            ExplosionAndDestroy(_smallExplosion);
        }
        else if (collision.gameObject.CompareTag("Eagle"))
        {
            ExplosionAndDestroy(_bigExplosion);
        }
    }

    private void ExplosionAndDestroy(ExplosionDestroyer explosion)
    {
        // play explosion animation
        Instantiate(explosion, transform.position, explosion.transform.rotation);

        // destroy bullet
        Destroy(gameObject);
    }

}
