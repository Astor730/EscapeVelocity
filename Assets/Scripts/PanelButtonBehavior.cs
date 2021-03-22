using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButtonBehavior : MonoBehaviour
{
    Transform player;
    bool closeEnough;
    public GameObject panel;

    public AudioClip panelSFX;

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
            FindObjectOfType<LevelManager>().DisplayPanelButtonText();
        }
        else
        {
            FindObjectOfType<LevelManager>().HidePanelButtonText();
        }

        if(Input.GetKeyDown(KeyCode.B) && closeEnough)
        {
            panel.SetActive(false);
            AudioSource.PlayClipAtPoint(panelSFX, Camera.main.transform.position);
        }
    }
}
