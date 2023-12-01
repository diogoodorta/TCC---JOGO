using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 3;
    private bool isCooldownActive = false;
    private bool isTrigger = false; // Flag para indicar o estado Is Trigger

    // Adicione refer�ncias aos scripts de movimento e combate, se necess�rio
    private move playerMoveScript;
    private Combate playerCombatScript;
    private Animator playerAnimator;

    void Start()
    {
        health = maxHealth;
        playerMoveScript = GetComponent<move>();
        playerCombatScript = GetComponent<Combate>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0 && !isTrigger)
        {
            // Se a vida chegar a zero e n�o estivermos no estado Is Trigger, execute a��es de desligamento
            PararTodasAcoes();
        }
    }

    void PararTodasAcoes()
    {

        // Desativar controles de movimento
        if (playerMoveScript != null)
        {
            playerMoveScript.isControllable = false;
            playerMoveScript.enabled = false;
        }

        // Desativar controles de combate
        if (playerCombatScript != null)
        {
            playerCombatScript.isAttackable = false;
            playerCombatScript.enabled = false;
        }

        // Desligar colis�es
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        // Congelar a posi��o
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Defina a flag isTrigger como true
        isTrigger = true;

        // Iniciar anima��o de morte
        if (playerAnimator != null)
        {
            // Desativar outras camadas de anima��o al�m da camada padr�o (Layer 0)
            for (int i = 1; i < playerAnimator.layerCount; i++)
            {
                playerAnimator.SetLayerWeight(i, 0);
            }

            playerAnimator.SetTrigger("Morte");
        }

        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        isCooldownActive = true;

        yield return new WaitForSeconds(4f);

        // Redefina a flag isTrigger
        isTrigger = false;

        isCooldownActive = false;

        // Destruir o jogador ap�s o cooldown
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        if (!isCooldownActive)
        {
            health -= amount;
            Debug.Log("Vida do jogador: " + health);

            if (health <= 0)
            {
                PararTodasAcoes();
            }
        }
    }

    public void TakeDamageKick(int amount)
    {
        if (!isCooldownActive)
        {
            health -= amount;
            Debug.Log("Jogador atingido pelo Kick! Vida do jogador: " + health);

            if (health <= 0)
            {
                PararTodasAcoes();
            }
        }
    }

    public void ReceberDano(int amount)
    {
        TakeDamage(amount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colis�o com: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
             IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("O inimigo n�o implementa a interface IDamageable.");
            }
            
        }
    }
}
