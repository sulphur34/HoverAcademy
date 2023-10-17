using UnityEngine;

public class EngineSystem : MonoBehaviour
{
    [SerializeField] private Engine[] _engines;

    private System.Random _random;

    private void Start()
    {
        _random = new System.Random();
        _engines = GetComponentsInChildren<Engine>();
    }

    public void Initialize(Vehicle vehicle)
    {
        foreach (var engine in _engines)
        {
            engine.Initialize(vehicle);
        }
    }

    public void Crash()
    {
        PickRandomEngine().enabled = false;
        PickRandomEngine().enabled = false;
    }

    public void Restore()
    {
        foreach (var engine in _engines)
        {
            engine.enabled = true;
        }
    }

    private Engine PickRandomEngine()
    {
        return _engines[_random.Next(_engines.Length)];
    }
}
