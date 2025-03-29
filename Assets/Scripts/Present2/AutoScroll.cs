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
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(newPosition); // �� value �p�� 0 �ɡAMathf.Clamp01 �|��^ 0�F�� value �j�� 1 �ɡA�|��^ 1�F�_�h�A��^ value �����C

        }
    }
}
