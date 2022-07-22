using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    public float Volume;
    public GameObject target1;
    public GameObject target2;
    public AudioClip impact;
    AudioSource audioSource;
    public bool Played = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerStay()
    {
        if (!Played)
        {
            audioSource.PlayOneShot(impact, Volume);
            Played = true;

            Debug.Log("работает");

        }
    }
}
