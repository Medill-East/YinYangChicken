using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YinController : MonoBehaviour
{
    //public float movementSpeed = 3;
    //public float jumpForce = 300;
    //public float timeBeforeNextJump = 1.2f;

    //Rigidbody rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Yin"), LayerMask.NameToLayer("Foreground"), true);
    }

    void Update()
    {
        //if (controlEnabled)
        //{
        //    ControllPlayer();
        //}
    }

    //void ControllPlayer()
    //{
    //    float moveHorizontal = Input.GetAxisRaw("Horizontal");
    //    float moveVertical = Input.GetAxisRaw("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

    //    // control rotation
    //    if (moveHorizontal < 0)
    //    {
    //        transform.rotation = Quaternion.Euler(0, 180f, 0f);
    //    }
    //    else if (moveHorizontal > 0)
    //    {
    //        transform.rotation = Quaternion.Euler(0, 0, 0f);
    //    }


    //    if (movement != Vector3.zero)
    //    {
    //        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    //        //anim.SetInteger("Walk", 1);
    //    }
    //    else
    //    {
    //        //anim.SetInteger("Walk", 0);
    //    }

    //    transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

    //}

    public void BecomeYang()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameObject yin = gameObject;
        GameObject yang = gameController.GetComponent<GameController>().yangInScene;

        // stop the movement of this yin player
        yin.GetComponent<PlayerController>().controlEnabled = false;

        // activate the yang player
        yang.gameObject.SetActive(true);

        // deactive the yin player
        yin.gameObject.SetActive(false);

        // recover the movement of the yang player
        yang.GetComponent<PlayerController>().controlEnabled = true;

    }
}
