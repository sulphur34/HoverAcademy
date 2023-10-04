using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent(typeof(DirectionAligner))]
[RequireComponent(typeof(Movement))]

public class Hover : MonoBehaviour
{
    [SerializeField] EngineSystem _engineSystem;
    [SerializeField] 

    private void Initialize(Vehicle vehicle, Vector3 alignPosition)
    {
        
    }
}
