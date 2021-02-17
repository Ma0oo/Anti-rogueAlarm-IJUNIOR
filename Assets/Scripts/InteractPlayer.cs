using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPlayer : MonoBehaviour
{
    [SerializeField] private float _distaceToInteract;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void Interact()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _distaceToInteract))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractive interactive))
                interactive.Interact();
        }
        Debug.DrawRay(ray.origin, ray.direction * _distaceToInteract, Color.red, 3);
    }
}
