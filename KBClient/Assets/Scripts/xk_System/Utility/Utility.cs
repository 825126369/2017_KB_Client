using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 对委托的一些操作
/// </summary>
/// <typeparam name="T"></typeparam>
public static class DelegateUtility
{
    public static bool CheckFunIsExist<T>(Action<T> mEvent, Action<T> fun)
    {
        if (mEvent == null)
        {
            return false;
        }
        Delegate[] mList = mEvent.GetInvocationList();
        return Array.Exists<Delegate>(mList, (x) => x.Equals(fun));
    }
}


public static class TimeUtility
{
    /// <summary>
    /// 时间戳转为C#格式时间
    /// </summary>
    /// <param name=”timeStamp”></param>
    /// <returns></returns>
    public static DateTime GetTime(ulong timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        return dtStart.AddSeconds(timeStamp);
    }

    /// <summary>
    /// DateTime时间格式转换为Unix时间戳格式
    /// </summary>
    /// <param name=”time”></param>
    /// <returns></returns>
    public static ulong GetTimeStamp(System.DateTime time)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new  DateTime(1970, 1, 1));
        return (ulong)(time - startTime).TotalSeconds;
    }

    public static int getTickCount()
    {
        return Environment.TickCount;
    }
}

public static class CoordinateUtility
{
    public static Vector3 WorldToUI(Camera camera, Vector3 pos)
    {
        CanvasScaler scaler = GameObject.Find("UIRoot").GetComponent<CanvasScaler>();

        float resolutionX = scaler.referenceResolution.x;
        float resolutionY = scaler.referenceResolution.y;

        Vector3 viewportPos = camera.WorldToViewportPoint(pos);

        Vector3 uiPos = new Vector3(viewportPos.x * resolutionX - resolutionX * 0.5f,
            viewportPos.y * resolutionY - resolutionY * 0.5f, 0);

        return uiPos;
    }

    /// <summary>
    /// 屏幕坐标转化为UGUI 锚点坐标
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>
    public static Vector2 ScreenToUIPoint(Canvas canvas, RectTransform parent)
    {
        Vector2 pos = Vector2.zero;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Input.mousePosition, canvas.worldCamera, out pos))
            {
                Debug.LogError("坐标系转换错误");
            }
        }else if(canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Input.mousePosition, null, out pos))
            {
                Debug.LogError("坐标系转换错误");
            }
        }
        return pos;
    }

    /// <summary>
    /// UGUI 锚点坐标转化为 世界坐标
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>
    public static Vector2 UIToWorldPoint(Canvas canvas,Vector2 UIPos)
    {
        Vector3 pos = Vector3.zero;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform,UIPos,canvas.worldCamera,out pos))
        {
            return pos;
        }
        else
        {
            Debug.LogError("坐标系转换错误");
            return pos;
        }
    }
}

