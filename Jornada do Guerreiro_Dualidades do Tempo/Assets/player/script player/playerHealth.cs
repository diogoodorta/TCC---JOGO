using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 3;
    private bool isCooldownActive = false;
    private bool isTrigger = false; // Flag para indicar o estado Is Trigger

    // Adicione referências aos scripts de movimento e combate, se necessário
    private movimentação playerMoveScript;
    private Combate playerCombatScript;
    private Animator playerAnimator;

    void Start()
    {
        health = maxHealth;
        playerMoveScript = GetComponent<movimentação>();
        playerCombatScript = GetComponent<Combate>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0 && !isTrigger)
        {
            // Se a vida chegar a zero e não estivermos no estado Is Trigger, execute ações de desligamento
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

        // Desligar colisões
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        // Congelar a posição
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Defina a flag isTrigger como true
        isTrigger = true;
        Debug.Log("O jogador está parando todas as ações devido à saúde baixa.");

        // Iniciar animação de morte
        playerAnimator.SetTrigger("Morte");

        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        isCooldownActive = true;

        yield return new WaitForSeconds(4f);

        // Redefina a flag isTrigger
        isTrigger = false;

        isCooldownActive = false;

        // Destruir o jogador após o cooldown
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        if (!isCooldownActive)
        {
            health -= amount;
            Debug.Log("Player foi atingido! Dano: " + amount);

            if (health <= 0)
            {
                PararTodasAcoes();
            }
        }
    }

    public void TakeDamageBola(int amount)
    {
        if (!isCooldownActive)
        {
            health -= amount;
            Debug.Log("Player foi atingido pela BolaDaMorte! Dano: " + amount + " | Vida restante: " + health);

            if (health <= 0)
            {
                Debug.Log("Player sem vida. Executando ações de morte.");
                PararTodasAcoes();
            }
        }
        else
        {
            Debug.Log("Cooldown ativo. O jogador não recebe dano.");
        }
    }


    public void TakeDamageKick(int amount)
    {
        if (!isCooldownActive)
        {
            health -= amount;
            Debug.Log("Player taking kick damage: " + amount);

            if (health <= 0)
            {
                PararTodasAcoes();
            }
        }
    }

    public void TakeDamageLarva(int quantidade)
    {
        Debug.Log("Jogador atingido pela Larva! Dano: " + quantidade);

        if (!isCooldownActive)
        {
            health -= quantidade;

            if (health <= 0)
            {
                Debug.Log("Player sem vida. Executando ações de morte.");
                PararTodasAcoes();
            }
        }
    }

    public void TakeDamageEspada(int amount)
    {
        // Verifique se o cooldown não está ativo antes de aplicar o dano
        if (!isCooldownActive)
        {
            health -= amount;
            Debug.Log("Jogador atingido pela 'Espada' com a espada! Vida do jogador: " + health);

            // Verifique se a vida do jogador chegou a zero ou menos
            if (health <= 0)
            {
                PararTodasAcoes();
            }
        }
    }

    public void TakeDamageZombie(int amount)
    {
        if (!isCooldownActive)
        {
            Debug.Log("Player tomou dano Zombie! Damage: " + amount);
            health -= amount;
            Debug.Log("Player tomou dano Zombie! Player's health: " + health);

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
        Debug.Log("Colisão com: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
            else
            {
                // Lógica para lidar com a colisão, se necessário
            }
        }

    }
}
