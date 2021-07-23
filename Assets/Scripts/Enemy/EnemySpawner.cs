using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _enemyIcon;
    [SerializeField] private GameObject _gridLayoutGroup;
    [Space]

    [Header("Spawn")]
    [SerializeField] private GameObject _spawnAnimation;
    [SerializeField] private float _spawnAnimationTime;
    [Space]

    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private int _enemiesToSpawn;

    [SerializeField] private float _startTimeBetweenSpawn;

    private GameManager _gameManager;

    private float _timeBetweenSpawn;

    private float _ySpawnPosition = 6.5f;
    private float _xMinPosition = -6.5f;
    private float _xMaxPosition = 5.5f;
    private float _additionalPosition = 0.5f;
    private Vector2 _spawnPosition;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        if (_spawnAnimation.activeSelf) { _spawnAnimation.SetActive(false); }

        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            Instantiate(_enemyIcon, _gridLayoutGroup.transform);
        }
    }

    private void Update()
    {
        if (_enemyPrefabs.Length > 0 && _enemiesToSpawn > 0)
        {
            if (_timeBetweenSpawn <= 0)
            {
                _spawnPosition = GenerateSpawnPosition();
                StartCoroutine(SpawnEnemy(_spawnPosition));
                _timeBetweenSpawn = _startTimeBetweenSpawn;
            }
            else
            {
                _timeBetweenSpawn -= Time.deltaTime;
            }
        }
        else if (_enemiesToSpawn == 0 && transform.childCount <= 2)
        {
            _gameManager.GameComplete();
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        int xInt = Random.Range(Mathf.FloorToInt(_xMinPosition), Mathf.FloorToInt(_xMaxPosition) + 1);
        return new Vector2(xInt + _additionalPosition, _ySpawnPosition);
    }

    private void DeleteIconFromGrid()
    {
        Destroy(_gridLayoutGroup.transform.GetChild(_gridLayoutGroup.transform.childCount - 1).gameObject);
    }

    private IEnumerator SpawnEnemy(Vector2 spawnPosition)
    {
        _spawnAnimation.transform.position = spawnPosition;
        _spawnAnimation.SetActive(true);

        yield return new WaitForSeconds(_spawnAnimationTime);
        
        _spawnAnimation.SetActive(false);
        Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)],
                    spawnPosition,
                    Quaternion.identity,
                    transform);

        _enemiesToSpawn--;
        DeleteIconFromGrid();
    }
}
