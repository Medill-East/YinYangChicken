using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YangController : MonoBehaviour
{

    public float movementSpeed = 3;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;

    public bool controlEnabled;

    private float canJump = 0f;
    Animator anim;
    Rigidbody2D rb;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controlEnabled = true;
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

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        // control rotation
        if (moveHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0f);
        }
        else if (moveHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0f);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            canJump = Time.time + timeBeforeNextJump;
            //anim.SetTrigger("jump");
        }
    }

    public void BecomeYin()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameObject yin = gameController.GetComponent<GameController>().yinInScene;
        GameObject yang = gameObject;

        // stop the movement of this yang player
        controlEnabled = false;

        // reset the position of the yin to a position in front of yang
        Vector3 yangPosition = yang.transform.position;
        yin.transform.position = yangPosition + new Vector3(0, 1, 0);

        // activate the yin player
        yin.gameObject.SetActive(true);

        // recover the movement of the yin player
        yin.GetComponent<YinController>().controlEnabled = true;
    }
}