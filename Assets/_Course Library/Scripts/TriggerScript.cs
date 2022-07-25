using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] int trgNum;

    public event Action<int> OnAddToList;
    public event Action<int> OnRemoveFromList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            OnAddToList?.Invoke(trgNum);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            OnRemoveFromList?.Invoke(trgNum);
    }
}
