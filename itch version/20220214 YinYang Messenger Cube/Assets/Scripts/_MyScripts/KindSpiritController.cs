using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KindSpiritController : MonoBehaviour
{
    public bool isChasing = false;

    private GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Yin")
        {
            isChasing = true;
            Chase(other.gameObject, 3);
            //Debug.Log("Follow Yin");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Yin")
        {
            isChasing = false;
        }
    }

    public static Vector3 moveDirection;


    void Move(Vector3 target, float moveSpeed, string towardOrBackward)
    {
        moveDirection = target;
        Rotate(target, towardOrBackward);
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    void Rotate(Vector3 target, string towardOrBackward)
    {
        if (towardOrBackward == "chase")
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
        if (towardOrBackward == "escape")
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

    public void Chase(GameObject target, float moveSpeed)
    {
        Vector3 newDirection;
        newDirection = target.transform.position - transform.position;

        // move toward target
        Move(newDirection, moveSpeed, "chase");
    }
}
