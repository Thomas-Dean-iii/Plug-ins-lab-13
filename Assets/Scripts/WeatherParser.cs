using System.Xml;
using UnityEngine;

public static class WeatherParser
{
    public static string GetCondition(string xml)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        return doc.SelectSingleNode("//weather").Attributes["value"].Value;
    }

    public static float GetTemperature(string xml)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        return float.Parse(doc.SelectSingleNode("//temperature").Attributes["value"].Value);
    }
}
