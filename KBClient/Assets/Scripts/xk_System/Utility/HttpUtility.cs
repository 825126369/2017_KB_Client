using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpUtility
{
    private  string result = string.Empty;
    private  string error = string.Empty;

    private void Clear()
    {
        result = string.Empty;
        error = string.Empty;
    }

    public  IEnumerator GetData(string url, Dictionary<string, string> data)
    {
        Clear();
        string urlStr = url;
        if(data.Count>0)
        {
            urlStr += "?";
        }
        foreach(var v in data)
        {
            urlStr += v.Key + "=" + v.Value+"&";
        }
        if (urlStr.EndsWith("&"))
        {
            urlStr = urlStr.Remove(urlStr.Length-1);
        }

        WWW www = new WWW(urlStr);
        yield return www;
        if(www.isDone &&string.IsNullOrEmpty(www.error))
        {
            result = www.text;
        }else
        {
            error = www.error;
        }
    }

    /// <summary>
    /// 可用于语音传输
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public IEnumerator PostData(string url,Dictionary<string,object> data)
    {
        Clear();
        WWWForm form= new WWWForm();
        foreach (var v in data)
        {
            form.AddField(v.Key, v.Value.ToString());
            form.AddField(v.Key, v.Value.ToString());
            if (v.Value is byte[])
            {
                byte[] bin = v.Value as byte[];
                form.AddBinaryData(v.Key, bin);
            }
        }
        WWW www = new WWW(url, form);
        yield return www;

        if (www.isDone && string.IsNullOrEmpty(www.error))
        {
            result = www.text;
        }
        else
        {
            error = www.error;
        }
    }

    public  string GetData()
    {
        return result;
    }

    public  string GetError()
    {
        return error;
    }

}
