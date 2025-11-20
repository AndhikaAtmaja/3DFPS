using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform enemyContainer;

    [Header("Events")]
    public OnStartUIPlayerEventSO onStartUIPlayerEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        SaveSystem.Load();
        MusicManager.instance.PlayMusic("BGM1");
        SpawnerManager.instance.BeginSpawnLoop();
        StartPlayerUI();
    }

    public void StartPlayerUI()
    {
        onStartUIPlayerEvent.OnStartUIPlayer(0);
    }
}
