using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    [SerializeField] protected float _duration;
    protected bool _isTrigger = false;
    public bool isTrigger { get { return _isTrigger; } }
    

    //[SerializeField] Effect _effect;

    //public abstract void ApplyEffect(Model m);

    protected abstract IEnumerator ApplyEffect(Model M);
}


public enum Effect
{
    Wings,
    CoinMagnet,
    AvoidCoinMagnet,
    Confusion
}
