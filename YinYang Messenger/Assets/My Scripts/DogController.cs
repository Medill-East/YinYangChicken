using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public GameObject patrolCenter;

    public float movementSpeed = 3;

    public bool turnRight;

    //public bool isPatrolling;
    public bool isChasing;
    public bool isBacking;

    public static Vector3 moveDirection;
    private static Vector3 moveLeft = new Vector3(-1f, 0f, 0f);
    private static Vector3 moveRight = new Vector3(1f, 0f, 0f);

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        //isPatrolling = true;
        isChasing = false;
        isBacking = false;

        Vector3 newDirection;
        newDirection = patrolCenter.transform.position - transform.position;
        moveDirection = newDirection;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isChasing == false)
        //{
        //    Patrol();
        //}
        //else if (isChasing)
        //{
        //    Move(moveDirection);
        //}

        AdjustRotation();

        // wait to chase
        if (isBacking && !isChasing)
        {
            GoBack();

            // if back to patrol center, stay
            if (MyApproximation(gameObject.transform.position.x,
                                patrolCenter.transform.position.x,
                                0.1f)
                               )
            {
                isBacking = false;
                gameObject.transform.position = new Vector3(patrolCenter.transform.position.x,
                                                            gameObject.transform.position.y,
                                                            gameObject.transform.position.z);
            }
        }

    }

    private bool MyApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if chased yang, kill
        if (collision.gameObject.tag == "Yang")
        {
            Kill(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //Debug.Log(other.gameObject.tag);

        // if exit patrol range, then go back
        if (other.gameObject.tag == "PatrolPoint")
        {
            Debug.Log("Dog prepare to go back!");
            isBacking = true;


            //// dog located at the right of patrolcenter
            //// move left
            //if(distance > 0.5f)
            //{
            //    rb.velocity = new Vector3(-5f, 0, 0);
            //}
            //// dog located at the left of patrolcenter
            //// move right
            //else if(distance < -0.5f)
            //{
            //    rb.velocity = new Vector3(5f, 0, 0);
            //}

        }
    }

    void AdjustRotation()
    {
        float moveDirection = transform.localPosition.x;

        // control rotation
        if (moveDirection < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0f);
        }
        else if (moveDirection > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0f);
        }
    }

    void Move(Vector3 newDirection)
    {
        moveDirection = newDirection;
        transform.Translate(moveDirection.normalized * movementSpeed * Time.deltaTime, Space.World);
    }

    void Patrol()
    {
        Move(moveDirection);
    }

    public void GoBack()
    {
        Vector3 newDirection;
        newDirection = patrolCenter.transform.position - transform.position;
        Move(newDirection);
        Debug.Log("Dog backing to patrol center!");
    }

    public void Chase(GameObject target)
    {
        isChasing = true;

        Vector3 newDirection;
        newDirection = target.transform.position - transform.position;

        // move toward target
        Move(newDirection);

        Debug.Log("Dog chasing!");
    }

    public void Escape(GameObject target)
    {
        Vector3 newDirection;
        newDirection = transform.position - target.transform.position;

        // move toward target
        Move(newDirection);

        Debug.Log("Dog escaping!");
    }

    public void Kill(GameObject target)
    {
        Destroy(target);
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().Death();
    }
}
