using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathScytheScript : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5.0f;
    public float distance = 10.0f;

    public int maxHealth = 100;
    private int currentHealth;

    public int damage;

    public Slider healthBar; // Referência à barra de vida no Unity
    public Animator bossAnimator; // Referência ao componente Animator do boss
    public GameObject victoryScreen; // Referência a uma tela de vitória ou outro objeto de tela


    public float boneWallCooldown = 120.0f;
    public float durationOfBoneWallAnimation = 2.0f; // Defina a duração da animação do Levantamento de Ossos.
    public float durationOfTemporalRipAnimation = 2.0f; // Defina a duração da animação do Rasgo Temporal.
    public float durationOfScytheDashAnimation = 2.0f; // Defina a duração da animação do Corte de Foice.
    public float durationOfMegaScytheAnimation = 2.0f; // Defina a duração da animação do Lançando Mega Foice.
    public float durationOfDeathScythesAnimation = 2.0f; // Defina a duração da animação das Foices da Morte.

    public float temporalRipCooldown = 90.0f;
    public float scytheDashCooldown = 60.0f;
    public float megaScytheCooldown = 180.0f;

    public float deathScythesCooldown = 15.0f;
    public float deathScytheSpeed = 10.0f; // Defina a velocidade de lançamento das Foices da Morte.
    public int deathScytheDamage = 10; // Defina o dano causado pelas Foices da Morte.
    public int numberOfDeathScythes = 3; // Defina o número de Foices da Morte lançadas.

    public GameObject deathScythePrefab; // Defina o prefab das Foices da Morte.
    public GameObject boneWallPrefab; // Defina o prefab da parede de ossos.

    private bool isUsingAbility = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth; // Inicialize a barra de vida com a vida máxima
        bossAnimator = GetComponent<Animator>();
        victoryScreen.SetActive(false);

        // Configurar os cooldowns dos ataques
        InvokeRepeating("BoneWallAttack", boneWallCooldown, boneWallCooldown);
        InvokeRepeating("TemporalRipAttack", temporalRipCooldown, temporalRipCooldown);
        InvokeRepeating("ScytheDashAttack", scytheDashCooldown, scytheDashCooldown);
        InvokeRepeating("MegaScytheAttack", megaScytheCooldown, megaScytheCooldown);
        InvokeRepeating("DeathScythesAttack", deathScythesCooldown, deathScythesCooldown);
    }

    private void Update()
    {
        Vector3 playerPosition = player.position;
        Vector3 bossPosition = transform.position;

        // Ajuste o boss para estar sempre posicionado no lado contrário do jogador.
        Vector3 direction = (playerPosition - bossPosition).normalized;
        Vector3 newPosition = playerPosition - direction * distance;
        transform.position = newPosition;

        // Use transform.Translate para mover o boss na direção do jogador.
        float step = moveSpeed * Time.deltaTime;
        transform.Translate(direction * step);
    }

    // Implemente a lógica de Levantamento de Ossos aqui.
    private void BoneWallAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative animações e efeitos visuais do ataque de Levantamento de Ossos.
            bossAnimator.SetTrigger("BoneWallAttack"); // Uma animação para esse ataque.

            // Implemente a lógica para criar uma parede de ossos que afeta o jogador.
            // Pode ser uma instância de um objeto de parede de ossos ou uma ação especial.
            // Certifique-se de definir o comportamento da parede de ossos, como colisões e duração.

            // Aplique dano ao jogador, se a parede de ossos atingir.

            // Desative isUsingAbility após a animação ou efeito.
            Invoke("FinishBoneWallAttack", durationOfBoneWallAnimation); // Defina a duração da animação.
        }
    }

    // Implemente a lógica do Rasgo Temporal aqui.
    private void TemporalRipAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative animações e efeitos visuais do ataque de Rasgo Temporal.
            bossAnimator.SetTrigger("TemporalRipAttack"); //Tenha uma animação para esse ataque.

            // Implemente a lógica do Rasgo Temporal, como a criação de efeitos visuais e dano ao jogador.

            // Desative isUsingAbility após a animação ou efeito.
            Invoke("FinishTemporalRipAttack", durationOfTemporalRipAnimation); // Defina a duração da animação.
        }
    }

    // Implemente a lógica do Corte de Foice aqui.
    private void ScytheDashAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative animações e efeitos visuais do ataque de Corte de Foice.
            bossAnimator.SetTrigger("ScytheDashAttack"); // Supondo que você tenha uma animação para esse ataque.

            // Implemente a lógica do Corte de Foice, como a movimentação do boss em alta velocidade em direção ao jogador.

            // Desative isUsingAbility após a animação ou efeito.
            Invoke("FinishScytheDashAttack", durationOfScytheDashAnimation); // Defina a duração da animação.
        }
    }

    // Implemente a lógica do Lançando Mega Foice aqui.
    private void MegaScytheAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative animações e efeitos visuais do ataque de Lançando Mega Foice.
            bossAnimator.SetTrigger("MegaScytheAttack"); // Supondo que você tenha uma animação para esse ataque.

            // Implemente a lógica do Lançando Mega Foice, como a criação de uma foice gigante que se move em direção ao jogador.

            // Desative isUsingAbility após a animação ou efeito.
            Invoke("FinishMegaScytheAttack", durationOfMegaScytheAnimation); // Defina a duração da animação.
        }
    }

    // Implemente a lógica das Foices da Morte aqui.
    private void DeathScythesAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative animações e efeitos visuais das Foices da Morte.
            bossAnimator.SetTrigger("DeathScythesAttack"); //Tenha uma animação para esse ataque.

            // Implemente a lógica para criar e lançar as Foices da Morte.
            LaunchDeathScythes();

            // Desative isUsingAbility após a animação ou efeito.
            Invoke("FinishDeathScythesAttack", durationOfDeathScythesAnimation); // Defina a duração da animação.
        }
    }

    // Método para criar e lançar as Foices da Morte.
    private void LaunchDeathScythes()
    {
        // Implemente a lógica para criar e lançar as Foices da Morte em direção ao jogador.
        for (int i = 0; i < numberOfDeathScythes; i++)
        {
            // Crie uma instância da "Foice da Morte" (GameObject que representa a foice).
            GameObject deathScythe = Instantiate(deathScythePrefab, transform.position, Quaternion.identity);

            // Determine a direção em que a foice será lançada (em direção ao jogador).
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Aplique uma força para mover a foice na direção do jogador.
            Rigidbody rb = deathScythe.GetComponent<Rigidbody>();
            rb.velocity = directionToPlayer * deathScytheSpeed;

            // Defina o dano que a foice causará ao jogador.
            DeathScytheScript deathScytheScript = deathScythe.GetComponent<DeathScytheScript>();
            deathScytheScript.damage = deathScytheDamage;
        }
    }

    private void FinishBoneWallAttack()
    {
        isUsingAbility = false;
    }

    private void FinishTemporalRipAttack()
    {
        isUsingAbility = false;
    }

    private void FinishScytheDashAttack()
    {
        isUsingAbility = false;
    }

    private void FinishMegaScytheAttack()
    {
        isUsingAbility = false;
    }

    private void FinishDeathScythesAttack()
    {
        isUsingAbility = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }

    private void Die()
    {
        bossAnimator.SetTrigger("Death");

        // Desative o comportamento do boss, desativando o script de movimento, se aplicável.
        // Inicie uma tela de vitória ou outra ação apropriada.
        victoryScreen.SetActive(true);
    }
}