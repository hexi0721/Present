using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenCard : MonoBehaviour
{

    public GameObject scratchZone , card , gestureSlip;

    bool isFadeIn;
    

    private void Awake()
    {
        card.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenCardFunc);

    }

    private void Start()
    {
        scratchZone.SetActive(false);
        gestureSlip.SetActive(false);
        isFadeIn = false;
    }


    private void Update()
    {
        if (isFadeIn)
        {
            FadeIn();
        }
    }

    public void OpenCardFunc()
    {
        card.transform.GetChild(0).gameObject.SetActive(false); // touchButton
        card.transform.GetChild(1).gameObject.SetActive(false); // openText

        GetComponent<Animator>().enabled = true;

        // StartCoroutine(ExampleCoroutine());
        
        

        
    }

    /*
    IEnumerator ExampleCoroutine()
    {

        yield return new WaitForSeconds(3.0f);


        card.SetActive(false);
        scratchZone.SetActive(true);

        isFadeIn = true;
    }
    */

    public void AnimatorEnd() // 動畫結束時，觸發事件 
    {
        card.SetActive(false);
        scratchZone.SetActive(true);
        gestureSlip.SetActive(true);
        isFadeIn = true;
    }

    
    void FadeIn()
    {

        if(scratchZone.GetComponent<CanvasGroup>().alpha == 1)
        {
            return;
        }

        scratchZone.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
        
    }

}
