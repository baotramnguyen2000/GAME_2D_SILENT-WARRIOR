using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkBox : MonoBehaviour
{
    enemyController enemyParent;
    private void Awake()
    {
        enemyParent = GetComponentInParent<enemyController>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyParent.setAtkEnemy(true);
        }
    }
}
