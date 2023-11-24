using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeFase : MonoBehaviour
{
    public string nomeDaFase;
    public float raioDetecao = 1.5f;
    public GameObject teclaSpace; // Objeto da tecla "Space" para ativar/desativar

    private Transform jogadorTransform; // Refer�ncia para o transform do jogador

    private void Start()
    {
        if (teclaSpace != null)
        {
            // Desativa a tecla "Space" no in�cio
            teclaSpace.SetActive(false);
        }

        // Encontra o jogador pelo identificador �nico ou tag
        jogadorTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Verifica se o jogador est� sobre o portal
        bool jogadorSobrePortal = EstaSobreOPortal();

        // Ativa/desativa a tecla "Space" conforme a presen�a do jogador sobre o portal
        if (teclaSpace != null)
        {
            teclaSpace.SetActive(jogadorSobrePortal);
        }

        // Verifica se o jogador pressionou a tecla de espa�o
        if (jogadorSobrePortal && Input.GetKeyDown(KeyCode.Space))
        {
            // Carrega a nova cena
            CarregarFase();
        }
    }

    private void CarregarFase()
    {
        // Move o jogador para a nova posi��o (ajuste conforme necess�rio)
        jogadorTransform.position = new Vector3(10f, 2f, 0f);

        // Carrega a nova cena
        SceneManager.LoadScene(nomeDaFase);
    }

    // Verifica se o jogador est� sobre o portal
    private bool EstaSobreOPortal()
    {
        // Obt�m a posi��o do jogador
        Vector2 posicaoDoJogador = jogadorTransform.position;

        // Obt�m a posi��o do portal
        Vector2 posicaoDoPortal = transform.position;

        // Verifica se a dist�ncia entre o jogador e o portal � menor que o raio de detec��o
        bool jogadorSobrePortal = Vector2.Distance(posicaoDoJogador, posicaoDoPortal) < raioDetecao;

        return jogadorSobrePortal;
    }
}
