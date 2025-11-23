using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager
{
    private string apiKey = "c2370700fbe334813e8a501a9a1e1175"; 

    
    private string URL(string city)
    {
        return $"https://api.openweathermap.org/data/2.5/weather?q={city}&mode=xml&appid={apiKey}";
    }

    
    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError($"Network problem: {request.error}");
            else if (request.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError($"Response error: {request.responseCode}");
            else
                callback(request.downloadHandler.text);
        }
    }

    
    public IEnumerator GetWeatherXML(string city, Action<string> callback, MonoBehaviour runner)
    {
        if (string.IsNullOrEmpty(city))
        {
            Debug.LogError("WeatherManager: City is null or empty!");
            yield break;
        }

        string url = URL(city);
        
        yield return runner.StartCoroutine(CallAPI(url, callback));
    }
}
