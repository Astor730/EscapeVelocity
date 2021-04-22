using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = startingHealth;
        print(startingHealth); 
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            print(currentHealth);
        }

        if (currentHealth <= 0)
        {
            
        }

        Debug.Log("Current health: " + currentHealth);
    }

    public void TakeHealth(int healthAmount)
    {
        if (currentHealth < 100)
        {
            currentHealth += healthAmount;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject, 3);
            Instantiate(explosion, transform.position, Quaternion.identity);

        }

        Debug.Log("Current health: " + currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(10);
        }
    }


}
