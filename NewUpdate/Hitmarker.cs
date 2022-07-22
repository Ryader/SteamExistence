using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    public float timeLive;
    public float maxTimeLive;
    public GameObject hitmarkerObj;
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Update()
    {
        if (timeLive == maxTimeLive)
        {
            hitmarkerObj.SetActive(true);
            audioSource.PlayOneShot(audioClip);
        }
        else if (timeLive == 0)
            hitmarkerObj.SetActive(false);

        timeLive -= Time.deltaTime;
        timeLive = Mathf.Clamp(timeLive, 0, maxTimeLive);
    }
}
