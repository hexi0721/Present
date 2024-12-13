using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


[System.Serializable]
public class CheckInBox
{
    [SerializeField] Transform _transform;
    [SerializeField] int _int;

    public CheckInBox(Transform _transform, int _int = 0)
    {
        this._transform = _transform;
        this._int = _int;
    }

}



public class SevenDaysCheckIn : MonoBehaviour
{

    DateTime birthDay; // 生日當天
    [SerializeField] int startDay , today;
    TimeSpan Span; // 時間差

    [SerializeField] GameObject checkInBoxContainer;
    [SerializeField] List<CheckInBox> checkInBoxList= new List<CheckInBox>() ;

    Dictionary<int, string> myDictionary;

    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject birthdayCard;


    // 紀錄哪幾天登錄過
    

    private void Start()
    {

        myDictionary = new Dictionary<int, string> {
            { 0 , "Day1"} ,
            { 1 , "Day2"} ,
            { 2 , "Day3"} ,
            { 3 , "Day4"} ,
            { 4 , "Day5"} ,
            { 5 , "Day6"} ,
            { 6 , "Day7"}
        };


        continueButton.SetActive(false);

        

        birthDay = new DateTime(DateTime.Now.Year, 12, 19, 0, 0, 0);
        startDay = birthDay.Day - 6;
        today = DateTime.Today.Day;

        

        if(startDay <= today && today <= birthDay.Day)
        {
            today -= startDay;
        }/*
        else
        {
            DeletePlayerPrefsAll();
        }
        */
        //Span = birthDay.Subtract(DateTime.Now);
        //Debug.Log(Span.Days +  " " + (7 - Span.Days + 1));
        PlayerPrefs.SetInt("Day" +  today + 1, 1);
        Debug.Log(PlayerPrefs.GetInt("Day" + today + 1));
        for (int i = 0; i < checkInBoxContainer.transform.childCount; i++)
        {
            Debug.Log(myDictionary[i]);
            Debug.Log(PlayerPrefs.GetInt("Day" + today + 1) + " test2");
            checkInBoxList.Add(new CheckInBox(checkInBoxContainer.transform.GetChild(i).transform , PlayerPrefs.GetInt(myDictionary[i])));
        }
        
    }

    private void Update()
    {
        

        OntheBirthDay(); // 在生日當天，開啟繼續按鈕


        DeletePlayerPrefs();
        DeletePlayerPrefsAll();
    }

    private void DeletePlayerPrefsAll()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("已清除所有 PlayerPrefs 資料");
        }
    }

    private void DeletePlayerPrefs()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (PlayerPrefs.HasKey("Day1"))
            {
                PlayerPrefs.DeleteKey("Day1");
                PlayerPrefs.Save();
                Debug.Log("已刪除 Day1 的資料");
            }
            else
            {
                Debug.Log("Day1 鍵不存在");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerPrefs.HasKey("Day2"))
            {
                PlayerPrefs.DeleteKey("Day2");
                PlayerPrefs.Save();
                Debug.Log("已刪除 Day2 的資料");
            }
            else
            {
                Debug.Log("Day2 鍵不存在");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (PlayerPrefs.HasKey("Day3"))
            {
                PlayerPrefs.DeleteKey("Day3");
                PlayerPrefs.Save();
                Debug.Log("已刪除 Day3 的資料");
            }
            else
            {
                Debug.Log("Day3 鍵不存在");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (PlayerPrefs.HasKey("Day4"))
            {
                PlayerPrefs.DeleteKey("Day4");
                PlayerPrefs.Save();
                Debug.Log("已刪除 Da4 的資料");
            }
            else
            {
                Debug.Log("Day4 鍵不存在");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (PlayerPrefs.HasKey("Day5"))
            {
                PlayerPrefs.DeleteKey("Day5");
                PlayerPrefs.Save();
                Debug.Log("已刪除 Day5 的資料");
            }
            else
            {
                Debug.Log("Day5 鍵不存在");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (PlayerPrefs.HasKey("Day6"))
            {
                PlayerPrefs.DeleteKey("Day6");
                PlayerPrefs.Save();
                Debug.Log("已刪除 Day6 的資料");
            }
            else
            {
                Debug.Log("Day6 鍵不存在");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (PlayerPrefs.HasKey("Day7"))
            {
                PlayerPrefs.DeleteKey("Day7");
                PlayerPrefs.Save();
                Debug.Log("已刪除 Day7 的資料");
            }
            else
            {
                Debug.Log("Day7 鍵不存在");
            }
        }
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