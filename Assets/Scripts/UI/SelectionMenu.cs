using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _leftSelectionButton;
    [SerializeField] private Button _rightSelectionButton;
    [SerializeField] private Slider _enemiesNumberSlider;
    [SerializeField] private TextMeshProUGUI _enemiesValueLabel;
    [SerializeField] private GameObject _hoverSelectionPlatform;
    [SerializeField] private MenuVehicleData[] _vehiclesData;
    [SerializeField] private Slider _armorSlider;
    [SerializeField] private Slider _damageSlider;
    [SerializeField] private Slider _handlingSlider;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private TextMeshProUGUI _hoverNameLabel;
    [SerializeField] private RawImage _hoverLogoImage;

    private Coroutine _rotationCoroutine;
    private float _currentRotationAngle = -90f;
    private int _currentVehiclePosition = 0;
    private float _platformRotationSpeed = 300;
    private int _defaultEnemiesNumber = 5;

    public int CurrentArmor => _vehiclesData[_currentVehiclePosition].ArmorValue;
    public int CurrentDamage => _vehiclesData[_currentVehiclePosition].DamageValue;
    public int CurrentHandling => _vehiclesData[_currentVehiclePosition].HandlingValue;
    public int CurrentSpeed => _vehiclesData[_currentVehiclePosition].SpeedValue;
    public string CurrentHoverName => _vehiclesData[_currentVehiclePosition].Name;
    public Texture CurrentHoverLogo => _vehiclesData[_currentVehiclePosition].Logo;

    public UnityEvent<float, Vehicle> OnGameBegin;

    private void Awake()
    {
        SetHoverValues();
        _enemiesNumberSlider.value = _defaultEnemiesNumber;
    }

    public void BeginGame()
    {
        OnGameBegin.Invoke(_enemiesNumberSlider.value, _vehiclesData[_currentVehiclePosition].Vehicle);
    }

    public void OnEnemiesSliderValueChanged()
    {
        _enemiesValueLabel.text = _enemiesNumberSlider.value.ToString();
    }

    public void OnRotationButtonClicked(float angle)
    {
        if (_rotationCoroutine != null)
            StopCoroutine(_rotationCoroutine);

        if (angle > 0)
            SwitchVehicleIndexUp();
        else
            SwitchVehicleIndexDown();

        SetHoverValues();
        _rotationCoroutine = StartCoroutine(RotateSelectionPlatform(angle));
    }

    private void SwitchVehicleIndexUp()
    {
        if (_currentVehiclePosition < _vehiclesData.Length - 1)
            _currentVehiclePosition++;
        else
            _currentVehiclePosition = 0;
    }
    private void SwitchVehicleIndexDown()
    {
        if (_currentVehiclePosition > 0)
            _currentVehiclePosition--;
        else
            _currentVehiclePosition = _vehiclesData.Length - 1;
    }

    private IEnumerator RotateSelectionPlatform(float angle)
    {
        _currentRotationAngle += angle;
        Quaternion targetRotation = Quaternion.Euler(0,_currentRotationAngle, 0);

        while (_hoverSelectionPlatform.transform.rotation != targetRotation) 
        {
            _hoverSelectionPlatform.transform.rotation = 
                Quaternion.RotateTowards(_hoverSelectionPlatform.transform.rotation, targetRotation, Time.deltaTime * _platformRotationSpeed);
            yield return null;
        }
    }

    private void SetHoverValues()
    {
        _armorSlider.value = CurrentArmor;
        _damageSlider.value = CurrentDamage;
        _handlingSlider.value = CurrentHandling;
        _speedSlider.value = CurrentSpeed;
        _hoverNameLabel.text = CurrentHoverName;
        _hoverLogoImage.texture = CurrentHoverLogo;
    }
}
