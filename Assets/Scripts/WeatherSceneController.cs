using UnityEngine;

public class WeatherSceneController : MonoBehaviour
{
    private WeatherManager weather;

    [Header("Skybox Materials")]
    public Material skyClear;
    public Material skyCloudy;
    public Material skyRain;
    public Material skySnow;
    public Material skyNight;

    [Header("Sun Settings")]
    public Light sun;

    [Header("City Selection")]
    public string[] cities = { "Orlando,us", "Tokyo,jp", "London,uk", "Sydney,au", "Reykjavik,is" };
    public int selectedCityIndex = 0;

    private void Awake()
    {
        
        weather = new WeatherManager();

        
        if (cities == null || cities.Length == 0)
        {
            Debug.LogError("WeatherSceneController: No cities set!");
        }

        if (sun == null)
        {
            Debug.LogError("WeatherSceneController: Sun Light is not assigned!");
        }
    }

    private void Start()
    {
        UpdateCity();
    }

    
    public void UpdateCity()
    {
        if (weather == null)
        {
            Debug.LogError("WeatherManager is not initialized!");
            return;
        }

        if (cities == null || cities.Length == 0)
        {
            Debug.LogError("No cities set!");
            return;
        }

        string city = cities[selectedCityIndex];
        StartCoroutine(weather.GetWeatherXML(city, OnWeatherLoaded, this));
    }

    
    private void OnWeatherLoaded(string xml)
    {
        if (string.IsNullOrEmpty(xml))
        {
            Debug.LogError("WeatherSceneController: Received empty weather data!");
            return;
        }

        string condition = WeatherParser.GetCondition(xml);

        
        switch (condition.ToLower())
        {
            case "clear sky":
                RenderSettings.skybox = skyClear;
                sun.intensity = 1.2f;
                break;

            case "clouds":
                RenderSettings.skybox = skyCloudy;
                sun.intensity = 0.6f;
                break;

            case "rain":
                RenderSettings.skybox = skyRain;
                sun.intensity = 0.3f;
                break;

            case "snow":
                RenderSettings.skybox = skySnow;
                sun.intensity = 0.5f;
                break;

            default:
                RenderSettings.skybox = skyNight;
                sun.intensity = 0.1f;
                break;
        }

        
        DynamicGI.UpdateEnvironment();
    }

    
    public void SetCity(int index)
    {
        if (index < 0 || index >= cities.Length)
        {
            Debug.LogError("WeatherSceneController: Invalid city index!");
            return;
        }

        selectedCityIndex = index;
        UpdateCity();
    }
}
