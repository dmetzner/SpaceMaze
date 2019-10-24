using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public float Speed = 5.0f;
    public float EndPositionX = 210f;
    public PlayerController Player;
    public PlayerController Player2;
    public int DeathBarrierDamage = 3;
    public AudioClip DeathBarrierClip;

    private float clipTimer;
    private bool clipReloaded = true;
    private float deathBarrierX;

    void Start()
    {
        Camera camera = Camera.main;

        // Find out the correct view zone size based on the aspect ratio
        float halfHeight = camera.orthographicSize;
        deathBarrierX = camera.aspect * halfHeight;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Options.CoopMode)
        {
            UpdateCameraPosition(
                (Player.transform.position.y + Player2.transform.position.y) / 2, 
                Player.IsAlive() || Player2.IsAlive()
            );
            DeathBarrier(Player);
            DeathBarrier(Player2);
        }
        else
        {
            UpdateCameraPosition(Player.transform.position.y, Player.IsAlive());
            DeathBarrier(Player);
        }

        clipTimer -= Time.deltaTime;
        if (clipTimer < 0)
        {
            clipReloaded = true;
        }
    }

    private void UpdateCameraPosition(float y, bool is_alive)
    {
        if (transform.position.x < EndPositionX && is_alive)
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, -10);
        }
    }

    private void DeathBarrier(PlayerController Player)
    {
        if (Player.transform.position.x + deathBarrierX + 1 < transform.position.x)
        {
            Player.ChangeHealth(-1 * DeathBarrierDamage);
            if (clipReloaded)
            {
                clipReloaded = false;
                Player.PlaySound(DeathBarrierClip);
                clipTimer = 2f;
            }
        }
    }
}
