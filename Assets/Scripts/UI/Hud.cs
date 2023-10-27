using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _enemiesLeftLabel;

    private GameHandler _gameHandler;

    private void OnEnable()
    {
        SetLabelValue();
    }

    private void Awake()
    {
        _gameHandler = FindObjectOfType<GameHandler>();
        _gameHandler.EnemyDied += SetLabelValue;
    }

    public void SetLabelValue()
    {
        _enemiesLeftLabel.text = _gameHandler.EnemiesLeft.ToString();
    }
}
