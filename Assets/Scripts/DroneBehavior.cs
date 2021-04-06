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
        if(!LevelManager.isGameOver)
        {
            transform.LookAt(player);

            closeEnough = Vector3.Distance(transform.position, player.position) <= 10;

            if (closeEnough && ready)
            {
                ready = false;
                Invoke("LaunchProjectile", 2);
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
    }


}
