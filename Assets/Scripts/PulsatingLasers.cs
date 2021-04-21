using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingLasers : MonoBehaviour
{

    public float pulseRate = 4f;
    public AudioClip laserOnSFX;
    public AudioClip laserOffSFX;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LaserSetInactive", pulseRate * 0.5f, pulseRate);
        InvokeRepeating("LaserSetActive", pulseRate, pulseRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LaserSetActive()
    {
        transform.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(laserOnSFX, transform.position);
    }

    void LaserSetInactive()
    {
        transform.gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(laserOffSFX, transform.position);
    }
}
