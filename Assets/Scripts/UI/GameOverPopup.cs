using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public GameObject gameOverPopup;
    public GameObject loosePopup;
    public GameObject newBestScorePopup;
    public TextMeshProUGUI score;
    private void Start()
    {
        gameOverPopup.SetActive(false);
    }
    private void OnEnable()
    {
        GameEvents.GameOver += OnGameOver;
    }
    private void OnDisable()
    {
        GameEvents.GameOver -= OnGameOver;
    }
    private void OnGameOver(bool newBestScore)
    {
        score.text = BestScoreBar.instance.bestScoreText.text;
        gameOverPopup.SetActive(true);
        loosePopup.SetActive(false);
        newBestScorePopup.SetActive(true);
    }
}
