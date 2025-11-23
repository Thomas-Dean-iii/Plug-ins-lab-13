using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader : MonoBehaviour
{
    [Header("Image URLs")]
    public string[] imageUrls;

    private Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>();

    // Main function to use
    public void GetWebImage(string url, Action<Texture2D> callback)
    {
        if (cache.ContainsKey(url))
        {
            callback(cache[url]);
        }
        else
        {
            StartCoroutine(DownloadImage(url, callback));
        }
    }

    private IEnumerator DownloadImage(string url, Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        Texture2D tex = DownloadHandlerTexture.GetContent(request);
        cache[url] = tex;

        callback(tex);
    }
}