using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverFlag;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            Instantiate(_gameOverFlag, transform.position, Quaternion.identity);
            _gameManager.GameOver();
            Destroy(gameObject);
        } 
    }
}
