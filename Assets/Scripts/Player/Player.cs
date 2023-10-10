using UnityEngine;

[RequireComponent(typeof(Hover))]
public class Player : MonoBehaviour
{
    [SerializeField] private Hover _hover;

    public void FixedUpdate()
    {
        _hover.GetComponent<Rotator>().RotateTowardsDirection(Camera.main.transform.forward);
    }
}
