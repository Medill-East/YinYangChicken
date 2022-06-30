using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaughtySpirit : Utilities
{
    public float moveSpeed;
    public float originalSpeed;
    public float controlledSpeed;

    private GameController gameController;
    private PlayerController yangPlayerController;
    private SpriteRenderer yangSpriteRederer;

    public Color32 normalColor;
    public Color32 reversedColor;

    public bool couldChange = true;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        yangSpriteRederer = GameObject.FindGameObjectWithTag("YangSprite").GetComponent<SpriteRenderer>();
        yangPlayerController = yangSpriteRederer.transform.parent.gameObject.GetComponent<PlayerController>();
        originalSpeed = yangPlayerController.movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Yang")
        {
            if(couldChange)
            {
                StartCoroutine(ChangeSpeedAndColor());
            }

        }
    }

    IEnumerator ChangeSpeedAndColor()
    {
        // speed control
        if (Mathf.Abs(yangPlayerController.movementSpeed) > controlledSpeed)
        {
            yangPlayerController.movementSpeed *= -0.7f;

        }
        else
        {
            if (yangPlayerController.movementSpeed > 0)
            {
                yangPlayerController.movementSpeed = -controlledSpeed;

            }
            else
            {
                yangPlayerController.movementSpeed = controlledSpeed;
            }
        }

        //color control
        if (yangSpriteRederer.color == Color.white || yangSpriteRederer.color == Color.grey)
        {
            yangSpriteRederer.color = reversedColor;
        }
        else if (yangSpriteRederer.color == reversedColor)
        {
            yangSpriteRederer.color = normalColor;
        }
        else if (yangSpriteRederer.color == normalColor)
        {
            yangSpriteRederer.color = reversedColor;
        }
        couldChange = false;    
        yield return new WaitForSeconds(3);
        couldChange = true;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        
    }




    public override void Chase(GameObject target, float moveSpeed)
    {
        base.Chase(target, moveSpeed);
    }
}
