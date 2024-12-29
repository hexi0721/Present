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

    [System.Serializable]
    public class CheckInBox
    {
        public Transform _transform;
        [SerializeField] int _int;

        public CheckInBox(Transform _transform, int _int = 0)
        {
            this._transform = _transform;
            this._int = _int;
        }

        public int Getint
        {
            get => _int;
        }

    }

    DateTime birthDay;
    [SerializeField] int startDay , span ;

    [SerializeField] GameObject checkInBoxContainer;
    [SerializeField] List<CheckInBox> checkInBoxList;
    public Sprite sprite;
    public int loginDay;

    Dictionary<int, string> myDictionary; 

    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject birthdayCard;


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

        

        birthdayCard.SetActive(false);
        continueButton.SetActive(false);

        // 修改生日時間
        birthDay = new DateTime(DateTime.Now.Year, 2, 17, 0, 0, 0);

        DateTime _today = DateTime.Today;
        if (birthDay <= new DateTime(DateTime.Now.Year, 1, 6, 0, 0, 0) && birthDay >= new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0) && _today.Month == 12) // 生日是1/6以前 1/1之後 而登入時間在12月
        {
            birthDay = birthDay.AddYears(1);
        }
        //

        DateTime startDay = birthDay.AddDays(-6);
        if (startDay <= _today && _today <= birthDay)
        {
            span = Mathf.Abs((_today - startDay).Days);
            string tmpString = "Day" + (span + 1).ToString();
            PlayerPrefs.SetInt(tmpString, 1);

            checkInBoxList = new List<CheckInBox>();
            loginDay = 0;
            for (int i = 0; i < checkInBoxContainer.transform.childCount; i++)
            {
                // 當天動畫
                if (span == i)
                {
                    loginDay += 1; // 當天登入
                    
                    checkInBoxContainer.transform.GetChild(i).GetComponent<Animator>().enabled = true;
                }

                // 檢測七天登入
                // Debug.Log($"第{i + 1}天 : " + PlayerPrefs.GetInt(myDictionary[i]));

                checkInBoxList.Add(new CheckInBox(checkInBoxContainer.transform.GetChild(i).transform, PlayerPrefs.GetInt(myDictionary[i])));
                CheckInBox tmpCheckInBox = checkInBoxList[i];

                // 除了今天外 其他有登入打勾
                if (tmpCheckInBox.Getint == 1 && span != i)
                {
                    loginDay += 1;
                    
                    tmpCheckInBox._transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                }

            }
        }
        else
        {
            DeletePlayerPrefsAll();
        }
        
        
        
        


    }

    private void Update()
    {
        

        OntheBirthDay(); // 在生日當天，開啟繼續按鈕

        // 測試用
        /*
        DeletePlayerPrefs();
        DeletePlayerPrefsAll();
        */
    }

    private void DeletePlayerPrefsAll()
    {
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("已清除所有 PlayerPrefs 資料");
        
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