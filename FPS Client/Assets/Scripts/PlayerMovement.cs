using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private MovementTypes _movementType = MovementTypes.CharacterControllerMove;

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerInput _playerInput;

    public enum MovementTypes {RigidbodyMovePosition, CharacterControllerMove, ChangePosition}

    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _movementDirection;
    private bool _canMove;
    private Camera _camera;

    public bool CanMove { get => _canMove; set => _canMove = value; }

    private void Awake()
    {
        Initialize();
    }
    
    private void FixedUpdate()
    {
        HandleMovement();
        Move();
    }

    private void Initialize()
    {
        _canMove = true;
        _camera = Camera.main;
    }

    private void HandleMovement()
    {
        //Для третьего лица
        // Vector3 cameraForward = _camera.transform.forward;
        // cameraForward.y = 0;
        // cameraForward = cameraForward.normalized;
        // Quaternion rotation = Quaternion.LookRotation(cameraForward);
        //     
        // _movementDirection = (rotation * _playerInput.PrimaryMovementDirection).normalized;

        _movementDirection = _playerInput.PrimaryMovementDirection.normalized;
    }

    private void Move()
    {
        if(!_canMove) return;
        
        switch (_movementType)
        {
            case MovementTypes.RigidbodyMovePosition:
                // _rigidbody.MovePosition(_rigidbody.position + _movementDirection * _moveSpeed);
                Vector3 velocity = (transform.forward * _playerInput.PrimaryMovementDirection.z + transform.right *
                                   _playerInput.PrimaryMovementDirection.x).normalized * _moveSpeed;
                _rigidbody.velocity = velocity;
                break;
            case MovementTypes.CharacterControllerMove:
                _characterController.Move(_movementDirection * _moveSpeed);
                break;
            case MovementTypes.ChangePosition:
                transform.position += _movementDirection * _moveSpeed;
                break;
        }
        
        SendMove();
    }

    public void GetMoveInfo(out Vector3 position)
    {
        position = transform.position;
    }
    
    private void SendMove()
    {
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"x", transform.position.x},
            {"y", transform.position.z}
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}