using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public bool flip;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 1f);
        rb = GetComponent<Rigidbody2D>();//.AddForce(Vector3.left * 1000);
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

    }

    void Update()
    {
        Vector3 scale = transform.localScale;

        //if (player.transform.position.x > transform.position.x)
        //{
            //scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        //}
        //else
        //{
            //scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        //}

        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }

    }

}