using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    ScrollRect scrollRect;
    [SerializeField] float scrollSpeed = 20f;

    bool isScrolling;

    public bool IsScrolling 
    {
        get => isScrolling; 
        set => isScrolling = value;
    }

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        isScrolling = false;
    }

    void Update()
    {
        


        if (isScrolling)
        {
            if (scrollRect.verticalNormalizedPosition <= 0)
            {
                return;
            }

            float newPosition = scrollRect.verticalNormalizedPosition - (scrollSpeed * Time.deltaTime / scrollRect.content.rect.height);
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(newPosition);

        }
    }
}
