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

    public Slider healthBar; // Refer�ncia � barra de vida no Unity
    public Animator bossAnimator; // Refer�ncia ao componente Animator do boss
    public GameObject victoryScreen; // Refer�ncia a uma tela de vit�ria ou outro objeto de tela


    public float boneWallCooldown = 120.0f;
    public float durationOfBoneWallAnimation = 2.0f; // Defina a dura��o da anima��o do Levantamento de Ossos.
    public float durationOfTemporalRipAnimation = 2.0f; // Defina a dura��o da anima��o do Rasgo Temporal.
    public float durationOfScytheDashAnimation = 2.0f; // Defina a dura��o da anima��o do Corte de Foice.
    public float durationOfMegaScytheAnimation = 2.0f; // Defina a dura��o da anima��o do Lan�ando Mega Foice.
    public float durationOfDeathScythesAnimation = 2.0f; // Defina a dura��o da anima��o das Foices da Morte.

    public float temporalRipCooldown = 90.0f;
    public float scytheDashCooldown = 60.0f;
    public float megaScytheCooldown = 180.0f;

    public float deathScythesCooldown = 15.0f;
    public float deathScytheSpeed = 10.0f; // Defina a velocidade de lan�amento das Foices da Morte.
    public int deathScytheDamage = 10; // Defina o dano causado pelas Foices da Morte.
    public int numberOfDeathScythes = 3; // Defina o n�mero de Foices da Morte lan�adas.

    public GameObject deathScythePrefab; // Defina o prefab das Foices da Morte.
    public GameObject boneWallPrefab; // Defina o prefab da parede de ossos.

    private bool isUsingAbility = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth; // Inicialize a barra de vida com a vida m�xima
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

        // Ajuste o boss para estar sempre posicionado no lado contr�rio do jogador.
        Vector3 direction = (playerPosition - bossPosition).normalized;
        Vector3 newPosition = playerPosition - direction * distance;
        transform.position = newPosition;

        // Use transform.Translate para mover o boss na dire��o do jogador.
        float step = moveSpeed * Time.deltaTime;
        transform.Translate(direction * step);
    }

    // Implemente a l�gica de Levantamento de Ossos aqui.
    private void BoneWallAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative anima��es e efeitos visuais do ataque de Levantamento de Ossos.
            bossAnimator.SetTrigger("BoneWallAttack"); // Uma anima��o para esse ataque.

            // Implemente a l�gica para criar uma parede de ossos que afeta o jogador.
            // Pode ser uma inst�ncia de um objeto de parede de ossos ou uma a��o especial.
            // Certifique-se de definir o comportamento da parede de ossos, como colis�es e dura��o.

            // Aplique dano ao jogador, se a parede de ossos atingir.

            // Desative isUsingAbility ap�s a anima��o ou efeito.
            Invoke("FinishBoneWallAttack", durationOfBoneWallAnimation); // Defina a dura��o da anima��o.
        }
    }

    // Implemente a l�gica do Rasgo Temporal aqui.
    private void TemporalRipAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative anima��es e efeitos visuais do ataque de Rasgo Temporal.
            bossAnimator.SetTrigger("TemporalRipAttack"); //Tenha uma anima��o para esse ataque.

            // Implemente a l�gica do Rasgo Temporal, como a cria��o de efeitos visuais e dano ao jogador.

            // Desative isUsingAbility ap�s a anima��o ou efeito.
            Invoke("FinishTemporalRipAttack", durationOfTemporalRipAnimation); // Defina a dura��o da anima��o.
        }
    }

    // Implemente a l�gica do Corte de Foice aqui.
    private void ScytheDashAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative anima��es e efeitos visuais do ataque de Corte de Foice.
            bossAnimator.SetTrigger("ScytheDashAttack"); // Supondo que voc� tenha uma anima��o para esse ataque.

            // Implemente a l�gica do Corte de Foice, como a movimenta��o do boss em alta velocidade em dire��o ao jogador.

            // Desative isUsingAbility ap�s a anima��o ou efeito.
            Invoke("FinishScytheDashAttack", durationOfScytheDashAnimation); // Defina a dura��o da anima��o.
        }
    }

    // Implemente a l�gica do Lan�ando Mega Foice aqui.
    private void MegaScytheAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative anima��es e efeitos visuais do ataque de Lan�ando Mega Foice.
            bossAnimator.SetTrigger("MegaScytheAttack"); // Supondo que voc� tenha uma anima��o para esse ataque.

            // Implemente a l�gica do Lan�ando Mega Foice, como a cria��o de uma foice gigante que se move em dire��o ao jogador.

            // Desative isUsingAbility ap�s a anima��o ou efeito.
            Invoke("FinishMegaScytheAttack", durationOfMegaScytheAnimation); // Defina a dura��o da anima��o.
        }
    }

    // Implemente a l�gica das Foices da Morte aqui.
    private void DeathScythesAttack()
    {
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Ative anima��es e efeitos visuais das Foices da Morte.
            bossAnimator.SetTrigger("DeathScythesAttack"); //Tenha uma anima��o para esse ataque.

            // Implemente a l�gica para criar e lan�ar as Foices da Morte.
            LaunchDeathScythes();

            // Desative isUsingAbility ap�s a anima��o ou efeito.
            Invoke("FinishDeathScythesAttack", durationOfDeathScythesAnimation); // Defina a dura��o da anima��o.
        }
    }

    // M�todo para criar e lan�ar as Foices da Morte.
    private void LaunchDeathScythes()
    {
        // Implemente a l�gica para criar e lan�ar as Foices da Morte em dire��o ao jogador.
        for (int i = 0; i < numberOfDeathScythes; i++)
        {
            // Crie uma inst�ncia da "Foice da Morte" (GameObject que representa a foice).
            GameObject deathScythe = Instantiate(deathScythePrefab, transform.position, Quaternion.identity);

            // Determine a dire��o em que a foice ser� lan�ada (em dire��o ao jogador).
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Aplique uma for�a para mover a foice na dire��o do jogador.
            Rigidbody rb = deathScythe.GetComponent<Rigidbody>();
            rb.velocity = directionToPlayer * deathScytheSpeed;

            // Defina o dano que a foice causar� ao jogador.
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

        // Desative o comportamento do boss, desativando o script de movimento, se aplic�vel.
        // Inicie uma tela de vit�ria ou outra a��o apropriada.
        victoryScreen.SetActive(true);
    }
}