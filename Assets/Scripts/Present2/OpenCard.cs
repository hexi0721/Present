using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenCard : MonoBehaviour
{

    public GameObject scratchZone , card , gestureSlip , rewardTreasure;
    
    EraseMask eraseMask;
    bool isFadeIn;
    

    private void Awake()
    {
        eraseMask = scratchZone.GetComponent<EraseMask>();
        card.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenCardFunc);

    }

    private void Start()
    {
        
        
        scratchZone.SetActive(false);
        gestureSlip.SetActive(false);
        rewardTreasure.SetActive(false);
        isFadeIn = false;
    }


    private void Update()
    {
        if (isFadeIn)
        {
            FadeIn();
        }

        if (gestureSlip.activeSelf && Input.GetMouseButtonDown(0))
        {
            gestureSlip.SetActive(false);

        }
    }

    public void OpenCardFunc()
    {
        card.transform.GetChild(0).gameObject.SetActive(false); // touchButton
        card.transform.GetChild(1).gameObject.SetActive(false); // openText

        GetComponent<Animator>().enabled = true;

        
    }

    public void AnimatorEnd() // 動畫結束時，觸發事件 
    {
        card.SetActive(false);
        scratchZone.SetActive(true);
        gestureSlip.SetActive(true);
        // rewardTreasure.SetActive(false);
        isFadeIn = true;
    }

    
    void FadeIn()
    {

        if(scratchZone.GetComponent<CanvasGroup>().alpha < 1)
        {
            scratchZone.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
        }
    }

}
