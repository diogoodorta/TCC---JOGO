using UnityEngine;

public class ProjétilDeCura : MonoBehaviour
{
    public float quantidadeDeCura = 4;
    public float velocidade = 5f;

    private Transform alvoDoProjétil;
    private Animation scaleAnimation;

    void Start()
    {

    }

    void Update()
    {
        if (alvoDoProjétil != null)
        {
            Debug.Log("Atualizando Projétil de Cura");

            Vector3 direcao = (alvoDoProjétil.position - transform.position).normalized;
            transform.Translate(direcao * velocidade * Time.deltaTime);

            if (!scaleAnimation.isPlaying)
            {
                scaleAnimation.Play();
            }
        }
    }

    public void DefinirAlvo(Transform novoAlvo)
    {
        alvoDoProjétil = novoAlvo;
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
