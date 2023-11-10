using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 5f;

    [Header("Components")]
    [SerializeField] private EnemyController _enemyController;

    private Vector3 _targetPosition = Vector3.zero;
    // private Vector3 _targetVelocity;
    private float _velocityMagnitude = 0;
    
    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void OnEnable()
    {
        // _enemyController.OnChangePosition += SetTargetPosition;
        _enemyController.OnChangeMovement += SetMovement;
    }

    private void OnDisable()
    {
        // _enemyController.OnChangePosition -= SetTargetPosition;
        _enemyController.OnChangeMovement -= SetMovement;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_velocityMagnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition,
                _velocityMagnitude * Time.deltaTime);
        }
        else
        {
            transform.position = _targetPosition;
        }
        
        // transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    public void SetMovement(Vector3 position, Vector3 velocity, float averageInterval)
    {
        _targetPosition = position + velocity * averageInterval;
        _velocityMagnitude = velocity.magnitude;
    }
}