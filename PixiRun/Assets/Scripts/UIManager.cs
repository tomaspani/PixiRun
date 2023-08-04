using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, IObserver
{
    Dictionary<string, System.Action> _observerActions;

    IObservable _object;

    [SerializeField] TextMeshProUGUI _coinsText;
   
    int _coinsQty = 0;

    private void Awake()
    {
        FillActionsDictionary();
    }

    private void Start()
    {
        _object = FindObjectOfType<Model>();
        _object?.Subscribe(this);

        _coinsText.text = _coinsQty.ToString();
    }

    void FillActionsDictionary()
    {
        _observerActions = new Dictionary<string, System.Action>();
        _observerActions.Add("PickUpCoin", PickUpCoin);
    }

    void PickUpCoin()
    {
        _coinsQty++;
        Debug.Log("Agarro moneda");
        _coinsText.text =_coinsQty.ToString();
    }

    public void Notify(string action)
    {
        if (_observerActions.ContainsKey(action))
            _observerActions[action]();
    }
}
