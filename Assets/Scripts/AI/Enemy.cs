using UnityEngine;

[RequireComponent(typeof(AIStateMachine))]
[RequireComponent (typeof(Hover))]
public class Enemy : MonoBehaviour
{ 
    private AIStateMachine _stateMachine;
    private Hover _hover;
    private EnemySpawner _spawner;
    private GameHandler _gameHandler;

    void Awake()
    {
        _hover = GetComponent<Hover> ();
        _hover.Health.Death += OnDeath;
        _spawner = GetComponentInParent<EnemySpawner> ();
        _gameHandler = FindObjectOfType<GameHandler> ();
    }

    private void OnEnable()
    {
        Reset();
    }

    public void Initialize(Waypoint[] waypoints, Player player)
    {
        _stateMachine = GetComponent<AIStateMachine>();
        GetComponent<AIFreeRoam>().Initialize(waypoints, player);
        GetComponent<AITargetChase>().Initialize(waypoints, player);
        _stateMachine.Enable();
    }

    public void OnDeath()
    {
        _stateMachine.Disable();
        _gameHandler.EnemyDied.Invoke();
    }

    private void Reset()
    {
        _stateMachine?.Disable();
        _stateMachine?.Enable();
    }
}
