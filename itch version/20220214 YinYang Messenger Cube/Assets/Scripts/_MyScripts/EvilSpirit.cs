using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpirit : Utilities
{
    public float moveSpeed;
    public float deathSpeed;

    private GameController gameController;
    private YangController yangController;
    private SpriteRenderer yangSpriteRederer;

    public GameObject deathTarget;
    public Color32 huntedColor;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        yangSpriteRederer = GameObject.FindGameObjectWithTag("YangSprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Yang" && gameController.isYin)
        {
            // change the color of hunted yang
            yangSpriteRederer.color = huntedColor;
            // could not switch back to yang
            gameController.couldSwitch = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Yang")
        {
            yangController = other.gameObject.GetComponent<YangController>();
            yangController.Chase(deathTarget, deathSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Yang")
        {

        }
    }


    public override void Chase(GameObject target, float moveSpeed)
    {
        base.Chase(target, moveSpeed);
    }
}
