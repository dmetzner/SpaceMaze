using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public int Damage = 1;
    public bool DieOnImpact = true;
    public AudioClip crashClip;

    new private bool enabled = true;
    private BasicHealthSystem healthSystem;
    private BoxCollider2D boxCollider2D;
    

    // Start is called before the first frame update
    private void Start()
    {
        healthSystem = GetComponent<BasicHealthSystem>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!enabled)
        {
            return;
        }

        PlayerController controller = other.gameObject.GetComponent<PlayerController>();

        if (controller == null)
        {
            return;
        }

        controller.PlaySound(crashClip);

        controller.ChangeHealth(-1 * Damage);
        if (DieOnImpact)
        {
            healthSystem.die();  
        }
        else
        {
            healthSystem.ChangeHealth(-1 * Damage);
        }
    }

    public void setEnabled(bool enabled)
    {
        this.enabled = enabled;
        boxCollider2D.isTrigger = true;
    }
}
