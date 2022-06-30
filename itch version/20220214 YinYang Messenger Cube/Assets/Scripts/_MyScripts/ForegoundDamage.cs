using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegoundDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Yang")
        {
            Kill(other.gameObject);
        }
    }

    public void Kill(GameObject target)
    {
        Destroy(target);
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().Death();

    }
}
