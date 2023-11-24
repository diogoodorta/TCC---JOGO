using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeFase : MonoBehaviour
{
    public string nomeDaFase;
    public float raioDetecao = 1.5f;
    public GameObject teclaSpace; // Objeto da tecla "Space" para ativar/desativar

    private Transform jogadorTransform; // Referência para o transform do jogador

    private void Start()
    {
        if (teclaSpace != null)
        {
            // Desativa a tecla "Space" no início
            teclaSpace.SetActive(false);
        }

        // Encontra o jogador pelo identificador único ou tag
        jogadorTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Verifica se o jogador está sobre o portal
        bool jogadorSobrePortal = EstaSobreOPortal();

        // Ativa/desativa a tecla "Space" conforme a presença do jogador sobre o portal
        if (teclaSpace != null)
        {
            teclaSpace.SetActive(jogadorSobrePortal);
        }

        // Verifica se o jogador pressionou a tecla de espaço
        if (jogadorSobrePortal && Input.GetKeyDown(KeyCode.Space))
        {
            // Carrega a nova cena
            CarregarFase();
        }
    }

    private void CarregarFase()
    {
        // Move o jogador para a nova posição (ajuste conforme necessário)
        jogadorTransform.position = new Vector3(10f, 2f, 0f);

        // Carrega a nova cena
        SceneManager.LoadScene(nomeDaFase);
    }

    // Verifica se o jogador está sobre o portal
    private bool EstaSobreOPortal()
    {
        // Obtém a posição do jogador
        Vector2 posicaoDoJogador = jogadorTransform.position;

        // Obtém a posição do portal
        Vector2 posicaoDoPortal = transform.position;

        // Verifica se a distância entre o jogador e o portal é menor que o raio de detecção
        bool jogadorSobrePortal = Vector2.Distance(posicaoDoJogador, posicaoDoPortal) < raioDetecao;

        return jogadorSobrePortal;
    }
}
