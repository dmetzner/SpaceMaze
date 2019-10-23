using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public bool hostile;

    private Rigidbody2D rigidbody2d;
    private Transform cameraTransform;
    private int damage;
    private IceEffect iceEffect;

    public AudioClip HitObjectClip;

        
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        rigidbody2d = GetComponent<Rigidbody2D>();
        iceEffect = GetComponent<IceEffect>();
    }

    public void Launch(Vector2 direction, float force, int damage = 1)
    {
        transform.eulerAngles = new Vector3(
            0, 0, Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI
        );
        rigidbody2d.AddForce(direction * force);
        this.damage = damage;
    }

    // Update is called once per frame
    private void Update()
    {
        if(transform.position.magnitude > cameraTransform.position.x + 100)
        {
            Destroy(gameObject);
        }   
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hostile)
        {
            PlayerController playerController = other.collider.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.PlaySound(HitObjectClip);
                playerController.ChangeHealth(-1 * damage);
            }
        }
        else
        {
            EnemyController enemyController = other.collider.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.ChangeHealth(-1 * damage);
                enemyController.PlaySound(HitObjectClip);
                if (null != iceEffect)
                {
                    enemyController.IceEffect(iceEffect.Duration);
                }      
            }
        }
        Destroy(gameObject);
    }
}
