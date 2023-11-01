using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //currentPoint = pointB.transform;
        //anim.SetBool("larva", true); 
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                transform.localScale = new Vector3(3, 3, 3);
                patrolDestination = 1;
            }
        }

        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                transform.localScale = new Vector3(-3, 3, 3);
                patrolDestination = 0;
            }
        }
    }

    //private void flip()
    //{
    //Vector3 localScale = transform,localScale;
    //localScale.x += -1;
    //transform.localScale = localScale;
    //}

    //private void OnDrawGizmos()
    //{
    //Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
    //Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
    //Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    //}
}