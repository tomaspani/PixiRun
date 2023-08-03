using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IObserver
{
    Dictionary<string, System.Action> _observerActions;

    IObservable _object;

    private void Awake()
    {
        FillActionsDictionary();
    }

    private void Start()
    {
        _object = FindObjectOfType<Model>();

        _object?.Subscribe(this);
    }

    void FillActionsDictionary()
    {
        _observerActions = new Dictionary<string, System.Action>();

        _observerActions.Add("GetHit", GetHit);
    }

    void GetHit()
    {
        //cambiar ui????
        Debug.Log("cambio en la ui vida!");
    }

    public void Notify(string action)
    {
        //System.Action myAction = _observerActions[action];

        //myAction();

        if (_observerActions.ContainsKey(action))
            _observerActions[action]();
    }
}
