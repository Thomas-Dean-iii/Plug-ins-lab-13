using UnityEngine;

public class WeatherSceneController : MonoBehaviour
{
    public WeatherManager weather;

    [Header("Skybox Materials")]
    public Material skyClear;
    public Material skyCloudy;
    public Material skyRain;
    public Material skySnow;
    public Material skyNight;

    [Header("Sun Settings")]
    public Light sun;

    [Header("City Selection")]
    public string[] cities;
    public int selectedCityIndex = 0;

    private void Start()
    {
        UpdateCity();
    }

    public void UpdateCity()
    {
        StartCoroutine(weather.GetWeatherXML(cities[selectedCityIndex], OnWeatherLoaded));
    }

    private void OnWeatherLoaded(string xml)
    {
        Debug.Log(xml);

        string condition = WeatherParser.GetCondition(xml);

        // --- Apply skybox ---
        switch (condition)
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

        // Force skybox update
        DynamicGI.UpdateEnvironment();
    }

    public void SetCity(int index)
    {
        selectedCityIndex = index;
        UpdateCity();
    }
}
