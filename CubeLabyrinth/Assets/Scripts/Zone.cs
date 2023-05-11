using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Zone : MonoBehaviour
{
    //private GameManager _gameManager;

    public event Action OnTriggerAction;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerAction?.Invoke();
    }
}
