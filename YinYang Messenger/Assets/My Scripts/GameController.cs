using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject yinInScene;
    public GameObject yangInScene;
    public GameObject yangPrefab;

    public GameObject cmvCam;

    public GameObject deathPanel;
    public GameObject respawnPoints;

    public bool isGameOver;
    public bool isYin;
    public bool isYang;

    private Color yangColor;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;


        yangColor = yangPrefab.GetComponent<MeshRenderer>().sharedMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        // restart
        if (isGameOver && Input.anyKeyDown) Restart();

        // Become yin
        if (Input.GetKeyDown(KeyCode.Y)) BecomeYin();

        // become yang
        if (Input.GetKeyDown(KeyCode.U)) BecomeYang();

        // back to yang
        if (Input.GetKeyDown(KeyCode.I)) BecomeYang();
    }

    public void Death()
    {
        deathPanel.SetActive(true);
        isGameOver = true;
    }

    public void Restart()
    {
        deathPanel.SetActive(false);
        isGameOver = false;
        Vector3 respawnPosition = respawnPoints.GetComponent<RespawnController>().respawnPoints[0].gameObject.transform.position;
        GameObject newYang = Instantiate(yangPrefab, respawnPosition, Quaternion.identity);
        yangInScene = newYang;
        cmvCam.GetComponent<CameraFollow>().ChangeFollow("Yang");
    }

    void BecomeYin()
    {
        // visual effect
        //level.GetComponent<Tilemap>().color = Color.grey;
        // become yin
        yangInScene.GetComponent<YangController>().BecomeYin();
        // change the camera to follow yin
        cmvCam.GetComponent<CameraFollow>().ChangeFollow("Yin");
    }

    void BecomeYang()
    {
        // visual effect
        //level.GetComponent<Tilemap>().color = yangColor;
        // become yang
        yinInScene.GetComponent<YinController>().BecomeYang();
        // change the camera to follow yang
        cmvCam.GetComponent<CameraFollow>().ChangeFollow("Yang");
    }
}
