using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BasicHealthSystem : MonoBehaviour
{
    public int MaxHealth = 5;
    public int Health { get { return currentHealth; } }
    public float InvincibleTime = 0;
    public bool Destructable = true;

    private int currentHealth;
    private bool isInvincible = false;
    private float invincibleTimer;
    private Animator animator;
    private AudioSource audioSource;

    UnityEvent deathEvent = new UnityEvent();

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        setHealth(MaxHealth);   
    }

    // Update is called once per frame
    private void Update()
    {
        if (InvincibleTime == 0)
        {
            return;
        }

        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer < 0) 
        {
            setIsInvincible(false);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (InvincibleTime > 0 && amount < 0) 
        {
            if (isInvincible) 
            {
                return; // can't lose health when invincible 
            }
            setIsInvincible(true);
            invincibleTimer = InvincibleTime;
        } 
        setHealth(amount);

        if (Destructable && currentHealth <= 0)
        {
            die();
        }
    }

    public void die() 
    {
        deathEvent.Invoke();
    }

    public bool isDead()
    {
        return Health <= 0;
    }

    public float GetHealthPercentage()
    {
        return Health / (float) MaxHealth;
    }

    private void setHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
        if (null != animator)
        {
            animator.SetInteger("health", currentHealth);
        }
    }

    private void setIsInvincible(bool isInvincible)
    {
        this.isInvincible = isInvincible;
        if (null != animator)
        {
            animator.SetBool("invincible", isInvincible);
        }
    }
    
    public void SubscribeToDeathEvent(UnityAction callback)
    {
        deathEvent.AddListener(callback);
    }

    public void RegainFullLife()
    {
        currentHealth = MaxHealth;
    }
}
