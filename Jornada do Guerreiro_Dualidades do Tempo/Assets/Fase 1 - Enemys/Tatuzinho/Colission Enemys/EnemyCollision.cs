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
            // Evitar colis�o entre inimigos. Voc� pode adicionar c�digo adicional aqui, como lidar com a colis�o.
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
    }
}