using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthCollectible : MonoBehaviour
{
    public int Health = 1;
    public AudioClip HealClip;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        
        if (controller != null)
        {
            controller.PlaySound(HealClip);
            controller.ChangeHealth(Health);
            Destroy(gameObject);
        }
    }
}
