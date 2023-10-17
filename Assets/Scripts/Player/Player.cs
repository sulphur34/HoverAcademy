using UnityEngine;

[RequireComponent(typeof(Hover))]
[RequireComponent (typeof(Controller))]
public class Player : MonoBehaviour
{
    private Hover _hover;
    private Controller _controller;

    private void Start()
    {
        _controller = GetComponent<Controller>();
        _hover = GetComponent<Hover>();
        _hover.Health.Death += DisableControls;
    }

    private void DisableControls()
    {
        _controller.enabled = false;
    }
}
