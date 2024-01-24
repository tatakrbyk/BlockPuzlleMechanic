using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public SquareTextureData squareTextureData;
    public TextMeshProUGUI scoreText;

    private bool newBestScore = false; 
    private BestScoreData bestScores_ = new BestScoreData();
    private int currentScores_;
    private string bestScoreKey_ = "bsdat";
    private void Awake()
    {
        if (BinaryDataStream.Exist(bestScoreKey_))
        {
            StartCoroutine(ReadDataFile());
        }
    }
    private void Start()
    {
        currentScores_ = 0;
        newBestScore = false;
        squareTextureData.SetStartColor();
        UpdateScoreText();
    }

    private void OnEnable()
    {
        GameEvents.AddScores += AddScores;
        GameEvents.GameOver += SaveBestScore;
    }
    private void OnDisable()
    {
        GameEvents.AddScores -= AddScores;
        GameEvents.GameOver -= SaveBestScore;
    }

    public void SaveBestScore(bool newBestScores)
    {
        BinaryDataStream.Save<BestScoreData>(bestScores_, bestScoreKey_);
    }
    private void AddScores(int scores)
    {
        currentScores_ += scores;
        if(currentScores_ > bestScores_.score)
        {
            newBestScore = true;
            bestScores_.score = currentScores_;
            SaveBestScore(true);
        }
        UpdateScoreColor();
        GameEvents.UpdateBestScoreBar(currentScores_, bestScores_.score);
        UpdateScoreText ();
    }
    private void UpdateScoreColor()
    {
        if(GameEvents.UpdateSquareColor != null && currentScores_ >= squareTextureData.tresholdVal)
        {
            squareTextureData.UpdateColors(currentScores_);
            GameEvents.UpdateSquareColor(squareTextureData.currentColor);
        }
    }
    private void UpdateScoreText()
    {
        scoreText.text = currentScores_.ToString();
    } 

    private IEnumerator ReadDataFile()
    {
        bestScores_ = BinaryDataStream.Read<BestScoreData>(bestScoreKey_);
        yield return new WaitForEndOfFrame(); ;
        GameEvents.UpdateBestScoreBar(currentScores_, bestScores_.score);
    }
}

[System.Serializable]
public class BestScoreData
{
    public int score = 0;
}