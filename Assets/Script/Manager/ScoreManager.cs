using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public struct BestScoreSaveData
{
    public int bestScore;
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Score Config")]
    [SerializeField] private int _score;
    [SerializeField] private int _bestScore;


    [Header("Events")]
    public OnUpdateScoreEventSO OnUpdateScoreEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AddScore(int amountScore)
    {
        _score += amountScore;
        OnUpdateScoreEvent.OnUpdateScore(_score);
        StatusManager.instance.UpdateScoreStatus(_score);
    }

    public void Save(ref BestScoreSaveData data)
    {
        if (_score > _bestScore)
            data.bestScore = _score;
    }

    public void Load(BestScoreSaveData data)
    {
        _bestScore = data.bestScore;
        HUDManager.instance.StartScorePlayerUI(_bestScore);
    }
}
