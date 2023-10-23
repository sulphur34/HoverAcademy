using UnityEngine;

[RequireComponent(typeof(AIStateMachine))]
[RequireComponent (typeof(Hover))]
public class Enemy : MonoBehaviour
{ 
    private AIStateMachine _stateMachine;
    private Hover _hover;
    private EnemySpawner _spawner;

    void Awake()
    {
        _stateMachine = GetComponent<AIStateMachine> ();
        _hover = GetComponent<Hover> ();
        _hover.Health.Death += OnDeath;
        _spawner = GetComponentInParent<EnemySpawner> ();
    }

    public void Initialize(Waypoint[] waypoints, Player player)
    {
        GetComponent<AIFreeRoam>().Initialize(waypoints, player);
        GetComponent<AITargetChase>().Initialize(waypoints, player);
        _stateMachine.Enable();
    }

    public void OnDeath()
    {
        _stateMachine.Disable();
    }

    public void Reset()
    {
        _stateMachine.Enable();
    }
}
