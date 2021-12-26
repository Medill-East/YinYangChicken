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

    public static Vector3 moveDirection;
    private static Vector3 moveLeft = new Vector3(-1f, 0f, 0f);
    private static Vector3 moveRight = new Vector3(1f, 0f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        //isPatrolling = true;
        isChasing = false;

        Vector3 newDirection;
        newDirection = patrolCenter.transform.position - transform.position;
        moveDirection = newDirection;
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

        // wait to chase

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Yang")
        {
            Kill(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log(other.gameObject.tag);

        // if exit patrol range, then go back
        if (other.gameObject.tag == "PatrolPoint")
        {
            Vector3 newDirection;
            newDirection = patrolCenter.transform.position - transform.position;
            Move(newDirection);
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

    public void Chase(GameObject target)
    {
        isChasing = true;
        //isPatrolling = false;

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
