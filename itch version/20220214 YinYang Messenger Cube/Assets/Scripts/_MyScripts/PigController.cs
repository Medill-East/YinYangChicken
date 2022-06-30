using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public bool isEscaping = false;
    public static Vector3 moveDirection;

    private Animator animator;
    private AudioSource audioSource;

    public AudioClip audioClip;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "KindSpirit")
        {
            isEscaping = true;
            Escape(other.gameObject, 3);

            animator.SetBool("isWalk", true);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip, 1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "KindSpirit")
        {
            isEscaping = false;

            animator.SetBool("isWalk", false);
            animator.SetBool("isIdle", true);
        }
    }

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

    public void Escape(GameObject target, float moveSpeed)
    {
        Vector3 newDirection;
        newDirection = transform.position - target.transform.position;
        // move backword target
        Move(newDirection, moveSpeed, "escape");
    }
}
