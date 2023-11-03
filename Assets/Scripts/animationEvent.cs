using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationEvent : MonoBehaviour
{
    enemyController enemyParent;
    public Animator anim;
    private void Awake()
    {
        enemyParent = GetComponentInParent<enemyController>();
    }
    // Start is called before the first frame update
    public void finishAtk()
    {
        enemyParent.setAtkEnemy(false);
        anim.SetBool("atk", false);
    }
    public void destroyVirus()
    {
        Destroy(transform.parent.parent.gameObject);
    }
    private void Update()
    {
        if (enemyParent.attackPlayer)
        {
            anim.SetBool("atk", true);   
        }
        if (enemyParent.isDeath)
        {
            anim.SetTrigger("death");
        }
    }
}
