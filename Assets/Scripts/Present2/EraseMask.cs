using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EraseMask: MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //是否擦除
    public bool isStartEraser;
    //是否擦拭結束
    public bool isEndEraser;
    //開始事件
    //public Action eraserStartEvent;
    //結束事件
    //public Action eraserEndEvent;
    public RawImage uiTex;
    public Image image;
    Texture2D tex;
    Texture2D MyTex;
    int mWidth;
    int mHeight;
    [Header("筆刷大小")]
    public int brushSize;
    [Header("刮刮樂比例")]
    public int rate;
    float maxColorA;
    float colorA;
    
    void Awake()
    {
        tex = (Texture2D)uiTex.mainTexture;
        MyTex = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        MyTex.SetPixels(tex.GetPixels());
        mWidth = MyTex.width;
        mHeight = MyTex.height;
        
        MyTex.Apply();
        uiTex.texture = MyTex;
        maxColorA = MyTex.GetPixels().Length;
        colorA = 0;
        isEndEraser = false;
        isStartEraser = false;

        // 將RawImage變得跟Image一樣大
        float scaleW = image.GetComponent<RectTransform>().rect.width / mWidth;
        float scaleH = image.GetComponent<RectTransform>().rect.height / mHeight;
        uiTex.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scaleW , scaleH , 0);

        
    }
    /// <summary>
    /// 貝塞爾平滑
    /// </summary>
    /// <param name="start">起點</param>
    /// <param name="mid">中點</param>
    /// <param name="end">終點</param>
    /// <param name="segments">段數</param>
    /// <returns></returns>
    public Vector2[] Beizier(Vector2 start, Vector2 mid, Vector2 end, int segments)
    {
        float d = 1f / segments;
        Vector2[] points = new Vector2[segments - 1];
        for (int i = 0; i < points.Length; i++)
        {
            float t = d * (i + 1);
            points[i] = (1 - t) * (1 - t) * mid + 2 * t * (1 - t) * start + t * t * end;
        }
        List<Vector2> rps = new List<Vector2>();
        rps.Add(mid);
        rps.AddRange(points);
        rps.Add(end);
        return rps.ToArray();
    }

    bool twoPoints = false;
    Vector2 lastPos;//最後一個點
    Vector2 penultPos;//倒數第二個點
    float radius = 12f;
    float distance = 1f;
    #region 事件
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isEndEraser) { return; }

        penultPos = eventData.position;
        CheckPoint(penultPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isEndEraser) { return; }
        if (twoPoints && Vector2.Distance(eventData.position, lastPos) > distance)//如果兩次記錄的鼠標坐標距離大於一定的距離，開始記錄鼠標的點
        {
            Vector2 pos = eventData.position;
            float dis = Vector2.Distance(lastPos, pos);

            CheckPoint(eventData.position);
            int segments = (int)(dis / radius);//計算出平滑的段數                                              
            segments = Mathf.Clamp(segments , 1 , 10);
                
            Vector2[] points = Beizier(penultPos, lastPos, pos, segments);//進行貝塞爾平滑
            for (int i = 0; i < points.Length; i++)
            {
                CheckPoint(points[i]);
            }
            lastPos = pos;
            if (points.Length > 2)
                penultPos = points[points.Length - 2];
        }
        else
        {
            twoPoints = true;
            lastPos = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isEndEraser) { return; }

        twoPoints = false;
    }


    #endregion
    void CheckPoint(Vector3 pScreenPos)
    {

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pScreenPos);
        Vector3 localPos = uiTex.gameObject.transform.InverseTransformPoint(worldPos);

        if (localPos.x > -mWidth / 2 && localPos.x < mWidth / 2 && localPos.y > -mHeight / 2 && localPos.y < mHeight / 2)
        {
            for (int i = (int)localPos.x - brushSize; i < (int)localPos.x + brushSize; i++)
            {
                for (int j = (int)localPos.y - brushSize; j < (int)localPos.y + brushSize; j++)
                {
                    if (Mathf.Pow(i - localPos.x, 2) + Mathf.Pow(j - localPos.y, 2) > Mathf.Pow(brushSize, 2))
                        continue;
                    if (i < 0) { if (i < -mWidth / 2) { continue; } }
                    if (i > 0) { if (i > mWidth / 2) { continue; } }
                    if (j < 0) { if (j < -mHeight / 2) { continue; } }
                    if (j > 0) { if (j > mHeight / 2) { continue; } }

                    Color col = MyTex.GetPixel(i + (int)mWidth / 2, j + (int)mHeight / 2);
                    if (col.a != 0f)
                    {
                        col.a = 0.0f;
                        colorA++;
                        MyTex.SetPixel(i + (int)mWidth / 2, j + (int)mHeight / 2, col);
                    }
                }
            }
            //開始刮的時候 去判斷進度
            
            if (!isStartEraser)
            {
                isStartEraser = true;
                InvokeRepeating("getTransparentPercent", 0f, 0.2f);
                //eraseEvent.Invoke();
                /*
                if (eraserStartEvent != null)
                    eraserStartEvent.Invoke();
                */
            }
            
        
            MyTex.Apply();
        }
    }
    double fate;
    /// <summary> 
    /// 檢測當前刮刮卡 進度
    /// </summary>
    /// <returns></returns>
    public void getTransparentPercent()
    {
        if (isEndEraser) { return; }
        fate = colorA / maxColorA * 100;
        fate = (float)Math.Round(fate, 2);
        // Debug.Log("當前百分比: " + fate);
        if (fate >= rate)
        {
            isEndEraser = true;
            CancelInvoke("getTransparentPercent");
            uiTex.gameObject.SetActive(false);
            //觸發結束事件
            /*
            if (eraserEndEvent != null)
                eraserEndEvent.Invoke();
            */
        }
    }
}