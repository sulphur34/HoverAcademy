using UnityEngine;

[RequireComponent(typeof(AIStateMachine))]
[RequireComponent (typeof(Health))]
public class Enemy : MonoBehaviour
{ 
    private AIStateMachine _stateMachine;
    private Health _health;

    void Awake()
    {
        _stateMachine = GetComponent<AIStateMachine> ();
        _health = GetComponent<Health> ();
        _health.Death += OnDeath;
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
