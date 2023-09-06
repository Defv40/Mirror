using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Boss : NetworkBehaviour
{
    [SerializeField] private GameObject firePrefab; // ������ ��� ��������

    [SerializeField] private float fireRate; // �������� � ������� ��������
    [SerializeField] private bool canFire = true;
    [SerializeField] private Transform[] firePoints; // ����� c ������� ����� ��������
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
            // �������� �� ������ �����
            foreach (Transform firePoint in firePoints)
            {
                Fire(firePoint);
            }

            StartCoroutine(ReloadFire());
        }
    }

 
    private void Fire(Transform firePoint)
    {
        // ������� ����
        GameObject bullet = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // ������ �������� ����
        rb.velocity = firePoint.right * bulletSpeed;

        // ���������� ���� ����� ��������� ����� (����� �� ��������� ����������� ���������� ����)
        Destroy(bullet, 10.0f);

        canFire = false;

        
    }

    private IEnumerator ReloadFire()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}


