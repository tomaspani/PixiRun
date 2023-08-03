using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    Animator _animator;

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
        //cambiar de animacion pq esta volando
        _animator.SetBool("isFlying", true);
    }
    
    public void NormalMovement()
    {
        _animator.SetBool("isFlying", false);
    }

    public void InvertedMovement()
    {

    }
}
