using UnityEngine;

public class Proj�tilDeCura : MonoBehaviour
{
    public float quantidadeDeCura = 4;
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
            Debug.Log("Atualizando Proj�til de Cura");

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
        if (other.gameObject.CompareTag("Enemy"))
        {
           SoldadoPossuido soldade = other.GetComponent<SoldadoPossuido>();
            if (soldade != null)
            {
                soldade.ReceberCura(quantidadeDeCura);
                Destroy(gameObject);
                Debug.Log("Aliado recebeu cura!");
            }
        }
    }
}
