using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    GameObject yin;
    GameObject yang;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFollow(string whichToFollow)
    {
        CinemachineVirtualCamera virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();

        yin = GameObject.FindWithTag("Yin");
        yang = GameObject.FindWithTag("Yang");

        //Debug.Log("found yin: " + yin.name);
        switch (whichToFollow)
        {
            case "Yin":
                virtualCamera.Follow = yin.transform;
                virtualCamera.LookAt = yin.transform;
                break;
            case "Yang":
                virtualCamera.Follow = yang.transform;
                virtualCamera.LookAt = yang.transform;
                break;
            default:
                break;
        }
    }
}
