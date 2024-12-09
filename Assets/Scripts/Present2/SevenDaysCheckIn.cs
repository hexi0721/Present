using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SevenDaysCheckIn : MonoBehaviour
{

    DateTime birthDay; // 生日當天
    [SerializeField] int startDay , endDay;
    TimeSpan Span; // 時間差


    [SerializeField] List<GameObject> checkInBox = new List<GameObject>();





    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject birthdayCard;

    private void Start()
    {
        continueButton.SetActive(false);
        // isReadyContinue = false;

        birthDay = new DateTime(DateTime.Now.Year, 12, 10, 0, 0, 0);
        startDay = birthDay.Day - 6;
        endDay = birthDay.Day;

        GameObject[] foundCheckInBoxobj = GameObject.FindGameObjectsWithTag("CheckInBox");
        // checkInBox = new List<GameObject>();
        foreach (GameObject i in foundCheckInBoxobj)
        {
            string strTmp  = i.transform.name.Substring(i.transform.name.Length - 1);
            // 須修復
            Debug.Log(strTmp);
            checkInBox.Insert(Convert.ToInt32(strTmp), i);
        }
    }

    private void Update()
    {
        // Span = birthDay.Subtract(DateTime.Now);

        OntheBirthDay(); // 在生日當天，開啟繼續按鈕

        

        

        





    }

    private void OntheBirthDay()
    {
        if (DateTime.Now.Day == birthDay.Day)
        {
            
            continueButton.SetActive(true);

            continueButton.GetComponent<Button>().onClick.AddListener(() => {

                birthdayCard.SetActive(true);
                gameObject.SetActive(false);


            });
        }

    }
}