using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnemy : MonoBehaviour
{
    public float maxDistance = 2f;
    public AudioClip disableSFX;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if(!LevelManager.isGameOver)
        {
            
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                Debug.Log(hit.collider.CompareTag("Drone"));
                if (hit.collider.CompareTag("Drone"))
                {
                    FindObjectOfType<LevelManager>().DisplayEnemyDisableText();
                    {
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            
                            AudioSource.PlayClipAtPoint(disableSFX, transform.position);
                            Instantiate(explosion, hit.transform.position, Quaternion.identity);
                            Destroy(hit.transform.gameObject);
                            FindObjectOfType<LevelManager>().HideEnemyDisableText();
                        }
                    }
                }
                else
                {
                    FindObjectOfType<LevelManager>().HideEnemyDisableText();
                }
            }
        }
        
    }

}
