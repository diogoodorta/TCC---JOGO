using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abelha : MonoBehaviour
{
    int side = 1;
    public GameObject player;
    public bool flip;
    public Transform bullet;
    public Transform pivot;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;

        //atirar projeteis
        timer += Time.deltaTime;
        transform.right = Vector2.right * side;

        if (timer > 2)
        {
            timer = 0;
            Instantiate(bullet, pivot.position, transform.rotation);

        }
    }
}
