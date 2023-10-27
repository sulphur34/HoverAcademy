using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Hover))]
[RequireComponent (typeof(Controller))]
public class Player : MonoBehaviour
{
    private Hover _hover;
    private Controller _controller;
    private float _destroyDelay = 5f;

    public UnityAction PlayerDied;

    private void Start()
    {
        _controller = GetComponent<Controller>();
        _hover = GetComponent<Hover>();
        _hover.Health.Death += Die;
    }

    private void Die()
    {
        PlayerDied.Invoke();
        _controller.enabled = false;
    }
}
