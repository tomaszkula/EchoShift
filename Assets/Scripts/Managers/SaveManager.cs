using UnityEngine;

public class SaveManager : BaseManager
{
    public int GetValue(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public float GetValue(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public string GetValue(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public void SetValue(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void SetValue(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public void SetValue(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}
