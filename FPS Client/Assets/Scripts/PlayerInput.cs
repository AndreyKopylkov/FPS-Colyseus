using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _primaryMovementDirection;
    private float _mouseY;
    private float _mouseX;
    private Vector2 _secondaryMovementDirection;

    // public Vector3 MovementDirection { get { return _movementDirection; } }
    public Vector3 PrimaryMovementDirection => _primaryMovementDirection;
    public Vector2 SecondaryMovementDirection => _secondaryMovementDirection;
    public event Action OnDashPush;

    
    void Update()
    {
        PrimaryMoveInput();
        SecondaryMoveInput();
        DashInput();
    }

    private void PrimaryMoveInput()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        _moveVertical = Input.GetAxis("Vertical");
        _primaryMovementDirection = new Vector3(_moveHorizontal, 0, _moveVertical);
    }
    
    private void SecondaryMoveInput()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        _secondaryMovementDirection = new Vector2(_mouseX, _mouseY);
    }
    
    private void DashInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnDashPush?.Invoke();
            Debug.Log("Dash Input");
        }
    }

    
}