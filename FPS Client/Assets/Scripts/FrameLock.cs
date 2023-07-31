using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrameLock : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;
    
    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}
