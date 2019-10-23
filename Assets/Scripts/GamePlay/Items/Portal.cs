using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController playerController = other.collider.GetComponent<PlayerController>();
        if (null != playerController)
        {
            LevelController.instance.MoveToNextLevel();
        }
    }
}
