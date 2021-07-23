using UnityEngine;
using UnityEngine.UI;

public class GameSharedUI : MonoBehaviour
{
    #region Singleton class: GameSharedUI

    public static GameSharedUI Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private Text[] scoreUIText;

    private void Start()
    {
        UpdateScoreUIText();
    }

    public void UpdateScoreUIText()
    {
        for (int i = 0; i < scoreUIText.Length; i++)
        {
            scoreUIText[i].text = "Score: " + Player.Score;
        }
    }

}
