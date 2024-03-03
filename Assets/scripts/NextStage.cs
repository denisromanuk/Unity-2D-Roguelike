using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    private Animator _animator;
    public bool bossdead = false;

    void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    }

    void Update() 
    {
        if(bossdead){
            _animator.SetInteger("closed", 1);
        }
        if(!bossdead){
            _animator.SetInteger("closed", 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player" && bossdead)
        {
            DontDestroyOnLoad(collider);
            collider.transform.position = new Vector3(0, -2, 10);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
