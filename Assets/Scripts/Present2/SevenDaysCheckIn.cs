using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Globalization;
using TMPro;

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

    public class TimeFetcher 
    {
        public DateTime GetNetworkTime()
        {
            const string url = "https://www.google.com";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    string dateHeader = response.Headers["date"];
                    if (DateTime.TryParseExact(
                        dateHeader,
                        "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AdjustToUniversal,
                        out DateTime networkDateTime))
                    {
                        return networkDateTime.ToLocalTime();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"獲取網路時間失敗：{e.Message}");
                
            }

            return new DateTime(); // 如果失敗，返回一個默認值
        }
    }



    DateTime birthDay;
    [SerializeField] int span ;


    [SerializeField] GameObject checkInBoxContainer;
    [SerializeField] GameObject hintContainer;
    [SerializeField] List<CheckInBox> checkInBoxList;
    public Sprite sprite;
    public int loginDay;

    Dictionary<int, string> myDictionary; 

    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject birthdayCard;
    [SerializeField] TextMeshProUGUI checkInText;
    Color currentColor = Color.red; // 初始為紅色
    [SerializeField] int colorState = 0;
    [SerializeField] Text timeIncorrectWarningText;

    TimeFetcher timeFetcher = new TimeFetcher(); // class TimeFetcher 

    [SerializeField] Button debugForceOpen;

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

        birthDay = new DateTime(DateTime.Now.Year, 3, 29, 0, 0, 0); // 修改生日時間

        //DateTime _today = DateTime.Today;
        DateTime _today = timeFetcher.GetNetworkTime();

        if(_today.Year == 1) // 獲取網路時間失敗
        {
            checkInBoxContainer.SetActive(false);
            hintContainer.SetActive(false);
            timeIncorrectWarningText.text = "開啟網路，獲取正確時間，難不成你想偷作弊?";
            //Debug.Log($"網路時間是：{_today}");
            return;
        }
        
        if (birthDay <= new DateTime(DateTime.Now.Year, 1, 6, 0, 0, 0) && birthDay >= new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0) && _today.Month == 12) // 生日是1/6以前 1/1之後 而登入時間在12月
        {
            birthDay = birthDay.AddYears(1);
        }
        
        DateTime startDay = birthDay.AddDays(-6);
        if (startDay <= _today && _today.Day <= birthDay.Day)
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

        // 測試用
        debugForceOpen.onClick.AddListener(() => 
        {
            birthDay = DateTime.Now;

        });

    }

    private void Update()
    {
        TitleRGB();
        OntheBirthDay(); // 在生日當天，開啟繼續按鈕

        // 測試用
        // DeletePlayerPrefs();
        // DeletePlayerPrefsAll();

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

    void TitleRGB()
    {
        float delta = Time.deltaTime ;

        switch (colorState)
        {
            case 0: // R 增加
                
                currentColor.r += delta;
                if (currentColor.r >= 1f)
                {
                    currentColor.r = 1f;
                    
                    colorState = 1; 
                }
                break;

            case 1: // G 減少
                
                currentColor.g -= delta;
                if (currentColor.g <= 0.5f)
                {
                    
                    currentColor.g = 0.5f;
                    colorState = 2; 
                }
                break;

            case 2: // B 增加

                currentColor.b += delta;
                if (currentColor.b >= 1f)
                {

                    currentColor.b = 1f;
                    colorState = 3;
                }
                break;

            case 3: // R 減少

                currentColor.r -= delta;
                if (currentColor.r <= 0.5f)
                {
                    currentColor.r = 0.5f;

                    colorState = 4;
                }
                break;

            case 4: // G 增加

                currentColor.g += delta;
                if (currentColor.g >= 1f)
                {

                    currentColor.g = 1f;
                    colorState = 5;
                }
                break;

            case 5: // B 減少

                currentColor.b -= delta;
                if (currentColor.b <= 0.5f)
                {

                    currentColor.b = 0.5f;
                    colorState = 0;
                }
                break;

        }

        checkInText.color = currentColor;
    }

}