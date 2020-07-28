using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float duration;
    [SerializeField] private AudioSource moveSound;
    [SerializeField] private AudioSource takeCoin;
    [SerializeField] private Text coinText;
    [SerializeField] private Text scoreText;
    //[SerializeField] private Vector3 finalPos;

    private FollowPlayer camera;
    private int coin = 0;
    private int score = 0;
    private bool isHop = false;

    void Start()
    {
        coin = PlayerPrefs.GetInt("Coin");
        coinText.text = coin.ToString();

        camera = cameraObject.GetComponent<FollowPlayer>();
        gameObject.SetActive(true);
    }


    void Update()
    {

        checkOutput();
        KeyboardController();
        if (!camera.isPlay()) { DestroyPlayer(); }
        if (transform.parent != null) { camera.sideMove(); }

    }

    private void moveCharacter(Vector3 diffence, Vector3 rotate)
    {
        //animator.SetTrigger("hop");
        transform.parent = null;
        isHop = true;
        transform.DORotate(rotate, duration, RotateMode.Fast);
        transform
            .DOJump((transform.position + diffence), 1f, 1, duration, false)
            .OnComplete(()=> { isHop = false; });



        moveSound.pitch = Random.Range(0.9f, 1.1f);
        moveSound.Play();



    }

    private void checkOutput()
    {
        if (gameObject.transform.position.z >= 10f || gameObject.transform.position.z <= -10)
        {
            DestroyPlayer();
        }
    }

    private void KeyboardController()
    {
        if (Input.GetKeyDown(KeyCode.W) /*&& !isHop*/)
        {

            //animator.SetTrigger("hop");

            if (transform.position.x >= 0)
            {
                terrainGenerator.SpawnTerrain();
            }
            float inaccuracy = 0;
            if (transform.position.z % 1 != 0)
            {
                inaccuracy = transform.position.z - Mathf.Round(transform.position.z);
            }

            moveCharacter(new Vector3(1, 0, -inaccuracy), new Vector3(0, 0, 0));
            camera.fastUpMove();

            ++score;
            scoreText.text = score.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isHop)
        {
            //animator.SetTrigger("leftHop");
            moveCharacter(new Vector3(0, 0, 1), new Vector3(0, -90, 0));
            camera.sideMove();

        }
        else if (Input.GetKeyDown(KeyCode.D) && !isHop)
        {
            //animator.SetTrigger("rightHop");
            moveCharacter(new Vector3(0, 0, -1), new Vector3(0, 90, 0));
            camera.sideMove();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Vehicle":     DestroyPlayer();                                    break;
            case "Water":       DestroyPlayer();                                    break;
            case "Log":         transform.parent = collision.collider.transform;    break;
            case "Coin":        takeCoinFunction(collision.gameObject);             break;
            default:            transform.parent = null;                            break;
        }

    }

    public void takeCoinFunction(GameObject gameObject)
    {
        ++coin;
        coinText.text = coin.ToString();
        gameObject.SetActive(false);
        takeCoin.Play();
    }

    private void DestroyPlayer()
    {

        if (PlayerPrefs.GetInt("BestScore") < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Coin", coin);
        Application.LoadLevel("Result");
        Destroy(gameObject);

    }
    public void finishHop()
    {
        isHop = false;
    }
}
