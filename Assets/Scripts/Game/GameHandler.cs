using Cinemachine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private SelectionMenu _selectionMenu;
    [SerializeField] private GameObject _selectionPad;
    [SerializeField] private CinemachineFreeLook _camera;
    [SerializeField] private Transform _playerStartPoint;
    [SerializeField] private Canvas _winMenu;
    [SerializeField] private Canvas _looseMenu;
    [SerializeField] private Canvas _hud;

    private float _enemiesToKill;
    private float _enemiesKilled;
    private Player _player;
    private float _levelEndTime;
    private Vector3 _defaultCameraPosition;
    public UnityAction EnemyDied;
    private Coroutine _endGameCoroutine;

    public float EnemiesLeft => _enemiesToKill - _enemiesKilled;

    private void Awake()
    {
        _selectionMenu.OnGameBegin.AddListener(GameBegan);
        EnemyDied += OnEnemyKilled;
        _defaultCameraPosition = new Vector3(0, 17, 35);
        _levelEndTime = 2f;
    }

    private void OnDisable()
    {
        StopCoroutine(_endGameCoroutine);
    }

    private void GameBegan(float enemiesAmount, Vehicle playerVehicle)
    {
        _enemiesToKill = enemiesAmount;
        _enemiesKilled = 0;
        _selectionMenu.gameObject.SetActive(false);
        _hud.gameObject.SetActive(true);
        Hover playerHover = playerVehicle.BuildHover(_playerStartPoint, typeof(Enemy));
        _player = playerHover.AddComponent<Player>();
        _player.PlayerDied += Loose;
        _camera.Follow = playerHover.transform;
        _camera.LookAt = playerHover.transform;        
        _enemySpawner.gameObject.SetActive(true);
        StartCoroutine(_enemySpawner.SpawnEnemies(enemiesAmount));
    }

    private void OnEnemyKilled()
    {
        _enemiesKilled++;
        if (_enemiesKilled == _enemiesToKill) 
            Win();
    }
    private IEnumerator OnGameEnd()
    {
        _hud.gameObject.SetActive(false);
        yield return new WaitForSeconds(_levelEndTime);
        _camera.transform.position = _defaultCameraPosition;
        _camera.Follow = null;
        _camera.LookAt = _selectionPad.transform;
        _enemySpawner.Reset();
        Destroy(_player.gameObject);
    }

    private void Win()
    {
        _endGameCoroutine = StartCoroutine(OnGameEnd());
        _winMenu.gameObject.SetActive(true);
        _winMenu.GetComponent<WinMenu>().Initialize(_enemiesKilled);
        
    }

    private void Loose()
    {
        _endGameCoroutine = StartCoroutine(OnGameEnd());
        _looseMenu.gameObject.SetActive(true);      
    }
}
