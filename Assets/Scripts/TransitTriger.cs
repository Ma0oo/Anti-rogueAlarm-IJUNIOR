using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitTriger : MonoBehaviour
{
    private Door _door;

    public void Init(Door door)
    {
        _door = door;
    }

    private void OnTriggerEnter(Collider other)
    {
        _door.DoTransitObject(other.gameObject);
    }
}
