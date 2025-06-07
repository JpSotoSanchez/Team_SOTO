using UnityEngine;
using UnityEngine.AI;

public class ClownEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject looser;
    public Animator animator;
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
        if(volume > volumeThreshold)
        {
            animator.SetBool("pers", true);
            agent.SetDestination(player.position);

        }
        else
        {
            animator.SetBool("pers", false);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") ){
        looser.SetActive(true);
        }
    }
}
