using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Shooting : MonoBehaviour 
{
    Player _player;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed; //6
    private float spawnpointOffset;
    private float nextFire = 0.0F;

    void Start() {
        _player = GetComponent<Player>();

        spawnpointOffset = (transform.localScale.x)/2+(bulletPrefab.transform.localScale.x)/2+0.011f; //0.011f je perfektní spot :)
    }

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        if(Input.GetKey(KeyCode.UpArrow) && Time.time > nextFire)
        {
            ShootBullet(bulletSpawnPoint.up, 0f, spawnpointOffset);
        }
        else if(Input.GetKey(KeyCode.RightArrow) && Time.time > nextFire)
        {
            ShootBullet(bulletSpawnPoint.right, spawnpointOffset, 0f);
        }
        else if(Input.GetKey(KeyCode.DownArrow) && Time.time > nextFire)
        {
            ShootBullet((bulletSpawnPoint.up*-1), 0f, -spawnpointOffset);
        }
        else if(Input.GetKey(KeyCode.LeftArrow) && Time.time > nextFire)
        {
            ShootBullet((bulletSpawnPoint.right*-1), -spawnpointOffset, 0f);
        }
    }

    void ShootBullet(Vector2 direction, float offsetX, float offsetY)
    {
        nextFire = Time.time + _player.fireRate;
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x + offsetX, bulletSpawnPoint.position.y + offsetY);
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x - offsetX, bulletSpawnPoint.position.y - offsetY);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
