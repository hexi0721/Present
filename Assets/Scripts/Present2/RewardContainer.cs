using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardContainer : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void CakeIdleEnd()
    {
        animator.SetBool("IdleEnd", true);

    }
}
