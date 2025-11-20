using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [Header("Refence")]
    [SerializeField] private PlayerUI _playerUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void StartScorePlayerUI(int bestScore)
    {
        _playerUI.IntiliazeScoreUI(bestScore);
    }

}
