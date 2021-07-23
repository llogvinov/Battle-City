using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    [HideInInspector] public int Health;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        Health = _maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, _maxHealth);

        if (Health == 0)
        {
            _enemy.AddPoints();
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
