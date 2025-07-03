using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotatingPointerHandler : MonoBehaviour
{
    public MyButton PlayButton;
    public MyButton SettingsButton;
    public MyButton ExitButton;
    public RectTransform Pointer;
    public float RotationSpeed = 1;

    private Vector3 _startRotation;
    private Vector3 _tragetRotation;
    private float _rotationLerpFactor = 0;
    private bool _updateRotation = false;

    private void OnEnable()
    {
        PlayButton.OnPointerEnterEvent += UpdatePointerRotation;
        SettingsButton.OnPointerEnterEvent += UpdatePointerRotation;
        ExitButton.OnPointerEnterEvent += UpdatePointerRotation;
    }

    private void OnDisable()
    {
        PlayButton.OnPointerEnterEvent -= UpdatePointerRotation;
        SettingsButton.OnPointerEnterEvent -= UpdatePointerRotation;
        ExitButton.OnPointerEnterEvent -= UpdatePointerRotation;
    }

    private void Awake()
    {
        _startRotation = Pointer.eulerAngles;
    }

    private void Update()
    {
        if (Pointer != null && _updateRotation)
        {
            _rotationLerpFactor += Time.deltaTime * RotationSpeed;
            Pointer.eulerAngles = Vector3.Lerp(_startRotation, _tragetRotation, _rotationLerpFactor);
            if (_rotationLerpFactor >= 1)
            {
                Pointer.eulerAngles = _tragetRotation;
                _updateRotation = false;
            }
        }
    }

    private void UpdatePointerRotation(PointerEventData eventData)
    {
        // do stuff here
        
        Vector2 PointedDirection = eventData.pointerEnter.transform.position - Pointer.position;
        float angle = Mathf.Atan2(PointedDirection.y, PointedDirection.x) * Mathf.Rad2Deg;

        //Debug.Log(PointedDirection);
        //Debug.LogWarning(angle);

        _startRotation = Pointer.eulerAngles;
        _tragetRotation = new Vector3(0, 0, angle);
        _rotationLerpFactor = 0;
        _updateRotation = true;
    }
}
