using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Open : MonoBehaviour
{

    DateTime BirthDay; // 生日當天
    TimeSpan Span; // 時間差

    // TXT
    Text Timetxt; 
    string TimeString;
    Text Prompttxt;

    // 動畫
    private Animator myAnimator;

    bool open , IsOpen; // 開啟寶箱判斷

    float FadeTime = 1.0f; 
    public bool Fadein , Fadeout; // 淡入淡出

    public CanvasGroup CanvasG , CanvasS;
    
    GameObject Bottom; // 容器

    
    void Start()
    {
        Timetxt = GameObject.Find("Time").GetComponent<Text>();
        Timetxt.text = "";
        Prompttxt = GameObject.Find("Prompt").GetComponent<Text>();
        Prompttxt.text = "請點擊螢幕";
        Prompttxt.gameObject.SetActive(false);

        Fadein = true;
        Fadeout = false;
        
        BirthDay = new DateTime(DateTime.Now.Year, 12, 8, 0, 0, 0);

        IsOpen = true;
        open = false;

        

        Bottom = GameObject.Find("Bottom");
        

        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {

        Span = BirthDay.Subtract(DateTime.Now);

        


        if (Span.Days >= -1)
        {
            if (Input.GetMouseButtonDown(0) && open && IsOpen)
            {
                myAnimator.SetTrigger("Open");
                Bottom.SetActive(false);
                
                AudioManager.Instance.PlayAudio(AudioManager.Instance.OpenAudio);
                IsOpen = false;

                
            }

            if (CanvasS.alpha < 1 && !IsOpen)
            {
                CanvasS.alpha += 0.4f * Time.deltaTime;

            }

            if (Span.Duration().Days > 0)
            {
                TimeString = Span.Duration().Days + "天" + Span.Duration().Hours + "時" + Span.Duration().Minutes + "分" + Span.Duration().Seconds + "秒";
            }
            else if (Span.Duration().Hours > 0)
            {
                TimeString = Span.Duration().Hours + "時" + Span.Duration().Minutes + "分" + Span.Duration().Seconds + "秒";
            }
            else if (Span.Duration().Minutes > 0)
            {
                TimeString = Span.Duration().Minutes + "分" + Span.Duration().Seconds + "秒";
            }
            else if (Span.Duration().Seconds > 0)
            {
                TimeString = Span.Duration().Seconds + "秒";
            }


            if (Span.TotalSeconds < 0)
            {
                open = true;
                Timetxt.text = " 寶箱已可開啟 !";
                Prompttxt.gameObject.SetActive(true);

                if (Fadein)
                {
                    if (CanvasG.alpha < 1)
                    {
                        CanvasG.alpha += FadeTime * Time.deltaTime;

                        if (CanvasG.alpha >= 1)
                        {
                            Fadein = false;
                            FadeOut();
                        }
                    }
                }

                if (Fadeout)
                {
                    if (CanvasG.alpha >= 0)
                    {
                        CanvasG.alpha -= FadeTime * Time.deltaTime;

                        if (CanvasG.alpha == 0)
                        {
                            Fadeout = false;
                            FadeIn();
                        }
                    }
                }
            }
            else if (!open)
            {
                Timetxt.text = " 寶箱打開剩餘時間 " + TimeString;
            }


        }
        else
        {
            Timetxt.text = " 寶箱過期 !";
        }


    }

    void FadeIn()
    {
        Fadein = true;
    }

    void FadeOut()
    {
        Fadeout = true;
    }
}
