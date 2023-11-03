using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    public float speedEnemy = 0.7f;
    public Transform leftLimit;
    public Transform rightLimit;
    public LayerMask groundMask;
    public HealthEnemy healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    LootScript virus;
    Transform targetPoint;
    CircleCollider2D cir2D;
    AudioSource audioSource;
    
    public AudioClip soundDie;
   
    

    bool facingRight;
    bool followPlayer;
    [HideInInspector]
    public bool attackPlayer;
    [HideInInspector]
    public bool isDeath;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cir2D = GetComponentInChildren<CircleCollider2D>();
        virus = GetComponent<LootScript>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        rb.gravityScale = 0;
        SelectTarget();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(maxHealth != currentHealth)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
        if (!InsideOfLimit() && !followPlayer)
        {
            SelectTarget();
        }
        if (isWall())
        {
            SelectTarget();
        }
    }
    void FixedUpdate()
    {
        if (!attackPlayer && !isDeath)
        {
            MoveIdle();
        }
    }
    bool InsideOfLimit()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight) {
            targetPoint = leftLimit;
            
        }
        else {
            targetPoint = rightLimit;
            
        }
        flipEnemy();
    }
    void MoveIdle()
    {
        Vector2 targetPosition = new Vector2(targetPoint.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speedEnemy * Time.deltaTime);
    }
    public void setAtkEnemy(bool isAtk)
    {
        attackPlayer = isAtk;
    }
    bool isWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(cir2D.bounds.center, facingRight ? Vector2.right : Vector2.left, 0.2f, groundMask);
        return raycastHit.collider != null;
    }
    void flipEnemy()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            followPlayer = true;
            if(facingRight && collision.transform.position.x < transform.position.x)
            {
                flipEnemy();
            }
            else if(!facingRight && collision.transform.position.x > transform.position.x)
            {
                flipEnemy();
            }
            targetPoint = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            followPlayer = false;
            if (InsideOfLimit())
            {
                SelectTarget();
            }
        }
    }
    //quái nhận damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            audioSource.PlayOneShot(soundDie);
            Die();
            
        }
    }
    //xử lí trạng thái chết của enemy
    void Die()
    {
        //Nhập trạng thái chết và rớt đồ
        Debug.Log("Enemy Die");
        cir2D.enabled = false;
        isDeath = true;
        virus.calculateLoot();
    }
}
