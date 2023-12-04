using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public string enemyTag = "Enemy";

    private int enemyLayer;

    private void Start()
    {
        enemyLayer = LayerMask.NameToLayer(enemyTag);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            // Evitar colisão entre inimigos. Você pode adicionar código adicional aqui, como lidar com a colisão.
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
    }
}