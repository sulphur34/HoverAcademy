using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private SelectionMenu _selectionMenu;
    [SerializeField] private CinemachineFreeLook _camera;
    [SerializeField] private Transform _playerStartPoint;

    private void Awake()
    {
        _selectionMenu.OnGameBegin.AddListener(GameBegan);
    }

    private void GameBegan(float enemiesAmount, Vehicle playerVehicle)
    {
        _selectionMenu.gameObject.SetActive(false);
        Hover playerHover = playerVehicle.BuildHover(_playerStartPoint, typeof(Enemy));
        playerHover.AddComponent<Player>();
        _camera.Follow = playerHover.transform;
        _camera.LookAt = playerHover.transform;
        _enemySpawner.gameObject.SetActive(true);
        StartCoroutine(_enemySpawner.SpawnEnemies(enemiesAmount));
    }
}
