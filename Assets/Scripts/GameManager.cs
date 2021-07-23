using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [HideInInspector] public bool IsGameOver;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameCompletePanel;

    private float _delay = 1.5f;

    private void Start()
    {
        Time.timeScale = 1f;
        IsGameOver = false;

        if (_gameOverPanel.activeSelf) { _gameOverPanel.SetActive(false); }
        if (_gameCompletePanel.activeSelf) { _gameCompletePanel.SetActive(false); }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());   
    }

    private IEnumerator GameOverCoroutine()
    {
        IsGameOver = true;
        yield return new WaitForSeconds(_delay);

        _gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameComplete()
    {
        StartCoroutine(GameCompleteCoroutine());
    }

    private IEnumerator GameCompleteCoroutine()
    {
        IsGameOver = true;
        yield return new WaitForSeconds(_delay);

        GameSharedUI.Instance.UpdateScoreUIText();
        _gameCompletePanel.SetActive(true);
        Time.timeScale = 0f;
    }

}
