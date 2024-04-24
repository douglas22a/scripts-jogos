using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   
    [SerializeField] Rigidbody2D bulletRb;
    int speed = 10;
    //int lifeEnemyEasy = 1;
   // int lifeEnemyMedium = 2;
    //int lifeEnemyHard = 3;

    EnemyBehavior scriptEnemy;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        bulletRb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                
                    Destroy(collision.gameObject);
                
            }
            
            Destroy(gameObject);
        }
    }


}
