using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 5f;

    [Header("Components")]
    [SerializeField] private EnemyController _enemyController;

    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void OnEnable()
    {
        _enemyController.OnChangePosition += SetTargetPosition;
    }

    private void OnDisable()
    {
        _enemyController.OnChangePosition -= SetTargetPosition;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }
}