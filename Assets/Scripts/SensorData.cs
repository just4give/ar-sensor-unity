
using UnityEngine;

[System.Serializable]
public class SensorData 
{
    public float temperature=0f;
    public float humidity=0f;

    public static SensorData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SensorData>(jsonString);
    }
}
