using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    Enemy1 _enemy;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed; //8
    private float spawnpointOffset_straight;
    private float spawnpointOffset_diagonal;
    private float nextFire = 0.0F;

    private byte shootingState = 1;

    void Awake() 
    {
        _enemy = GetComponent<Enemy1>();
        spawnpointOffset_straight = (transform.localScale.x)/2+(bulletPrefab.transform.localScale.x)/2+0.011f;
        spawnpointOffset_diagonal = (transform.localScale.x * Mathf.Sqrt(2))/2;
    }

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
        nextFire = Time.time + _enemy.fireRate;
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x + offsetX, bulletSpawnPoint.position.y + offsetY);
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x - offsetX, bulletSpawnPoint.position.y - offsetY);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void ShootStraight()
    {
        if(Time.time > nextFire){
            ShootBullet(bulletSpawnPoint.up, 0f, spawnpointOffset_straight);
            ShootBullet(bulletSpawnPoint.right, spawnpointOffset_straight, 0f);
            ShootBullet((bulletSpawnPoint.up*-1), 0f, -spawnpointOffset_straight);
            ShootBullet((bulletSpawnPoint.right*-1), -spawnpointOffset_straight, 0f);
            shootingState = 2;
        }

    }

    void ShootDiagonally()
    {
        if(Time.time > nextFire)
        {
            //SV
            ShootBullet(new Vector2(1, 1).normalized, spawnpointOffset_diagonal, spawnpointOffset_diagonal);
            //JV
            ShootBullet(new Vector2(1, -1).normalized, spawnpointOffset_diagonal, -spawnpointOffset_diagonal);
            //JZ
            ShootBullet(new Vector2(-1, -1).normalized, -spawnpointOffset_diagonal, -spawnpointOffset_diagonal);
            //SZ
            ShootBullet(new Vector2(-1, 1).normalized, -spawnpointOffset_diagonal, spawnpointOffset_diagonal);
            shootingState = 1;
        }
    }
}
