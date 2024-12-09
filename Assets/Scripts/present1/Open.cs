using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Open : MonoBehaviour
{

    DateTime BirthDay; // �ͤ���
    TimeSpan Span; // �ɶ��t

    // TXT
    Text Timetxt; 
    string TimeString;
    Text Prompttxt;

    // �ʵe
    private Animator myAnimator;

    bool open , IsOpen; // �}���_�c�P�_

    float FadeTime = 1.0f; 
    public bool Fadein , Fadeout; // �H�J�H�X

    public CanvasGroup CanvasG , CanvasS;
    
    GameObject Bottom; // �e��

    
    void Start()
    {
        Timetxt = GameObject.Find("Time").GetComponent<Text>();
        Timetxt.text = "";
        Prompttxt = GameObject.Find("Prompt").GetComponent<Text>();
        Prompttxt.text = "���I���ù�";
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
                TimeString = Span.Duration().Days + "��" + Span.Duration().Hours + "��" + Span.Duration().Minutes + "��" + Span.Duration().Seconds + "��";
            }
            else if (Span.Duration().Hours > 0)
            {
                TimeString = Span.Duration().Hours + "��" + Span.Duration().Minutes + "��" + Span.Duration().Seconds + "��";
            }
            else if (Span.Duration().Minutes > 0)
            {
                TimeString = Span.Duration().Minutes + "��" + Span.Duration().Seconds + "��";
            }
            else if (Span.Duration().Seconds > 0)
            {
                TimeString = Span.Duration().Seconds + "��";
            }


            if (Span.TotalSeconds < 0)
            {
                open = true;
                Timetxt.text = " �_�c�w�i�}�� !";
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
                Timetxt.text = " �_�c���}�Ѿl�ɶ� " + TimeString;
            }


        }
        else
        {
            Timetxt.text = " �_�c�L�� !";
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
