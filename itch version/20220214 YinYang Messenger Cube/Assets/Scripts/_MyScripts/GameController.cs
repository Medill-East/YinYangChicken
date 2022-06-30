using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public GameObject yinInScene;

    public GameObject yangInScene;
    public GameObject yangPrefab;
    public SpriteRenderer yangSpriteRenderer;
    public Tilemap foregroundTilemap;

    public Tilemap backgroundTilemap;

    public GameObject mainCamera;

    public GameObject deathPanel;
    public GameObject respawnPoints;

    public GameObject lastWords;

    public bool isGameOver;
    public bool isYin;
    public bool isYang;
    public bool couldSwitch = true;

    private Color yangColor;
    private InputManager inputManager;


    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isYang = true;

        inputManager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        yangSpriteRenderer = GameObject.FindGameObjectWithTag("YangSprite").GetComponent<SpriteRenderer>();
        //yangColor = yangPrefab.GetComponent<MeshRenderer>().sharedMaterial.color;
        lastWords.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // restart
        if (isGameOver)
        {
            CheckRestartInput();
        }

        //// Become yin
        //if (Input.GetKeyDown(KeyCode.Y)) BecomeYin();

        //// become yang
        //if (Input.GetKeyDown(KeyCode.U)) BecomeYang();

        //// back to yang
        //if (Input.GetKeyDown(KeyCode.I)) BecomeYang();
        if(couldSwitch)
        {
            CheckSwitchInput();
        }
    }

    /// <summary>
    /// Description:
    /// If the input manager is set up, reads the restart input
    /// Input:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void CheckRestartInput()
    {
        //Debug.Log("check restart input");
        if (inputManager != null)
        {
            if (inputManager.restartButton == 1)
            {
                //Restart();
                //Consume the input
                inputManager.restartButton = 0;
            }
        }
    }


    /// <summary>
    /// Description:
    /// If the input manager is set up, reads the switch input
    /// Input:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void CheckSwitchInput()
    {
        //Debug.Log("check switch input");
        if (inputManager != null)
        {
            if (inputManager.switchButton == 1)
            {
                //Debug.Log("ready to toggle switch");
                ToggleSwitch();
                //Consume the input
                inputManager.switchButton = 0;
            }
        }
    }

    /// <summary>
    /// Description:
    /// If current is yang, switch to yin.
    /// If current is yin, switch to yang.
    /// Input:
    /// none
    /// Retuns:
    /// void (no return)
    /// </summary>
    public void ToggleSwitch()
    {
        if (isYang)
        {
            BecomeYin();
            isYin = true;
            isYang = false;
        }
        else
        {
            BecomeYang();
            isYang = true;
            isYin = false;
        }
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
        //cmvCam.GetComponent<CameraFollow>().ChangeFollow("Yang");
    }

    void BecomeYin()
    {
        if (!isGameOver)
        {
            // visual effect
            foregroundTilemap.color = Color.grey;
            backgroundTilemap.color = Color.grey;
            if (yangSpriteRenderer != null)
            {
                if (yangSpriteRenderer.color == Color.white)
                {
                    yangSpriteRenderer.color = Color.grey;
                }
            }
            // become yin
            yangInScene.GetComponent<YangController>().BecomeYin();
            // change the camera to follow yin
            //mainCamera.GetComponent<CameraController>().target = yinInScene.transform;
        }
    }

    void BecomeYang()
    {
        if (!isGameOver)
        {
            // visual effect
            foregroundTilemap.color = Color.white;
            backgroundTilemap.color = Color.white;

            if (yangSpriteRenderer != null)
            {
                if (yangSpriteRenderer.color == Color.grey)
                {
                    yangSpriteRenderer.color = Color.white;
                }
            }

            // become yang
            yinInScene.GetComponent<YinController>().BecomeYang();
            // change the camera to follow yang
            //mainCamera.GetComponent<CameraController>().target = yangInScene.transform;
        }
    }

    public void ShowLastWords()
    {
        lastWords.SetActive(true);
    }
}
