using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent (typeof(Rotator))]
public class Controller : MonoBehaviour
{
    private Mover _movement;
    private Rotator _rotation;

    private void Start()
    {
        _movement = GetComponent<Mover>();
        _rotation = GetComponent<Rotator>();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            _movement.MoveForward();
        }

        if (Input.GetKey(KeyCode.A))
        {
            _movement.StrafeLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            _movement.StrafeRight();
        }

        if (Input.GetKey(KeyCode.S))
        {
            _movement.MoveBackward();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _movement.Brake();
        }
    }

    public void FixedUpdate()
    {
        _rotation.RotateTowardsDirection(Camera.main.transform.forward);
    }
}
