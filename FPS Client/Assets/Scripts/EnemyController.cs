using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Action<Vector3> OnChangePosition;
    
    public void OnChange(List<DataChange> changes)
    {
        Vector3 position = transform.position;
        
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "x":
                    position.x = (float) dataChange.Value;
                    break;
                case "y":
                    position.z = (float) dataChange.Value;
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
    }
}