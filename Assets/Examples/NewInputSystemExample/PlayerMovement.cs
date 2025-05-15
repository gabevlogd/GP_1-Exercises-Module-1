using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputSystem_Actions _inputHandler;

    [SerializeField]
    private float _speed = 100.0f;
    private Vector3 _moveInput;

    private Rigidbody _rb;

    private void OnEnable()
    {
        if (_inputHandler == null)
        {
            _inputHandler = new InputSystem_Actions();
        }

        _inputHandler.Gameplay.Move.started += OnMoveStarted;
        _inputHandler.Gameplay.Move.performed += OnMovePerformed;
        _inputHandler.Gameplay.Move.canceled += OnMoveCanceled;
        _inputHandler.Enable();
    }

    private void OnDisable()
    {
        _inputHandler.Gameplay.Move.started -= OnMoveStarted;
        _inputHandler.Gameplay.Move.performed -= OnMovePerformed;
        _inputHandler.Gameplay.Move.canceled -= OnMoveCanceled;
        _inputHandler.Disable();
    }

    private void Awake()
    {
        TryGetComponent(out _rb);
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            Vector3 velocity = _moveInput * _speed * Time.fixedDeltaTime;
            //Debug.Log(velocity);
            _rb.linearVelocity = velocity;
        }
    }

    private void OnMoveStarted(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log("Started");
    }

    private void OnMovePerformed(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log("Performed");
        _moveInput = callbackContext.ReadValue<Vector3>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log("Canceled");
    }

    
}
