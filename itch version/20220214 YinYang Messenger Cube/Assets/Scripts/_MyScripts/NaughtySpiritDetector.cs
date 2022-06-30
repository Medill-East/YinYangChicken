using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaughtySpiritDetector : MonoBehaviour
{
    private NaughtySpirit naughtySpirit;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        naughtySpirit = gameObject.transform.parent.gameObject.GetComponent<NaughtySpirit>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Yang")
        {
            naughtySpirit.Chase(other.gameObject, naughtySpirit.moveSpeed);
            //Debug.Log("Tease Yang");
        }
    }
}
