using UnityEngine;

public class ProjétilDeCura : MonoBehaviour
{
    public float quantidadeDeCura = 50;
    public float velocidade = 5f;
    

    private Transform alvoDoProjétil;


    void Update()
    {
        if (alvoDoProjétil != null)
        {
            Vector3 direcao = (alvoDoProjétil.position - transform.position).normalized;
            transform.Translate(direcao * velocidade * Time.deltaTime);
        }
    }

    public void DefinirAlvo(Transform novoAlvo)
    {
        alvoDoProjétil = novoAlvo;  
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