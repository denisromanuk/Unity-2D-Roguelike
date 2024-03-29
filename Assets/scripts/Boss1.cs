using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [Header("Boss stats:")]
    public float hp; //default: 360
    public float dmg; //default: 1
    public float speed; //default: 3
    public float fireRate; //default: 2.5

    [Header("Movement:")]
    public Rigidbody2D rb;
    public List<BoxCollider2D> triggers = new List<BoxCollider2D>();

    [Header("Shooting:")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed; //8

    private Boss1Movement _movement;
    private Boss1Shooting _shooting;
    private AudioManager _audiomanager;

    //use Start() instead of Awake() to dissable it in inspector
    void Start() {
        _movement = gameObject.AddComponent<Boss1Movement>();
        _shooting = gameObject.AddComponent<Boss1Shooting>();
        _audiomanager = FindAnyObjectByType<AudioManager>().GetComponent<AudioManager>();
    }

    public void Stats(float h, float d, float s, float fr)
    {
        hp = h;
        dmg = d;
        speed = s;
        fireRate = fr;
    }

    public void GetDamage(float damageTaken)
    {
        hp -= damageTaken;
        if(hp <= 0)
        {
            _audiomanager._musicSource.time = 140;
            _audiomanager._musicSource.volume = 0.4f;
            _audiomanager._musicSource.loop = false;
            
            _audiomanager.PlaySFX(_audiomanager.bossEnd);
            Destroy(gameObject);
        }
    }
}

class Boss1Movement : MonoBehaviour
{
    Boss1 _boss1;
    float[] rngSmer = {-1f, 1f};
    float x;
    float y;
    Vector2 moveDirection;

    void Awake() {
        _boss1 = gameObject.GetComponent<Boss1>();

        x = rngSmer[Random.Range(0, rngSmer.Length)];
        y = rngSmer[Random.Range(0, rngSmer.Length)];
    }

    void Update()
    {
        moveDirection = new Vector2(x, y).normalized;
    }

    void FixedUpdate() 
    {
        _boss1.rb.velocity = new Vector2(moveDirection.x * _boss1.speed, moveDirection.y * _boss1.speed);
    }

    //player gets damage on collision with boss
    void OnCollisionEnter2D(Collision2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().GetDamage(2);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) 
    {
        if(trigger.CompareTag("TileMap"))
        {
            foreach (BoxCollider2D boxTrigger in _boss1.triggers)
            {
                if(trigger.IsTouching(boxTrigger))
                {
                    switch(boxTrigger.offset.x)
                    {
                        case -0.51f:
                            x = 1f;
                            break;
                        case 0.51f:
                            x = -1f;
                            break;
                    }
                    switch(boxTrigger.offset.y)
                    {
                        case -0.5f:
                            y = 1f;
                            break;
                        case 0.5f:
                            y = -1f;
                            break;
                    }
                }
            }
        }
    }
}

class Boss1Shooting : MonoBehaviour
{
    Boss1 _boss1;
    private float spawnpointOffset_straight;
    private float spawnpointOffset_diagonal;
    private float nextFire = 0.0F;

    void Awake() {
        _boss1 = gameObject.GetComponent<Boss1>();
        spawnpointOffset_straight = (transform.localScale.x)/2+(_boss1.bulletPrefab.transform.localScale.x)/2+0.011f;
        spawnpointOffset_diagonal = (transform.localScale.x * Mathf.Sqrt(2))/4;
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(Time.time > nextFire){
            ShootBullet(_boss1.bulletSpawnPoint.up, 0f, spawnpointOffset_straight); //S
            ShootBullet(_boss1.bulletSpawnPoint.right, spawnpointOffset_straight, 0f); //V
            ShootBullet((_boss1.bulletSpawnPoint.up*-1), 0f, -spawnpointOffset_straight); //J
            ShootBullet((_boss1.bulletSpawnPoint.right*-1), -spawnpointOffset_straight, 0f); //Z
            ShootBullet(new Vector2(1, 1).normalized, spawnpointOffset_diagonal, spawnpointOffset_diagonal); //SV
            ShootBullet(new Vector2(1, -1).normalized, spawnpointOffset_diagonal, -spawnpointOffset_diagonal); //JV
            ShootBullet(new Vector2(-1, -1).normalized, -spawnpointOffset_diagonal, -spawnpointOffset_diagonal); //JZ
            ShootBullet(new Vector2(-1, 1).normalized, -spawnpointOffset_diagonal, spawnpointOffset_diagonal); //SZ
        }
    }

    void ShootBullet(Vector2 direction, float offsetX, float offsetY)
    {
        nextFire = Time.time + _boss1.fireRate;
        _boss1.bulletSpawnPoint.position = new Vector2(_boss1.bulletSpawnPoint.position.x + offsetX, _boss1.bulletSpawnPoint.position.y + offsetY);
        var bullet = Instantiate(_boss1.bulletPrefab, _boss1.bulletSpawnPoint.position, _boss1.bulletSpawnPoint.rotation);
        _boss1.bulletSpawnPoint.position = new Vector2(_boss1.bulletSpawnPoint.position.x - offsetX, _boss1.bulletSpawnPoint.position.y - offsetY);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * _boss1.bulletSpeed;
    }
}
