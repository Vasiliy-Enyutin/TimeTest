using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeController : MonoBehaviour
{
    private const string SERVER_URL = "worldtimeapi.org/api/timezone/Europe/Moscow";

    public void GetMoscowTime()
    {
        StartCoroutine(FetchMoscowTime());
    }

    IEnumerator FetchMoscowTime()
    {
        UnityWebRequest request = UnityWebRequest.Get(SERVER_URL);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error time request: " + request.error);
            yield break;
        }

        string jsonText = request.downloadHandler.text;
        TimeInfo timeInfo = JsonUtility.FromJson<TimeInfo>(jsonText);

        if (timeInfo != null)
        {
            string moscowTime = timeInfo.datetime;

            // JavaScript
            string jsCode = "alert('Moscow date and time: " + moscowTime + "');";
            Application.ExternalEval(jsCode);
        }
        else
        {
            Debug.LogError("JSON deserialization error");
        }
    }
    
    [Serializable]
    public class TimeInfo
    {
        public string datetime;
    }
}