using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Open : MonoBehaviour
{

    DateTime BirthDay;
    TimeSpan Span;

    Text Timetxt;
    string TimeString;
    Text Prompttxt;
    

    private Animator myAnimator;

    bool open;

    float FadeTime = 1.0f;
    bool b = true;

    // Start is called before the first frame update
    void Start()
    {
        Timetxt = GameObject.Find("Time").GetComponent<Text>();
        Timetxt.text = "";
        Prompttxt = GameObject.Find("Prompt").GetComponent<Text>();
        Prompttxt.text = "請點擊螢幕";
        Prompttxt.color = new Color(1.0f, 0.8470588f, 0.3607843f, 1.0f);

        //BirthDay = new DateTime(DateTime.Now.Year, 10, 08, 17, 51, 0);
        BirthDay = new DateTime(DateTime.Now.Year, 11, 24, 0, 0, 0);

        open = false;

        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Prompttxt.color.a > 0.0f)
        {
            
            Prompttxt.color = Color.Lerp(Prompttxt.color, new(1.0f, 0.8470588f, 0.3607843f, 0.0f), FadeTime * Time.deltaTime);
            Debug.Log(Prompttxt.color.a);
            
        }
        else
        {

        }

        
        
        
        

        


        if (Input.GetMouseButtonDown(0) && open)
        {
            myAnimator.SetTrigger("Open");
            
        }

        Span = DateTime.Now.Subtract(BirthDay);


        if (Span.Duration().Days <= 0)
        {
            TimeString = Span.Duration().Hours + "時" + Span.Duration().Minutes + "分" + Span.Duration().Seconds + "秒";
        }
        
        if (Span.Duration().Hours <= 0)
        {
            TimeString = Span.Duration().Minutes + "分" + Span.Duration().Seconds + "秒";
        }

        if (Span.Duration().Minutes <= 0)
        {
            TimeString = Span.Duration().Seconds + "秒";
        }


        if(Span.TotalSeconds > 0)
        {
            open = true;
            Timetxt.text = " 寶箱已可開啟 ";
        }
        else if(!open)
        {
            Timetxt.text = " 寶箱打開剩餘時間 " + TimeString;
        }


        

    }
}
