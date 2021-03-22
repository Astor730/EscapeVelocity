using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public GameObject button;
    bool closeEnough;

    public AudioClip hitSFX;
    public AudioClip deactivateSFX;

    // Start is called before the first frame update
    void Start()
    {
        closeEnough = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(button != null)
        {
            var buttonScript = button.GetComponent<LaserButtonBehavior>();
            closeEnough = buttonScript.closeEnough;
        }

        if (Input.GetKeyDown(KeyCode.B) && button != null && closeEnough == true)
        {
            AudioSource.PlayClipAtPoint(deactivateSFX, Camera.main.transform.position, 0.1f);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().LevelLost();
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position);
        }
    }
}
