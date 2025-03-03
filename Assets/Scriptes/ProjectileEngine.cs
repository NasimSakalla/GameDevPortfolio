using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectileEngine : MonoBehaviour
{
    [SerializeField] Renderer projectileRend;
    public static int howManyDied;


    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
        DestroyProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            howManyDied++;
            PlayerScore.PlayerScoreValue += 100;
        }
    }

    void DestroyProjectile()
    {
        if (!projectileRend.isVisible)
        {
            Destroy(gameObject);
        }
    }

    void MoveProjectile()
    {
        transform.Translate(0, 4 * Time.deltaTime, 0);
    }
}
