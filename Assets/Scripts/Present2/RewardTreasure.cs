using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardTreasure : MonoBehaviour , IPointerClickHandler
{

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    #region ¨Æ¥ó
    public void OnPointerClick(PointerEventData eventData)
    {
        OpenRewardTreasure();
    }


    #endregion

    public void OpenRewardTreasure()
    {
        animator.SetBool("IsOpen", true);

    }


}
