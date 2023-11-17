using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abelha : MonoBehaviour
{
    public int side = 1;
    public GameObject player;
    public bool flip;
    public Transform bullet;
    public Transform pivot;
    public float agroRange; // Defina o valor correto
    public Transform castPoint; // Defina o Transform correto
    public float speed;
    public Animator animator;

    private bool morrendo = false; // Flag para controlar se a abelha está morrendo
    private float timer;
    private Rigidbody2D rb;
    private bool IsFacingLeft;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Corrigi a refer�ncia a face
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.right = Vector2.right * side;
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer < agroRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            if (timer < 1)  
            {
                animator.SetBool("beAtacando", false);
            }

            if(timer > 2.8)
            {
                 animator.SetBool("beAtacando", true);
            }

            if (timer > 3)
            {
                Instantiate(bullet, pivot.position, transform.rotation);
                timer = 0;
            }

            
            

        }

        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        transform.localScale = scale;

        // Chame a fun��o CanSeePlayer e use seu retorno
        bool canSee = CanSeePlayer(10f); // Defina a dist�ncia correta
        if (canSee)
        {
            // Fa�a algo se o jogador estiver vis�vel
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (IsFacingLeft)
        {
            castDist = -distance;
        }
        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow); // Corrigi a cor do Debug.DrawLine
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        return val; // Retorne o valor
    }

    private void Morrer()
    {
        // Inicie a animação de morte ou qualquer lógica necessária
        if (animator != null)
        {
            animator.SetTrigger("MorrerTrigger"); // Ativar o trigger de morte
            morrendo = true; // Marcar a abelha como morrendo
        }

        // Aguarde um tempo antes de destruir o GameObject (opcional)
        StartCoroutine(DestruirAposAnimacao());
    }

    IEnumerator DestruirAposAnimacao()
    {
        // Aguarde o tempo da animação de morte
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Aguarde o término da animação

        // Destrua o GameObject
        Destroy(gameObject);
    }
}
