using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image _healthNumberPanel;
    [SerializeField] private int _maxHealth;

    [SerializeField] private Sprite[] _numbers;

    [HideInInspector] public int Health;

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();

        Health = _maxHealth;
        UpdateHealthPanel(Health);
    }

    public void ApplyDamage(int damage)
    {
        if (!_player.IsShildActive)
        {
            Health = Mathf.Clamp(Health - damage, 0, _maxHealth);

            UpdateHealthPanel(Health);

            if (Health > 0)
                _player.RespawnPlayer();
            else
                _player.KillPlayer();
        }    
    }

    private void UpdateHealthPanel(int number)
    {
        _healthNumberPanel.sprite = _numbers[number];
    }

}
