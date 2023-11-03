using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    public float jumpForce = 4f;

    public int maxHealth = 100;
    public int currentHealth;
    [HideInInspector]
    public int sceneCurrent;
    [HideInInspector]
    public float timeValue;
    [HideInInspector]
    public List<string> listitemTag = new List<string>();

    public AudioClip jumpEffect;
    public AudioClip soundGameOver;
    public AudioClip soundHit;
    public AudioClip soundGetItem;
    public AudioClip soundWinner;
    public AudioClip soundFinishScene;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Animator animator;
    public HealthEnemy healthBar;


    TimeCountDown timeCountDown;
    AudioSource audioSource;
    BoxCollider2D boxcol2D;
    Rigidbody2D rb;
    GameController gc;
    UIController ui;
    PatientController patient;
    //CapsuleCollider2D poly2d;

    float xDir, yDir;
    bool isClimp;
    [HideInInspector]
    public bool isDeath;
    bool checkClimping;
    bool m_FacingRight = true;
    bool finish;
    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        boxcol2D = GetComponent<BoxCollider2D>();
        
        patient = FindObjectOfType<PatientController>();
        ui = FindObjectOfType<UIController>();
        audioSource = GetComponent<AudioSource>();
        timeCountDown = FindObjectOfType<TimeCountDown>();
        currentHealth = maxHealth;
        
        if (!gc.getNewGame() && gc.GetPlayAgain())
        {
            Debug.Log(gc.GetPlayAgain());
            transform.position = gc.startPlayer.position;
            gc.setPlayAgain(false);
        }
        else if(!gc.getNewGame())
        {
            LoadPlayer();
        }
        else
        {
            transform.position = gc.startPlayer.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //slider health player
        if (currentHealth != maxHealth)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
        //Nhan vat nhay
        if (Input.GetKeyDown(KeyCode.X) && isGrounded() && !checkClimping && !isDeath && !isWall() && !finish)
        {
            jumpPLayer();
            audioSource.PlayOneShot(jumpEffect);
        }
        //Nhan vat leo thang
        yDir = Input.GetAxis("Vertical");
        if (isClimp && Mathf.Abs(yDir) > 0)
        {
            checkClimping = true;
        }

        if (currentHealth == 0)
        {
            animator.SetTrigger("Death");
        }
        isWall();
        sceneCurrent = SceneManager.GetActiveScene().buildIndex;
        timeValue = timeCountDown.timeValue;
    }
    void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(soundHit);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isDeath = true;
            audioSource.PlayOneShot(soundGameOver);
        }
    }
    private void FixedUpdate()
    {
        //nhan vat khong cham vao tuong moi co the di chuyen
        if (!isDeath && !finish)
        {
            MovePLayer();
        }
        climpPlayer();
    }

    //ham di chuyen nhan vat va animation
    void MovePLayer()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(xDir));

        //xoay nhan vat theo chieu di chuyen
        if (xDir > 0 && m_FacingRight)
        {
            Flip();
        }
        else if (xDir < 0 && !m_FacingRight)
        {
            Flip();
        }
        rb.velocity = new Vector2(xDir * speed, rb.velocity.y);  
        
    }

    //ham nhan vat leo cau thang va animation
    void climpPlayer()
    {
        if (checkClimping)
        {
            rb.gravityScale = 0f;
            Vector3 climp = new Vector3(0, yDir, 0) * 2f * Time.deltaTime;
            transform.position += climp;
        }
        else
        {
            rb.gravityScale = 1f;
        }
        animator.SetBool("IsClimp", checkClimping);
    }
    //ham nhan vat nhay va animaiton
    void jumpPLayer()
    {
        rb.velocity = Vector2.up * jumpForce;
        animator.SetTrigger("IsJump");
    }
    //ham xoay nhanh vat
    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        // chuyen scale x =-1 de chuyen mat
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    bool isWall()
    {
        //kiem tra nhan vat co cham tuong hay khong
        Vector3 posCheck = boxcol2D.bounds.center + new Vector3(0, 0.7f, 0);
        RaycastHit2D raycastHit = Physics2D.Raycast(posCheck, m_FacingRight ? Vector2.left : Vector2.right, 0.2f, groundMask);

        return raycastHit.collider != null;
    }
    bool isGrounded()
    {
        return Physics2D.BoxCast(boxcol2D.bounds.center, boxcol2D.bounds.size, 0f, Vector2.down, 0.1f, groundMask);
    }
    //Khi animation Player vanish chay xong thi goi ham PlayerDeath de set game over true
    public void PlayerDeath()
    {
        gc.SetGameOver(true);
    }

    // load data (Chua update day du )
    void LoadPlayer() 
    {
        
        for(int i = 0;i< PlayerPrefs.GetInt("countList"); i++)
        {
            listitemTag.Add(PlayerPrefs.GetString("itemTag" + i));
        }

        Vector3 pos;
        pos.x = PlayerPrefs.GetFloat("position_x");
        pos.y = PlayerPrefs.GetFloat("position_y");
        pos.z = PlayerPrefs.GetFloat("position_z");
       
        if (PlayerPrefs.GetInt("nextLevel")==1)
        {
            transform.position = gc.startPlayer.position;
            listitemTag.Clear();
            currentHealth = maxHealth;
            PlayerPrefs.SetInt("nextLevel", 0);
        }
        else
        {
            transform.position = pos;
            timeCountDown.timeValue = PlayerPrefs.GetFloat("timeValue");
            foreach (string tagItem in listitemTag)
            {
                GameObject go = GameObject.FindGameObjectWithTag(tagItem);
                int id = go.GetInstanceID();
                Debug.Log("id_PlayerController:"+id);
                
                ui.itemActive(id);
                gc.IncreateItem();
                Destroy(go);
            }
            currentHealth = PlayerPrefs.GetInt("currentHealth");
        }
            
    }
    //Kiem tra cac va cham
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Cham vao cau thang
        if (collision.gameObject.tag == "Stair")
        {
            isClimp = true;
        }
        //Cham vao Item
        if (collision.gameObject.layer == 8)
        {
            audioSource.PlayOneShot(soundGetItem);
            int Id = collision.gameObject.GetInstanceID();
            string tagItem = collision.gameObject.tag;
            Destroy(collision.gameObject);
            gc.IncreateItem();
            listitemTag.Add(tagItem);

            ui.itemActive(Id);
        }
        // Cham vao Virus
        if (collision.gameObject.CompareTag("virus"))
        {
           
            TakeDamage(20);
            
        }
        //cham vao patient
        if (collision.gameObject.CompareTag("patient"))
        {
            if (gc.isEnoughItem)
            {
                if(SceneManager.GetActiveScene().buildIndex != 3)
                {
                    audioSource.PlayOneShot(soundFinishScene);
                }
                else
                {
                    audioSource.PlayOneShot(soundWinner);
                }
                animator.SetTrigger("finish");
                finish = true;
                rb.velocity = new Vector2(-1.5f, rb.velocity.y);
                patient.triggerRecover();
            }
            else
            {
                //Debug.Log("Not enough item");
                ui.FadeOutBubbleChat();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stair")
        {
            isClimp = false;
            checkClimping = false;
        }
    }
}
