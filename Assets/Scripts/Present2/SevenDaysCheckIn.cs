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

    DateTime birthDay; // �ͤ���
    [SerializeField] int startDay , today;
    TimeSpan Span; // �ɶ��t

    [SerializeField] GameObject checkInBoxContainer;
    [SerializeField] List<CheckInBox> checkInBoxList= new List<CheckInBox>() ;

    Dictionary<int, string> myDictionary;

    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject birthdayCard;


    // �������X�ѵn���L
    

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
        

        OntheBirthDay(); // �b�ͤ��ѡA�}���~����s


        DeletePlayerPrefs();
        DeletePlayerPrefsAll();
    }

    private void DeletePlayerPrefsAll()
    {
        if (Input.GetKeyDown(KeyCode.P))
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