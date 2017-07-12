using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test_view : MonoBehaviour
{
    public RectTransform tran;
    public Canvas cam;
    public RectTransform obj;
    void Start()
    {


    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos= CoordinateUtility.ScreenToUIPoint(cam,tran);
            Debug.LogError("坐标："+pos);
            obj.anchoredPosition = pos;

        }
    }

}
