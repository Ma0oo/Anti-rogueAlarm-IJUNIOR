using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractive
{
    [SerializeField] private Transform _pivotDoorPanel;
    [SerializeField] private Vector3 _openlocalEulerAngelPanel;
    [SerializeField] private Door _targetDool;
    [SerializeField] private TransitTriger _transitPlace;
    [SerializeField] private Transform _pointExit;
    [SerializeField] private UnityEvent _somewhoHasExit;

    private bool _isOpen = false;

    private void Awake()
    {
        if(!_targetDool)
        {
            Debug.LogWarning("У двери нет цели");
        }
        _transitPlace.Init(this);
    }

    public void Open()
    {
        if (!_isOpen)
        {
            _pivotDoorPanel.localEulerAngles = _openlocalEulerAngelPanel;
            _isOpen = true;
            _targetDool.Open();
        }
    }

    public void Close()
    {
        if (_isOpen)
        {
            _pivotDoorPanel.localEulerAngles = Vector3.zero;
            _isOpen = false;
            _targetDool.Close();
        }
    }

    public void DoTransitObject(GameObject transitObject)
    {
        _targetDool.GetTransitObject(transitObject);
    }

    public void GetTransitObject(GameObject transitObject)
    {
        transitObject.transform.position = _pointExit.position;
        transitObject.transform.eulerAngles = _pointExit.eulerAngles;
        _somewhoHasExit?.Invoke();
    }

    public void Interact()
    {
        if (_isOpen)
            Close();
        else
            Open();
    }
}
