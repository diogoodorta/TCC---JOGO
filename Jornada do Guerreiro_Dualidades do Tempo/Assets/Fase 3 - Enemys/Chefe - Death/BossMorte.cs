using System.Collections;
using UnityEngine;

public class BossMorte : MonoBehaviour
{
    public GameObject bolaDaMortePrefab;
    public Transform spawnPoint;

    void Start()
    {
        StartCoroutine(LancarBolas());
    }

    IEnumerator LancarBolas()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); // Intervalo entre os ataques
            InstanciarBolaDaMorte();
        }
    }

    void InstanciarBolaDaMorte()
    {
        GameObject bolaDaMorte = Instantiate(bolaDaMortePrefab, spawnPoint.position, Quaternion.identity);
        // Ajuste a velocidade da bola ou outras propriedades aqui, se necessário
    }
}