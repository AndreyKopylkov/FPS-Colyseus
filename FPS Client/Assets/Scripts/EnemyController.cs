using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<float> _receiveTimeIntervals = new List<float> {0, 0, 0, 0, 0};
    private float _lastReceiveTime = 0f;

    private float _averageInterval
    {
        get
        {
            int receiveTimeIntervalsCount = _receiveTimeIntervals.Count;
            float summ = 0;

            for (int i = 0; i < receiveTimeIntervalsCount; i++)
            {
                summ += _receiveTimeIntervals[i];
            }

            return summ / receiveTimeIntervalsCount;
        }
    }
    
    public Action<Vector3> OnChangePosition;
    public Action<Vector3, Vector3, float> OnChangeMovement;

    private void SaveReceiveTime()
    {
        float interval = Time.time - _lastReceiveTime;
        _lastReceiveTime = Time.time;
        _receiveTimeIntervals.Add(interval);
        _receiveTimeIntervals.RemoveAt(0);
    }

    public void OnChange(List<DataChange> changes)
    {
        SaveReceiveTime();
        
        Vector3 position = transform.position;
        Vector3 velocity = Vector3.zero;
        
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "pX":
                    position.x = (float) dataChange.Value;
                    break;
                case "pY":
                    position.y = (float) dataChange.Value;
                    break;
                case "pZ":
                    position.z = (float) dataChange.Value;
                    break;
                case "vX":
                    velocity.z = (float) dataChange.Value;
                    break;
                case "vY":
                    velocity.y = (float) dataChange.Value;
                    break;
                case "vZ":
                    velocity.z = (float) dataChange.Value;
                    break;
                default:
                    Debug.LogWarning($"{dataChange.Field} - не обрабатывается");
                    break;
            }

            // if (dataChange.Field == "x")
            // {
            //     position.x = (float) dataChange.Value;
            // }
            // if (dataChange.Field == "y")
            // {
            //     position.z = (float) dataChange.Value;
            // }
        }

        OnChangePosition?.Invoke(position);
        OnChangeMovement?.Invoke(position, velocity, _averageInterval);
    }
}