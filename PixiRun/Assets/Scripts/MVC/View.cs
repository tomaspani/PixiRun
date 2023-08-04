using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    Animator _animator;
    [SerializeField] GameObject _wings;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void OnJump()
    {
        _animator.SetTrigger("jump");
    }

    public void SineMovement()
    {
        _wings.SetActive(true);
        _animator.SetBool("isFlying", true);
    }
    
    public void NormalMovement()
    {
        _wings.SetActive(false);
        _animator.SetBool("isFlying", false);
    }

    public void OnDown()
    {
        _animator.SetTrigger("down");
    }

    public void InvertedMovement()
    {

    }
}
