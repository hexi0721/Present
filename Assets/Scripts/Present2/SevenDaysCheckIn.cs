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
                Debug.LogError($"��������ɶ����ѡG{e.Message}");
                
            }

            return new DateTime(); // �p�G���ѡA��^�@���q�{��
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
    Color currentColor = Color.red; // ��l������
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

        birthDay = new DateTime(DateTime.Now.Year, 3, 29, 0, 0, 0); // �ק�ͤ�ɶ�

        //DateTime _today = DateTime.Today;
        DateTime _today = timeFetcher.GetNetworkTime();

        if(_today.Year == 1) // ��������ɶ�����
        {
            checkInBoxContainer.SetActive(false);
            hintContainer.SetActive(false);
            timeIncorrectWarningText.text = "�}�Һ����A������T�ɶ��A�������A�Q���@��?";
            //Debug.Log($"�����ɶ��O�G{_today}");
            return;
        }
        
        if (birthDay <= new DateTime(DateTime.Now.Year, 1, 6, 0, 0, 0) && birthDay >= new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0) && _today.Month == 12) // �ͤ�O1/6�H�e 1/1���� �ӵn�J�ɶ��b12��
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
                // ��Ѱʵe
                if (span == i)
                {
                    loginDay += 1; // ��ѵn�J
                    
                    checkInBoxContainer.transform.GetChild(i).GetComponent<Animator>().enabled = true;
                }

                // �˴��C�ѵn�J
                // Debug.Log($"��{i + 1}�� : " + PlayerPrefs.GetInt(myDictionary[i]));

                checkInBoxList.Add(new CheckInBox(checkInBoxContainer.transform.GetChild(i).transform, PlayerPrefs.GetInt(myDictionary[i])));
                CheckInBox tmpCheckInBox = checkInBoxList[i];

                // ���F���ѥ~ ��L���n�J����
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

        // ���ե�
        debugForceOpen.onClick.AddListener(() => 
        {
            birthDay = DateTime.Now;

        });

    }

    private void Update()
    {
        TitleRGB();
        OntheBirthDay(); // �b�ͤ��ѡA�}���~����s

        // ���ե�
        // DeletePlayerPrefs();
        // DeletePlayerPrefsAll();

    }

    private void DeletePlayerPrefsAll()
    {
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("�w�M���Ҧ� PlayerPrefs ���");
        
    }

    private void DeletePlayerPrefs()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (PlayerPrefs.HasKey("Day1"))
            {
                PlayerPrefs.DeleteKey("Day1");
                PlayerPrefs.Save();
                Debug.Log("�w�R�� Day1 �����");
            }
            else
            {
                Debug.Log("Day1 �䤣�s�b");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerPrefs.HasKey("Day2"))
            {
                PlayerPrefs.DeleteKey("Day2");
                PlayerPrefs.Save();
                Debug.Log("�w�R�� Day2 �����");
            }
            else
            {
                Debug.Log("Day2 �䤣�s�b");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (PlayerPrefs.HasKey("Day3"))
            {
                PlayerPrefs.DeleteKey("Day3");
                PlayerPrefs.Save();
                Debug.Log("�w�R�� Day3 �����");
            }
            else
            {
                Debug.Log("Day3 �䤣�s�b");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (PlayerPrefs.HasKey("Day4"))
            {
                PlayerPrefs.DeleteKey("Day4");
                PlayerPrefs.Save();
                Debug.Log("�w�R�� Da4 �����");
            }
            else
            {
                Debug.Log("Day4 �䤣�s�b");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (PlayerPrefs.HasKey("Day5"))
            {
                PlayerPrefs.DeleteKey("Day5");
                PlayerPrefs.Save();
                Debug.Log("�w�R�� Day5 �����");
            }
            else
            {
                Debug.Log("Day5 �䤣�s�b");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (PlayerPrefs.HasKey("Day6"))
            {
                PlayerPrefs.DeleteKey("Day6");
                PlayerPrefs.Save();
                Debug.Log("�w�R�� Day6 �����");
            }
            else
            {
                Debug.Log("Day6 �䤣�s�b");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (PlayerPrefs.HasKey("Day7"))
            {
                PlayerPrefs.DeleteKey("Day7");
                PlayerPrefs.Save();
                Debug.Log("�w�R�� Day7 �����");
            }
            else
            {
                Debug.Log("Day7 �䤣�s�b");
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
            case 0: // R �W�[
                
                currentColor.r += delta;
                if (currentColor.r >= 1f)
                {
                    currentColor.r = 1f;
                    
                    colorState = 1; 
                }
                break;

            case 1: // G ���
                
                currentColor.g -= delta;
                if (currentColor.g <= 0.5f)
                {
                    
                    currentColor.g = 0.5f;
                    colorState = 2; 
                }
                break;

            case 2: // B �W�[

                currentColor.b += delta;
                if (currentColor.b >= 1f)
                {

                    currentColor.b = 1f;
                    colorState = 3;
                }
                break;

            case 3: // R ���

                currentColor.r -= delta;
                if (currentColor.r <= 0.5f)
                {
                    currentColor.r = 0.5f;

                    colorState = 4;
                }
                break;

            case 4: // G �W�[

                currentColor.g += delta;
                if (currentColor.g >= 1f)
                {

                    currentColor.g = 1f;
                    colorState = 5;
                }
                break;

            case 5: // B ���

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