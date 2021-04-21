using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCutscene : MonoBehaviour
{
    public AudioClip startingAudio;
    public static bool isCutscene;

    // Start is called before the first frame update
    void Start()
    {
        isCutscene = true;
        AudioSource.PlayClipAtPoint(startingAudio, transform.position);
        Invoke("IsCutscene", startingAudio.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IsCutscene()
    {
        isCutscene = false;
        
    }
}
