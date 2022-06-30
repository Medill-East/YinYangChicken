using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public bool isChasing;
    public bool isBacking;
    public bool isEscaping;

    public static Vector3 moveDirection;


    public void Move(Vector3 target, float moveSpeed, string towardOrBackward)
    {
        moveDirection = target;
        Rotate(target, towardOrBackward);
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    public void Rotate(Vector3 target, string towardOrBackward)
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

    public virtual void Chase(GameObject target, float moveSpeed)
    {
        Vector3 newDirection;
        newDirection = target.transform.position - transform.position;

        // move toward target
        Move(newDirection, moveSpeed, "chase");
    }

    public virtual void Escape(GameObject target, float moveSpeed)
    {
        Vector3 newDirection;
        newDirection = transform.position - target.transform.position;
        // move backword target
        Move(newDirection, moveSpeed, "escape");
    }

    public virtual void Kill(GameObject target)
    {
        Destroy(target);
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().Death();

    }
}
