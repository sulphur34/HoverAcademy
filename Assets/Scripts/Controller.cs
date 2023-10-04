using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Movement))]
public class Controller : MonoBehaviour
{
    private Movement _movement;

    private void Start()
    {
        _movement = GetComponent<Movement>();
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
}
