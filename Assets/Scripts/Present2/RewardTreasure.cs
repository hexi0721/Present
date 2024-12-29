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
    Image reward;

    private void Start()
    {
        animator = GetComponent<Animator>();
        reward = rewardContainer.transform.GetChild(1).GetComponent<Image>();
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
        // Debug.Log(sevenDaysCheckIn.loginDay);
        switch (sevenDaysCheckIn.loginDay)
        {
            case 1:
            case 2:

                reward.sprite = drink[0];
                break;

            case 3:
            case 4:

                reward.sprite = drink[1];
                break;

            case 5:
            case 6:
                reward.sprite = drink[2];
                break;

            case 7:

                reward.sprite = drink[3];
                break;
        }
        
        
        
    }


}
