using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _salaryLabel;
    [SerializeField] private TextMeshProUGUI _positionLabel;
    [SerializeField] private Image[] _rankSigns;

    private List<string> _positionsNames;
    private float _salaryMultiplier;
    private float _positionDivider;

    private void Awake()
    {
        _salaryMultiplier = 450f;
        _positionDivider = 10f;

        _positionsNames = new List<string>() 
        {
            "Second Pilot",
            "First Pilot",
            "Ace Pilot",
            "Wing Commander",
        };

        ResetRank();
    }

    public void Initialize(float enemiesDefeated)
    {
        _salaryLabel.text = (enemiesDefeated * _salaryMultiplier).ToString();
        int positionIndex = Mathf.RoundToInt(enemiesDefeated / _positionDivider);        
        _positionLabel.text = _positionsNames[positionIndex];
        SetRank(positionIndex);
    }

    private void ResetRank()
    {
        foreach (Image rankSign in _rankSigns) 
        {
            rankSign.enabled = false;
        }
    }

    private void SetRank(float rankCount)
    {
        for (int i = 0; i <= rankCount; i++)
        {
            _rankSigns[i].enabled = true;
        }
    }
}
