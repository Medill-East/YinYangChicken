using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDetector : MonoBehaviour
{
    private GameObject dog;

    // Start is called before the first frame update
    void Start()
    {
        dog = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Dog Detector found " + other.gameObject.name);

        if(other.gameObject.tag == "Yang")
        {
            Debug.Log("Dog prepaer to chase!");
            dog.GetComponent<DogController>().Chase(other.gameObject);
        }
        else if(other.gameObject.tag == "Yin")
        {
            dog.GetComponent<DogController>().Escape(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Yang")
        {
            Debug.Log("Dog not to chase!");
            transform.parent.gameObject.GetComponent<DogController>().isChasing = false;
        }
    }


}