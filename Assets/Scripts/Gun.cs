using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private bool _enemyGun;
    [SerializeField] private float _startTimeBetweenShoots;
    
    private float _timeBetweenShoots;

    private void Update()
    {
        if (_timeBetweenShoots <= 0) 
        {
            if (Input.GetKeyDown(KeyCode.Space) || _enemyGun)
            {
                Instantiate(_bulletPrefab, transform.position, transform.rotation, transform);
                _timeBetweenShoots = _startTimeBetweenShoots;
            }
        }
        else
        {
            _timeBetweenShoots -= Time.deltaTime;
        }
    }

}
