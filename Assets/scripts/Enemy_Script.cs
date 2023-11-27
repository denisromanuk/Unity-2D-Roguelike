using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed; //8
    public float spawnpointOffset; //0.65f
    public float fireRate; //0.9f = default
    private float nextFire = 0.0F;

    private byte shootingState = 1;

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        switch(shootingState)
        {
            case 1:
                ShootStraight();
                break;
            case 2:
                ShootDiagonally();
                break;
        }
    }

    void ShootBullet(Vector2 direction, float offsetX, float offsetY)
    {
        nextFire = Time.time + fireRate;
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x + offsetX, bulletSpawnPoint.position.y + offsetY);
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x - offsetX, bulletSpawnPoint.position.y - offsetY);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void ShootStraight()
    {
        if(Time.time > nextFire){
            ShootBullet(bulletSpawnPoint.up, 0f, spawnpointOffset);
            ShootBullet(bulletSpawnPoint.right, spawnpointOffset, 0f);
            ShootBullet((bulletSpawnPoint.up*-1), 0f, -spawnpointOffset);
            ShootBullet((bulletSpawnPoint.right*-1), -spawnpointOffset, 0f);
            shootingState = 2;
        }

    }

    void ShootDiagonally()
    {
        if(Time.time > nextFire)
        {
            //SV
            ShootBullet(new Vector2(1, 1).normalized, spawnpointOffset, spawnpointOffset);
            //JV
            ShootBullet(new Vector2(1, -1).normalized, spawnpointOffset, -spawnpointOffset);
            //JZ
            ShootBullet(new Vector2(-1, -1).normalized, -spawnpointOffset, -spawnpointOffset);
            //SZ
            ShootBullet(new Vector2(-1, 1).normalized, -spawnpointOffset, spawnpointOffset);
            shootingState = 1;
        }
    }
}
