using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, IObserver
{
    Dictionary<string, System.Action> _observerActions;

    IObservable _object;

    int _lives = 3;
    [SerializeField] private TextMeshProUGUI _livesText;

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
        _observerActions.Add("PickUpCoin", PickUpCoin);
    }

    void GetHit()
    {
        //cambiar ui????            
        _lives--;
        _livesText.SetText(_lives.ToString());
        Debug.Log("cambio en la ui vida!");
    }
   
    void PickUpHeart() //la implemento???
    {
        //cambiar ui
        Debug.Log("Agarro corazon");
    }


    void PickUpCoin()
    {
        //cambiar ui
        Debug.Log("Agarro moneda");
    }

    public void Notify(string action)
    {
        //System.Action myAction = _observerActions[action];

        //myAction();

        if (_observerActions.ContainsKey(action))
            _observerActions[action]();
    }
}
