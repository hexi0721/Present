using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardTreasure : MonoBehaviour , IPointerClickHandler
{

    Animator animator;
    public List<Sprite> drink = new List<Sprite>();
        
    public SevenDaysCheckIn sevenDaysCheckIn; // SevenDaysCheckIn script
    public GameObject rewardContainer;
    public Image reward;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    #region ¨Æ¥ó
    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetBool("IsOpen", true);

    }


    #endregion

    public void OpenRewardTreasure()
    {

        rewardContainer.SetActive(true);

        switch (sevenDaysCheckIn.loginDay)
        {
            case 1:

                reward.sprite = drink[0];
                break;

            case 3:

                reward.sprite = drink[1];
                break;

            case 5:

                reward.sprite = drink[2];
                break;

            case 7:

                reward.sprite = drink[3];
                break;
        }
        
        
        
    }


}
