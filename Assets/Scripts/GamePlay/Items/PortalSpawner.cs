using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalSpawner : MonoBehaviour
{
    public GameObject PortalPrefab;
    
    private BasicHealthSystem basicHealthSystem;

    private void Start() 
    {
        basicHealthSystem = GetComponent<BasicHealthSystem>();
        basicHealthSystem.SubscribeToDeathEvent(Spawn);
    }

    private void Spawn()
    {
        Instantiate(PortalPrefab, transform.position, Quaternion.identity);
    }
}
