using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlatformBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if(other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
