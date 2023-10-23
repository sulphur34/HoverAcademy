using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new menuVehicleData", menuName = "MenuVehicleData", order = 54)]
public class MenuVehicleData : ScriptableObject
{
    [SerializeField] private Vehicle _vehicle;
    [SerializeField] private int _armorValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _speedValue;
    [SerializeField] private int _handlingValue;
    [SerializeField] private string _name;
    [SerializeField] private Texture _logo;

    public Vehicle Vehicle => _vehicle;
    public int ArmorValue => _armorValue;
    public int DamageValue => _damageValue;
    public int SpeedValue => _speedValue;
    public int HandlingValue => _handlingValue;
    public string Name => _name;
    public Texture Logo => _logo;
}
