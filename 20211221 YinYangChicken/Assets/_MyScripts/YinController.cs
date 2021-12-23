using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YinController : MonoBehaviour
{
    public float movementSpeed = 3;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;

    public bool controlEnabled;

    private float canJump = 0f;
    Animator anim;
    Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (controlEnabled)
        {
            ControllPlayer();
        }
    }

    void ControllPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            anim.SetInteger("Walk", 1);
        }
        else
        {
            anim.SetInteger("Walk", 0);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(0, jumpForce, 0);
            canJump = Time.time + timeBeforeNextJump;
            anim.SetTrigger("jump");
        }
    }

    public void BecomeYang()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameObject yin = gameController.GetComponent<GameController>().yin;
        GameObject yang = gameController.GetComponent<GameController>().yang;

        // stop the movement of the yin player
        yin.GetComponent<YinController>().controlEnabled = false;

        // activate the yang player
        yang.gameObject.SetActive(true);

        // deactive the yin player
        yin.gameObject.SetActive(false);

        // recover the movement of the yang player
        yang.GetComponent<YangController>().controlEnabled = true;


    }
}
