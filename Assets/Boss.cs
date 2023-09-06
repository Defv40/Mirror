using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Boss : NetworkBehaviour
{
    [SerializeField] private GameObject firePrefab; // префаб дл€ стрельбы

    [SerializeField] private float fireRate; // скорость с которой стрел€ет
    [SerializeField] private bool canFire = true;
    [SerializeField] private Transform[] firePoints; // точки c которых будет стрел€ть
    [SerializeField] private float bulletSpeed;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Debug.Log("Server: " + isServer.ToString());
    }

    private void Update()
    {
        if (!isServer) { return; }
        if (canFire)
        {
            // —трел€ем из каждой точки
            foreach (Transform firePoint in firePoints)
            {
                Fire(firePoint);
            }

            StartCoroutine(ReloadFire());
        }
    }

 
    private void Fire(Transform firePoint)
    {
        // —оздаем пулю
        GameObject bullet = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // «адаем скорость пули
        rb.velocity = firePoint.right * bulletSpeed;

        // ”ничтожаем пулю через некоторое врем€ (чтобы не создавать бесконечное количество пуль)
        Destroy(bullet, 10.0f);

        canFire = false;

        
    }

    private IEnumerator ReloadFire()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}


