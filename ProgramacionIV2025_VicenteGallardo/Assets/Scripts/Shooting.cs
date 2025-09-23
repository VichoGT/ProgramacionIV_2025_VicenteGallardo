using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [Header("Input Systems")]
    public InputActionAsset inputActions;
    private InputAction m_attackAction;

    [Header("Bullet")]
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public float powerShoot;

   
    public void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    public void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        m_attackAction = InputSystem.actions.FindAction("Attack");
    }
    private void Update()
    {
        if (m_attackAction.WasPerformedThisFrame())
        {
            Shoot();
        }
    }
    
    public void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = spawnPoint.up * powerShoot;
            rb.transform.up = spawnPoint.up;
        }
        Destroy(bullet,2);
    }


 
}
