using UnityEngine;

public class Proj�tilDeCura : MonoBehaviour
{
    public float quantidadeDeCura = 50;
    public float velocidade = 5f;

    private Transform alvoDoProj�til;
    private Animation scaleAnimation;

    void Start()
    {
        
    }

    void Update()
    {
        if (alvoDoProj�til != null)
        {
            Vector3 direcao = (alvoDoProj�til.position - transform.position).normalized;
            transform.Translate(direcao * velocidade * Time.deltaTime);

            if (!scaleAnimation.isPlaying)
            {
                scaleAnimation.Play();
            }
        }
    }

    public void DefinirAlvo(Transform novoAlvo)
    {
        alvoDoProj�til = novoAlvo;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ally"))
        {
            Aliado aliado = other.GetComponent<Aliado>();
            if (aliado != null)
            {
                aliado.ReceberCura(quantidadeDeCura);
                Destroy(gameObject);
                Debug.Log("Aliado recebeu cura!");
            }
        }
    }
}