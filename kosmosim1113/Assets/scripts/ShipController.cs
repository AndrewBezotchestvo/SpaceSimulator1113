using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float _force = 1000f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _maxSpeed = 100f;

    private Rigidbody _rb;

    private float _verticalInput; //вперед назад
    private float _horizontalInput; //вправо влево рыскание
    private float _rollInput; //вправо влево крен
    private float _pitchInput; //вверх вниз

    private bool _cursorVisible = false;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.visible = _cursorVisible;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        _rollInput = Input.GetAxis("Mouse X");
        _pitchInput = Input.GetAxis("Mouse Y");

        if (_cursorVisible)
        {
            Cursor.visible = _cursorVisible;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = _cursorVisible;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _cursorVisible = !_cursorVisible;
        }

    }

    private void FixedUpdate()
    {
        _rb.AddTorque(transform.up * _horizontalInput * _rotationSpeed * 3 * Time.fixedDeltaTime, ForceMode.VelocityChange);
        _rb.AddTorque(transform.forward * -_rollInput * _rotationSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        _rb.AddTorque(transform.right * -_pitchInput * _rotationSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if (_verticalInput > 0)
        {
            _rb.AddForce(transform.forward * _force * _verticalInput);
        }
        if (_verticalInput < 0)
        {
            _rb.AddForce(transform.forward * _force * 0.3f * _verticalInput);
        }
    }
}
