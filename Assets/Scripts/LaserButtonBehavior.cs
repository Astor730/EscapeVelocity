using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserButtonBehavior : MonoBehaviour
{
    Transform player;
    public bool closeEnough;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        closeEnough = false;
    }

    // Update is called once per frame
    void Update()
    {
        closeEnough = Vector3.Distance(player.position, transform.position) <= 3;
        if(closeEnough)
        {
            FindObjectOfType<LevelManager>().DisplayLaserButtonText();
        }
        else
        {
            FindObjectOfType<LevelManager>().HideLaserButtonText();
        }
    }
}
