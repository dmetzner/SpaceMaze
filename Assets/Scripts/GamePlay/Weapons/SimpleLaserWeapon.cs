using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleLaserWeapon : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public int Damage = 1;
    public Vector2 ShootingDirection = Vector2.right;
    public float ProjectileSpeed = 500;
    public float ShootingDelay = 0.8f; 
    public bool AutomaticFire = true;
    public AudioClip ShootClip;

    private AudioSource audioSource;
    private BasicHealthSystem healthSystem;
    private bool weaponIsLoaded = true;
    private float shootingDelayTimer = 0.8f; 
    new private bool enabled = true;

    private void Start() 
    {
        healthSystem = GetComponent<BasicHealthSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (!enabled || healthSystem.isDead())
        {
            return;
        }

        shootingDelayTimer -= Time.deltaTime;
        if (shootingDelayTimer < 0)
        {
            weaponIsLoaded = true;
        }

        if (weaponIsLoaded && AutomaticFire)
        {
            Shoot();
        }
    }
    
    public void Shoot()
    {
        if (!weaponIsLoaded)
        {
            return;
        }
        weaponIsLoaded = false;
        shootingDelayTimer = ShootingDelay;
        
        Vector3 relativeWeaponPosition = ShootingDirection;
        GameObject projectileObject = Instantiate(
            ProjectilePrefab, 
            transform.position + relativeWeaponPosition * 0.5f, 
            Quaternion.identity
        );

        if (audioSource != null && ShootClip != null)
        {
            audioSource.PlayOneShot(ShootClip);
        }

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(ShootingDirection, ProjectileSpeed, Damage);
    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
    }

    public float GetWeaponLoadedPercentage()
    {
        if (weaponIsLoaded || Mathf.Approximately(ShootingDelay, 0))
        {
            return 1;
        }
        else
        {
            return 1 - (shootingDelayTimer / (float) ShootingDelay);
        }
    }
}
