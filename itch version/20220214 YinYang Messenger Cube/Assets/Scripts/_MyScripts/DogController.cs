using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public GameObject patrolCenter;

    public float movementSpeed = 3;
    
    public bool isChasing;
    public bool isBacking;
    public bool isEscaping;

    public static Vector3 moveDirection;
    private static Vector3 moveLeft = new Vector3(-1f, 0f, 0f);
    private static Vector3 moveRight = new Vector3(1f, 0f, 0f);

    private Rigidbody rb;
    private MeshRenderer meshRenderer;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        isChasing = false;
        isBacking = false;
        isEscaping = false; 

        Vector3 newDirection;
        newDirection = patrolCenter.transform.position - transform.position;
        moveDirection = newDirection;

        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        // set the sorting layer
        //meshRenderer.sortingLayerName = "Foreground";
        //meshRenderer.sortingOrder = 1;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Dog"), LayerMask.NameToLayer("Foreground"), true);

        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // the priority of escaping is highest
        if(!isEscaping)
        {
            // wait to chase
            if (isBacking && !isChasing)
            {
                GoBack();

                // if back to patrol center, stay, begin to patrol
                if (MyApproximation(gameObject.transform.position.x,
                                    patrolCenter.transform.position.x,
                                    0.1f)
                                   )
                {
                    isBacking = false;
                    //isPatrolling = true;
                    gameObject.transform.position = new Vector3(patrolCenter.transform.position.x,
                                                                gameObject.transform.position.y,
                                                                gameObject.transform.position.z);
                    animator.SetBool("isWalk", false);
                    animator.SetBool("isIdle", true);
                }
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
        if (collision.gameObject.name == "Yang")
        {
            Kill(collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // if exit patrol range, then go back
        if (other.gameObject.tag == "PatrolCenter")
        {
            Debug.Log("Dog prepare to go back!");
            isBacking = true;
        }
    }

    void Move(Vector3 target, string towardOrBackward)
    {
        moveDirection = target;
        Rotate(target, towardOrBackward);
        transform.Translate(moveDirection.normalized * movementSpeed * Time.deltaTime, Space.World);
    }

    void Rotate(Vector3 target, string towardOrBackward)
    {
        if(towardOrBackward == "chase")
        {
            // if target locates at left of the dog, turn left
            if (target.x < 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                // if target locates at right of the dog, turn right
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if(towardOrBackward == "escape")
        {
            if (target.x < 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
        }

    }

    void Patrol()
    {
        Move(moveDirection, "chase");
    }

    public void GoBack()
    {
        Vector3 newDirection;
        newDirection = patrolCenter.transform.position - transform.position;
        Move(newDirection, "chase");

        animator.SetBool("isIdle", false);
        animator.SetBool("isWalk", true);
        //Debug.Log("Dog backing to patrol center!");
    }

    public void Chase(GameObject target)
    {
        Vector3 newDirection;
        newDirection = target.transform.position - transform.position;

        // move toward target
        Move(newDirection, "chase");

        animator.SetBool("isIdle", false);
        animator.SetBool("isWalk", true);
        //Debug.Log("Dog chasing!");
    }

    public void Escape(GameObject target)
    {
        Vector3 newDirection;
        newDirection = transform.position - target.transform.position;
        // move backword target
        Move(newDirection, "escape");

        animator.SetBool("isIdle", false);
        animator.SetBool("isWalk", true);
        //Debug.Log("Dog escaping!");
    }

    public void Kill(GameObject target)
    {
        Destroy(target);
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().Death();

        animator.SetBool("isAttack", true);
        animator.SetBool("isWalk", false);
        StartCoroutine(ResetToIdle());
    }

    IEnumerator ResetToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isIdle", true);

    }
}
