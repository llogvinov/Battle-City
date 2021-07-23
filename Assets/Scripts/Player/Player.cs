using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnPosition;

    [SerializeField] private GameObject _shildAnimation;

    [HideInInspector] public static int Score;
    [HideInInspector] public bool IsShildActive;
    private float _timeShildActive = 3f;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        Score = 0;

        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        transform.position = _spawnPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // activate shild
        StartCoroutine(ActivateShild());
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
        _gameManager.GameOver();
    }

    private IEnumerator ActivateShild()
    {
        IsShildActive = true;
        _shildAnimation.SetActive(true);
        
        yield return new WaitForSeconds(_timeShildActive);
        
        IsShildActive = false;
        _shildAnimation.SetActive(false);
    }

    public void AddScore(int points)
    {
        Score += points;
        Debug.Log("score: " + Score);
    }

}
