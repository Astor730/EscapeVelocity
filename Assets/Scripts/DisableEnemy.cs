using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnemy : MonoBehaviour
{
    public float maxDistance = 3f;

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
                if (hit.collider.CompareTag("Enemy"))
                {
                    FindObjectOfType<LevelManager>().DisplayEnemyDisableText();
                    {
                        if (Input.GetKeyDown(KeyCode.F))
                        {
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
