using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Shooting : MonoBehaviour 
{
    Player _player;

    private Animator _animator;
    private AudioManager _audiomanager;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed; //6
    private float spawnpointOffset;
    private float nextFire = 0.0F;

    void Start() {
        _player = GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        _audiomanager = FindAnyObjectByType<AudioManager>().GetComponent<AudioManager>();

        spawnpointOffset = (transform.localScale.x)/2+(bulletPrefab.transform.localScale.x)/2+0.011f; //0.011f je perfektní spot :)
    }

    void Update()
    {
        Shooting();
        StoppedShooting();
        
    }

    void Shooting()
    {
        if(Input.GetKey(KeyCode.UpArrow) && Time.time > nextFire)
        {
            ShootBullet(bulletSpawnPoint.up, 0f, spawnpointOffset);
            _animator.SetInteger("anim_state", 1); // head_up
        }
        else if(Input.GetKey(KeyCode.RightArrow) && Time.time > nextFire)
        {
            ShootBullet(bulletSpawnPoint.right, spawnpointOffset, -0.1f);
            _animator.SetInteger("anim_state", 2); // head_right
        }
        else if(Input.GetKey(KeyCode.DownArrow) && Time.time > nextFire)
        {
            ShootBullet((bulletSpawnPoint.up*-1), 0f, -spawnpointOffset);
            _animator.SetInteger("anim_state", 3); // head_down
        }
        else if(Input.GetKey(KeyCode.LeftArrow) && Time.time > nextFire)
        {
            ShootBullet((bulletSpawnPoint.right*-1), -spawnpointOffset, -0.1f);
            _animator.SetInteger("anim_state", 4); // head_left
        }
    }

    void StoppedShooting()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _animator.SetInteger("anim_state", 0); // head_static |nestřílí
        }
    }

    void ShootBullet(Vector2 direction, float offsetX, float offsetY)
    {
        nextFire = Time.time + _player.fireRate;
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x + offsetX, bulletSpawnPoint.position.y + offsetY);
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<bullet>()._player = _player;
        bulletSpawnPoint.position = new Vector2(bulletSpawnPoint.position.x - offsetX, bulletSpawnPoint.position.y - offsetY);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        _audiomanager.PlaySFX(_audiomanager.fire);
    }
}
