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
    [SerializeField] int startDay , today;
    // TimeSpan Span; // �ɶ��t

    [SerializeField] GameObject checkInBoxContainer;
    [SerializeField] List<CheckInBox> checkInBoxList= new List<CheckInBox>();
    public Sprite sprite;

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

        // �ק�ͤ�ɶ�
        birthDay = new DateTime(DateTime.Now.Year, 12, 16, 0, 0, 0);
        startDay = birthDay.Day - 6;
        today = DateTime.Today.Day;
        //
        

        if(startDay <= today && today <= birthDay.Day)
        {
            today -= startDay;
        }/*
        else
        {
            DeletePlayerPrefsAll();
        }
        */
        
        string tmpString = "Day" + (today + 1).ToString();
        PlayerPrefs.SetInt(tmpString , 1);

        for (int i = 0; i < checkInBoxContainer.transform.childCount; i++)
        {
            // ��Ѱʵe
            if (today == i) 
            {
                checkInBoxContainer.transform.GetChild(i).GetComponent<Animator>().enabled = true;
            }

            // �˴��C�ѵn�J
            Debug.Log($"��{i + 1}�� : " + PlayerPrefs.GetInt(myDictionary[i]));
            //
            checkInBoxList.Add(new CheckInBox(checkInBoxContainer.transform.GetChild(i).transform , PlayerPrefs.GetInt(myDictionary[i])));

            var tmpCheckInBox = checkInBoxList[i];

            if (tmpCheckInBox.Getint == 1 && today != i)
            {
                tmpCheckInBox._transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            }
                
        }
        /*
        foreach(var CheckInBox in checkInBoxList)
        {
            if (CheckInBox.Getint == 1 && )
            {
                CheckInBox._transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            }
        }
        */        
    }

    private void Update()
    {
        

        OntheBirthDay(); // �b�ͤ��ѡA�}���~����s


        DeletePlayerPrefs();
        DeletePlayerPrefsAll();
    }

    private void DeletePlayerPrefsAll()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("�w�M���Ҧ� PlayerPrefs ���");
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
}