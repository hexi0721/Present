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

    #region �ƥ�
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
