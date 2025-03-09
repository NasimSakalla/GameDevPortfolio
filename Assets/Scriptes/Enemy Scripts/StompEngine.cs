using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StompEngine : MonoBehaviour
{
    private int enemyHp;
    private float playerHP;
    private Transform playerHeight;
    private UnityEngine.UI.Image playerHealthBar;

    private void Awake()
    {
        playerHeight = GameObject.Find("Player").transform;
        playerHealthBar = GameObject.Find("Foreground Of HP Bar").GetComponent<UnityEngine.UI.Image>();
    }
    private void Start()
    {
        enemyHp = 0;
        playerHP = 1;
        playerHealthBar.fillAmount = playerHP;
    }

    void ShouldEnemyDie()
    {
        // checking if an enemy took two hits if so kill him
        if (enemyHp % 2 == 0 && enemyHp != 0)
        {
            Destroy(gameObject);
        }
    }
    void ShouldPlayerDie()
    {
        //make sure player hp stays the same as the fill amount
        playerHealthBar.fillAmount = playerHP;

        if (playerHP <= 0.01)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // lower player hp if he touches them head-on
        if (collision.gameObject.CompareTag("Player") && transform.position.y +2 > playerHeight.position.y )
        {
            playerHP -= 0.2f;
            //load loss scene if player died
            ShouldPlayerDie();
        }

        // allow player to stomp enemy
        if (collision.gameObject.CompareTag("Player") && transform.position.y +2 < playerHeight.position.y )
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
        // kill enemy and destroy arrow on impact
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            enemyHp++;
        }

        //after getting hit with an arrow check if he is spsd to die
        ShouldEnemyDie();
    }
}
