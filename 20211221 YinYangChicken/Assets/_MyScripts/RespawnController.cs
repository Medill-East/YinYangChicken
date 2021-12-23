using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public List<Transform> respawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            respawnPoints.Add(child.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
