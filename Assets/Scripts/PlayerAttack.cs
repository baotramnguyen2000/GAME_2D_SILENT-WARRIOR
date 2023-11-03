using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackTime;
    public float startTimeAttack;
    
    AudioSource audioSource;
    Animator anim;
    public AudioClip soundAtk;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemies;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        anim = GetComponentInParent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            audioSource.PlayOneShot(soundAtk);
            Attack();
        }
    }
    void Attack()
    {
        anim.SetTrigger("atk");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);
        
        foreach (Collider2D virus in hitEnemies)
        {
            virus.GetComponentInParent<enemyController>().TakeDamage(50);
        }
    }
}

