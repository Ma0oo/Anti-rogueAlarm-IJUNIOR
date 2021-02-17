using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitPlace : MonoBehaviour
{
    [SerializeField] private Door _myDoor;


    private void OnTriggerEnter(Collider other)
    {
        _myDoor.DoTransitObject(other.gameObject);
    }
}
