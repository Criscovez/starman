// EnemyPatrol.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;

    public float moveSpeed, waitAtPoints;
    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D theRB;

    public Animator anim;

    void Start()
    {
        waitCounter = waitAtPoints;

        foreach(Transform point in patrolPoints)
        {
            point.SetParent(null);
        }

    }

    void Update()
    {
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            if(transform.position.x < patrolPoints[currentPoint].position.x) 
            {
                theRB.velocity = new Vector2(moveSpeed,theRB.velocity.y);
                transform.localScale = Vector3.one;
                
            } else
            {
                theRB.velocity = new Vector2(- moveSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1f, 1, 1);

            }

            if (transform.position.y < patrolPoints[currentPoint].position.y - .5f && theRB.velocity.y < .1f)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

        } else
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoints;

                currentPoint = currentPoint + 1;

                if (currentPoint >= patrolPoints.Length) currentPoint = 0;

            }




        }

        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
    }

    
}
