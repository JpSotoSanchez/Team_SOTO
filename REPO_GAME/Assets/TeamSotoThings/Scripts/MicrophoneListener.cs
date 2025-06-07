using UnityEngine;

public class MicrophoneListener : MonoBehaviour
{
    public string micName;
    public float volumeThreshold = 0.1f;
    public bool isMakingNoise;

    private AudioClip micClip;
    private int sampleWindow = 128;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            micName = Microphone.devices[0];
            micClip = Microphone.Start(micName, true, 1, 44100);
        }
        else
        {
            Debug.LogWarning("No se detectó micrófono");
        }
    }

    void Update()
    {
        float volume = GetMaxVolume();
        isMakingNoise = volume > volumeThreshold;
    }

    float GetMaxVolume()
    {
        float maxVol = 0f;
        float[] samples = new float[sampleWindow];
        int micPos = Microphone.GetPosition(micName) - sampleWindow;
        if (micPos < 0) return 0f;

        micClip.GetData(samples, micPos);
        foreach (var sample in samples)
        {
            float absSample = Mathf.Abs(sample);
            if (absSample > maxVol)
                maxVol = absSample;
        }
        return maxVol;
    }
}