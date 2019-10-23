using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageZone : MonoBehaviour
{
    public int Damage = 1;
    public AudioClip crashClip;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        PlayerController controller = other.collider.GetComponent<PlayerController>();
        
        if (controller != null) 
        {
            controller.PlaySound(crashClip);
            controller.ChangeHealth((-1) * Damage);
        }
    }
}
