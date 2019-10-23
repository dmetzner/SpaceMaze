using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMovement : MonoBehaviour
{
    public float Speed = 3.0f;
    public bool Vertical = true;
    public float TimeTillTraverse = 0;

    private float traverseTimer;
    new private bool enabled = true;


    // Start is called before the first frame update
    void Start()
    {
        traverseTimer = TimeTillTraverse;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled)
        {
            return;
        }

        if (TimeTillTraverse > 0)
        {
            traverseTimer -= Time.deltaTime;
            if (traverseTimer <= 0)
            {
                Speed *= -1;
                traverseTimer = TimeTillTraverse;
            }
        }

        if (Vertical) 
        {
            transform.position = new Vector2(
                transform.position.x, 
                transform.position.y + Speed * Time.deltaTime
            );
        }
        else 
        {
            transform.position = new Vector2(
                transform.position.x + Speed * Time.deltaTime, 
                transform.position.y
            );
        }
    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
    }
}
