using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehavior : MonoBehaviour
{
    Transform player;
    public GameObject projectilePrefab;
    public float projectileForce = 10f;
    bool closeEnough;
    bool ready;
    public AudioClip shootSFX;
    public float shootRate = 2f;
    public float shootDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        closeEnough = false;
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        if(!LevelManager.isGameOver)
        {
           
            transform.LookAt(player);

            closeEnough = Vector3.Distance(transform.position, player.position) <= shootDistance;

            if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if(hit.transform.CompareTag("Player") && closeEnough && ready)
                {
                    ready = false;
                    Invoke("LaunchProjectile", shootRate);
                }
            }
        }
    }

    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab,
                transform.position + transform.forward,
                transform.rotation) as GameObject;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * projectileForce, ForceMode.VelocityChange);

        projectile.transform.SetParent(
            GameObject.FindGameObjectWithTag("ProjectileParent").transform);

        ready = true;

        AudioSource.PlayClipAtPoint(shootSFX, transform.position);
    }


}
