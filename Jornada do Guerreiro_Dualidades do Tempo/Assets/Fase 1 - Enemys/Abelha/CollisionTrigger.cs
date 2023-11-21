using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public bool flip;
    public float projectileSpeed;
    public int playerHealth = 100;
    public float destroyDelay = 3f;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        // Destruir o objeto após um certo período de tempo
        anim.SetTrigger("morte");
        Destroy(gameObject, destroyDelay);
    }

    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth -= 1;

            if (playerHealth <= 0)
            {
                Destroy(other.gameObject);
                // Adicione aqui a lógica para lidar com o fim do jogo
            }
        }
    }
}
