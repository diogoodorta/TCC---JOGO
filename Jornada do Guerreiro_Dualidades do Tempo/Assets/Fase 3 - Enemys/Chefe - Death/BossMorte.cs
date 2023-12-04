using UnityEngine;
using System.Collections;

public class BossMorte : MonoBehaviour
{
    public GameObject bolaDaMortePrefab;
    public Transform spawnPoint;
    public float cooldown = 3f; // Tempo de cooldown em segundos
    public int health = 100; // ou o valor desejado

    private float lastAttackTime;

    void Start()
    {
        lastAttackTime = -cooldown; // Inicia com o cooldown completo para permitir o primeiro lançamento
        StartCoroutine(LancarBolas());
    }

    IEnumerator LancarBolas()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);

            // Verifica se o cooldown foi concluído
            if (Time.time - lastAttackTime >= cooldown)
            {
                InstanciarBolaDaMorte();
                lastAttackTime = Time.time; // Atualiza o tempo do último lançamento
            }
        }
    }

    void InstanciarBolaDaMorte()
    {
        GameObject bolaDaMorte = Instantiate(bolaDaMortePrefab, spawnPoint.position, Quaternion.identity);
        // Ajuste a velocidade da bola ou outras propriedades aqui, se necessário
    }

    public void SofrerDano(int quantidade)
    {
        health -= quantidade;
        Debug.Log("Boss sofreu " + quantidade + " de dano. Vida restante: " + health);

        if (health <= 0)
        {
            health = 0;
            Debug.Log("Boss derrotado!");
            DestruirBoss();
        }
    }

    // Remova esse método se não estiver usando
    void DestruirBoss()
    {
        // Adicione qualquer lógica adicional antes de destruir o Boss
        Debug.Log("Destruindo o Boss.");
        Destroy(gameObject);
    }
}
