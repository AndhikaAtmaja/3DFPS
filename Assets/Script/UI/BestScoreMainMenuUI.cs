using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreMainMenuUI : MonoBehaviour
{
    [SerializeField] private int bestScore;
    public TextMeshProUGUI bestScoreText;
    public GameObject BestScorePanel;

    private void Start()
    {
        BestScoreSaveData data = SaveSystem.GetBestScore();
        bestScore = data.bestScore;
    }

    public void UpdateBestScore()
    {
        if (bestScoreText != null)
            if (bestScore > 0)
            {
                bestScoreText.text = bestScore.ToString();
            }
            else
            {
                bestScoreText.text = 0.ToString("D5");
            }
    }
}
