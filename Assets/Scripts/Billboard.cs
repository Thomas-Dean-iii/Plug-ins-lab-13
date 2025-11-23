using UnityEngine;

public class Billboard : MonoBehaviour
{
    public ImageDownloader downloader;
    public string imageURL;
    public Renderer rend;

    private void Start()
    {
        downloader.GetWebImage(imageURL, ApplyTexture);
    }

    private void ApplyTexture(Texture2D tex)
    {
        rend.material.mainTexture = tex;
    }
}
