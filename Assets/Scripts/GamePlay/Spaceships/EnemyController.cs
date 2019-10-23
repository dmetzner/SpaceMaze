using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Material DefaultMaterial, IceMaterial;
    public AudioClip DeathSound;

    private BasicHealthSystem healthSystem;
    private SimpleLaserWeapon simpleLaserWeapon;
    private DirectionalMovement directionalMovement;
    private CollisionController collisionController;
    private AudioSource audioSource;
    private Animator animator;

    private void Start()
    {
        healthSystem = GetComponent<BasicHealthSystem>();
        simpleLaserWeapon = GetComponent<SimpleLaserWeapon>();
        directionalMovement = GetComponent<DirectionalMovement>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        collisionController = GetComponent<CollisionController>();

        healthSystem = GetComponent<BasicHealthSystem>();
        healthSystem.SubscribeToDeathEvent(HandleDeath);
    }

    private void HandleDeath()
    {
        animator.SetBool("Exploded", true);
        ShutDown();
        collisionController.setEnabled(false);
        if (audioSource != null && DeathSound != null) 
        {
            audioSource.PlayOneShot(DeathSound);
        }
        StartCoroutine("DeathAnimation");
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }

    public void ChangeHealth(int amount) 
    {
        healthSystem.ChangeHealth(amount);
    }

    public void IceEffect(float duration)
    {
        IEnumerator coroutine = IceProcess(duration);
        StartCoroutine(coroutine);        
    }

    public void ShutDown()
    {
        if (null != simpleLaserWeapon)
        {
            simpleLaserWeapon.SetEnabled(false);
        }
    
        if (null != directionalMovement)
        {
            directionalMovement.SetEnabled(false);
        }
    }

    private IEnumerator IceProcess(float duration)
    {
        Renderer renderer = GetComponent<Renderer>();

        ShutDown();

        renderer.material = IceMaterial;

        yield return new WaitForSeconds(duration);

        renderer.material = DefaultMaterial;

        if (null != simpleLaserWeapon)
        {
            simpleLaserWeapon.SetEnabled(true);
        }
    
        if (null != directionalMovement)
        {
            directionalMovement.SetEnabled(true);
        }
    }

    public void PlaySound(AudioClip audio)
    {
        if (audio != null) 
        {
            audioSource.PlayOneShot(audio);
        }
    }
}
