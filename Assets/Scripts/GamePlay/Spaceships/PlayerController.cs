using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f;
    public int PlayerNumber = 1;
    public ParticleSystem propulsionParticleSystem;

    private Rigidbody2D rigidbody2d;
    private BasicHealthSystem healthSystem;
    private SimpleLaserWeapon simpleLaserWeapon;
    private IceLaser iceLaser;
    private bool alive = true;

    AudioSource audioSource;

    private void Awake() 
    {
        if (!Options.CoopMode && PlayerNumber != 1) 
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<BasicHealthSystem>();
        simpleLaserWeapon = GetComponent<SimpleLaserWeapon>();
        iceLaser = GetComponent<IceLaser>();
        audioSource = GetComponent<AudioSource>();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        UIHealthBar.instance.SetValue(1, PlayerNumber); 
        UIManaBar.instance.SetValue(1, PlayerNumber); 

        healthSystem.MaxHealth = (int) (healthSystem.MaxHealth * Options.difficulty);
        healthSystem.RegainFullLife();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!alive)
        {
            return;
        }

        HandleMovement();
        HandleShooting();

        UIManaBar.instance.SetValue(
            iceLaser.GetWeaponLoadedPercentage(),
            PlayerNumber
        ); 
    }

    public void ChangeHealth(int amount) 
    {
        if (!alive)
        {
            return;
        }
        healthSystem.ChangeHealth(amount);

        UIHealthBar.instance.SetValue(
            healthSystem.GetHealthPercentage(),
            PlayerNumber    
        ); 

        if (healthSystem.isDead())
        {
            propulsionParticleSystem.Stop();
            LevelController.instance.SetGameOver();
            alive = false;
            return;
        }
    }

    private void HandleShooting()
    {
        if(PlayerNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                simpleLaserWeapon.Shoot();
            }
            else if(Input.GetKeyDown(KeyCode.C)) 
            {
                iceLaser.Shoot();
            }
        }
        else if(PlayerNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                simpleLaserWeapon.Shoot();
            }
            else if(Input.GetKeyDown(KeyCode.RightShift)) 
            {
                iceLaser.Shoot();
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 move = Options.CoopMode ? GetCoopModeInput() : GetSinglePlayerInput();

        Vector2 position = rigidbody2d.position;
        position += move * Speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        HandlePropulsionAnimation(move);
    }

    private Vector2 GetSinglePlayerInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        return new Vector2(horizontal, vertical);
    }

    private Vector2 GetCoopModeInput()
    {
        float horizontal = Input.GetAxis("P" + PlayerNumber + "_Horizontal");
        float vertical = Input.GetAxis("P" + PlayerNumber + "_Vertical");
        return new Vector2(horizontal, vertical);
    }

    private void HandlePropulsionAnimation(Vector2 move)
    {
        if (move.x > 0)
        {
            propulsionParticleSystem.Play();
        }
        else
        {
            propulsionParticleSystem.Stop();
        }
    }

    public void PlaySound(AudioClip audio)
    {
        if (audio != null) 
        {
            audioSource.PlayOneShot(audio);
        }
    }

    public bool IsAlive()
    {
        return alive;
    }
}
