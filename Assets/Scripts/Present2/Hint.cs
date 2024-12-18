using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{

    public Button hintButton;
    public Image scrollImgae;

    private void Start()
    {
        scrollImgae.gameObject.SetActive(false);
        hintButton.onClick.AddListener(() => { scrollImgae.gameObject.SetActive(!scrollImgae.gameObject.activeSelf); });
    }

}
