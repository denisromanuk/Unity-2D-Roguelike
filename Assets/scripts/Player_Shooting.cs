using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour 
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5;
    public float spawnpointOffset; //0.65f

    bool already_shooting = false;

    void Update()
    {
        Shooting();
        StopShooting();
    }

    void Shooting()
    {
        if(!already_shooting)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                ShootBullet(bulletSpawnPoint.up, 0f, spawnpointOffset);
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShootBullet(bulletSpawnPoint.right, spawnpointOffset, 0f);
            }

            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShootBullet((bulletSpawnPoint.up*-1), 0f, -spawnpointOffset);
            }

            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShootBullet((bulletSpawnPoint.right*-1), -spawnpointOffset, 0f);
            }
        }
    }

    void ShootBullet(Vector2 direction, float offsetX, float offsetY)
    {
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x + offsetX, bulletSpawnPoint.position.y + offsetY);
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x - offsetX, bulletSpawnPoint.position.y - offsetY);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        already_shooting = true;
    }

    void StopShooting()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow)){already_shooting = false;}
        else if(Input.GetKeyUp(KeyCode.RightArrow)){already_shooting = false;}
        else if(Input.GetKeyUp(KeyCode.DownArrow)){already_shooting = false;}
        else if(Input.GetKeyUp(KeyCode.LeftArrow)){already_shooting = false;}
    }
}
