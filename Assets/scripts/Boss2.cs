using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    [Header("Boss stats:")]
    public float hp; //default: 280
    public float dmg; //default: 2
    public float speed; //default: 3

    [Header("Movement:")]
    public Rigidbody2D rb;
    public List<BoxCollider2D> triggers = new List<BoxCollider2D>();

    [Header("Split 1:")]
    public List<GameObject> splittedBosses1 = new List<GameObject>();

    private Boss2Movement _movement;
    private Boss2Split _split;

    //use Start() instead of Awake() to dissable it in inspector
    void Start() {
        _movement = gameObject.AddComponent<Boss2Movement>();
        _split = gameObject.AddComponent<Boss2Split>();
    }

    public void Stats(float h, float d, float s, float fr)
    {
        hp = h;
        dmg = d;
        speed = s;
    }

    public void GetDamage(float damageTaken)
    {
        hp -= damageTaken;
        if(hp <= 0)
        {
            _split.splitStage++;
            Destroy(gameObject);
        }
    }
}

class Boss2Movement : MonoBehaviour
{
    Boss2 _boss2;
    float[] rngSmer = {-1f, 1f};
    float x;
    float y;
    Vector2 moveDirection;

    void Awake() {
        _boss2 = gameObject.GetComponent<Boss2>();

        x = rngSmer[Random.Range(0, rngSmer.Length)];
        y = rngSmer[Random.Range(0, rngSmer.Length)];
    }

    void Update()
    {
        moveDirection = new Vector2(x, y).normalized;
    }

    void FixedUpdate() 
    {
        _boss2.rb.velocity = new Vector2(moveDirection.x * _boss2.speed, moveDirection.y * _boss2.speed);
    }

    //player gets damage on collision with boss
    void OnCollisionEnter2D(Collision2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().GetDamage(_boss2.dmg);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) 
    {
        if(trigger.CompareTag("TileMap"))
        {
            foreach (BoxCollider2D boxTrigger in _boss2.triggers)
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

class Boss2Split : MonoBehaviour
{
    Boss2 _boss2;
    public int splitStage = 0;

    void Awake() {
        _boss2 = gameObject.GetComponent<Boss2>();   
    }

    void Update() 
    {
        Debug.Log(splitStage);

        Split();
    }

    void Split()
    {
        switch(splitStage)
        {
            case 1:
                foreach(GameObject splittedBoss in _boss2.splittedBosses1){
                    splittedBoss.SetActive(true);
                }
                break;
        }
    }
}
