using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDetector : MonoBehaviour
{
    private GameObject dog;
    private Animator dogAnimator;

    private AudioSource dogAudioSource;
    public AudioClip barkSound;
    public AudioClip growlSound;


    // Start is called before the first frame update
    void Start()
    {
        dog = transform.parent.gameObject;
        dogAnimator = dog.GetComponent<Animator>();
        dogAudioSource = dog.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Dog Detector found " + other.gameObject.name);

        if (other.gameObject.name == "Yin")
        {
            Debug.Log("Dog prepare to escape!");
            dog.GetComponent<DogController>().isEscaping = true;
            dog.GetComponent<DogController>().Escape(other.gameObject);

            if (!dogAudioSource.isPlaying)
            {
                dogAudioSource.PlayOneShot(growlSound, 1);
            }
        }
        else if (other.gameObject.name == "Yang")
        {
            // if not escaping, then good to chase
            if(!dog.GetComponent<DogController>().isEscaping)
            {
                Debug.Log("Dog prepare to chase!");
                dog.GetComponent<DogController>().isChasing = true;
                dog.GetComponent<DogController>().Chase(other.gameObject);
                if (!dogAudioSource.isPlaying)
                {
                    dogAudioSource.PlayOneShot(barkSound, 1);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Yang")
        {
            Debug.Log("Dog not to chase!");
            transform.parent.gameObject.GetComponent<DogController>().isChasing = false;

            dogAnimator.SetBool("isWalk", false);
            dogAnimator.SetBool("isIdle", true);
        }

        // if there is no yin, not escape
        if (other.gameObject.name == "Yin")
        {
            Debug.Log("Dog not escaping!");
            dog.GetComponent<DogController>().isEscaping = false;

            dogAnimator.SetBool("isWalk", false);
            dogAnimator.SetBool("isIdle", true);
        }
    }


}
