using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    ScrollRect scrollRect;
    [SerializeField] float scrollSpeed;

    bool isScrolling;

    public void SetUp(bool isScrolling)
    {
        this.isScrolling = isScrolling;
    }

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    void Update()
    {
        if (isScrolling)
        {
            if (scrollRect.verticalNormalizedPosition <= 0)
            {
                return;
            }

            float newPosition = scrollRect.verticalNormalizedPosition - scrollSpeed * Time.deltaTime;// / scrollRect.content.rect.height);
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(newPosition); // 當 value 小於 0 時，Mathf.Clamp01 會返回 0；當 value 大於 1 時，會返回 1；否則，返回 value 本身。

        }
    }
}
