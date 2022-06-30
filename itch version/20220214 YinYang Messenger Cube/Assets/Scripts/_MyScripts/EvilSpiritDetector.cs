using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpiritDetector : MonoBehaviour
{
    private EvilSpirit evilSpirit;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        evilSpirit = gameObject.transform.parent.gameObject.GetComponent<EvilSpirit>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Yang" && gameController.isYin)
        {
            evilSpirit.Chase(other.gameObject, evilSpirit.moveSpeed);
            Debug.Log("Hunt Yang");
        }
    }
}
