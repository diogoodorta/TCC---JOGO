using UnityEngine;


public class Aliado : MonoBehaviour
{
    public float vida = 30;

    public void ReceberCura(float quantidadeDeCura)
    {
        vida += quantidadeDeCura;
        // Adicione outras l�gicas relacionadas � cura, se necess�rio.
    }
}