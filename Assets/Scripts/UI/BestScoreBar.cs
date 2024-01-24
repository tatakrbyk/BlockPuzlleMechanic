using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreBar : MonoBehaviour
{
    public static BestScoreBar instance;
    public Image fillInImage;
    public TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        fillInImage.fillAmount = 0;
    }
    private void OnEnable()
    {
        GameEvents.UpdateBestScoreBar += UpdateBestScoreBar;
    }
    private void OnDisable()
    {
        GameEvents.UpdateBestScoreBar -= UpdateBestScoreBar;
    }
    private void UpdateBestScoreBar(int currentScore, int bestScore)
    {
        float currentPercentage = (float) currentScore / (float) bestScore;
        fillInImage.fillAmount = currentPercentage;
        bestScoreText.text = bestScore.ToString();
    }

}
