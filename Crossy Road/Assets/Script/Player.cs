using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private TerrainGenerator terrainGenerator = null;
    [SerializeField] private GameObject cameraObject = null;
    [SerializeField] private float duration = 0;
    [SerializeField] private AudioSource moveSound = null;
    [SerializeField] private AudioSource takeCoin = null;
    [SerializeField] private Text coinText = null;
    [SerializeField] private Text scoreText = null;
 

    Vector3 diffence = new Vector3();
    private FollowPlayer camera = null;
    private int coin = 0;
    private int score = 0;
    private bool isHop = false;

    void Start()
    {
        //Считываем сколько у игрока монет
        coin = PlayerPrefs.GetInt("Coin");
        coinText.text = coin.ToString();

        camera = cameraObject.GetComponent<FollowPlayer>();
        gameObject.SetActive(true);
    }


    void Update()
    {

        СheckOutput();

        KeyboardController();

        if (!camera.IsPlay()) { DestroyPlayer(); }
        if (transform.parent != null) { camera.SideMove(); }

    }

    //Перемещение и ротация игрока в пространстве + звук  
    private void MoveCharacter(Vector3 diffence, Vector3 rotate)
    {
        isHop = true;

        transform
           .DOJump((transform.position + diffence), 1f, 1, duration, false);
      
        transform.parent = null;
            transform.DORotate(rotate, duration, RotateMode.Fast);

        moveSound.pitch = Random.Range(0.9f, 1.1f);
        moveSound.Play();

    }


    private void СheckOutput()
    {
        if (gameObject.transform.position.z >= 10f || gameObject.transform.position.z <= -10)
        {
            DestroyPlayer();
        }
    }

    private void KeyboardController()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isHop)
        {


            if (transform.position.x >= 0)
            {
                terrainGenerator.SpawnTerrain();
            }
            float inaccuracy = 0;
            if (transform.position.z % 1 != 0)
            {
                inaccuracy = transform.position.z - Mathf.Round(transform.position.z);
            }
            diffence = new Vector3(1, 0, -inaccuracy);
            MoveCharacter(new Vector3(1, 0, -inaccuracy), new Vector3(0, 0, 0));
            camera.FastUpMove();

            ++score;
            scoreText.text = score.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isHop)
        {
 
            diffence = new Vector3(0, 0, 1);

            MoveCharacter(new Vector3(0, 0, 1), new Vector3(0, -90, 0));
            camera.SideMove();

        }
        else if (Input.GetKeyDown(KeyCode.D) && !isHop)
        {

            diffence = new Vector3(0, 0, -1);

            MoveCharacter(new Vector3(0, 0, -1), new Vector3(0, 90, 0));
            camera.SideMove();

        } else if (Input.GetKeyDown(KeyCode.S) && !isHop)
        {

            diffence = new Vector3(-1, 0, 0);
            camera.FastUpMove();
            MoveCharacter(new Vector3(-1, 0, 0), new Vector3(0, 180, 0));

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isHop = false;
        switch (collision.gameObject.tag)
        {
            case "Train":       DestroyPlayer();                                            break;
            case "Vehicle":     DestroyPlayer();                                            break;
            case "Water":       DestroyPlayer();                                            break;
            case "Log":         transform.parent = collision.collider.transform;            break;
            case "Coin":        TakeCoinFunction(collision.gameObject);                     break;
            case "Tree":        transform.DOJump(GetPosition(), 0.1f, 1, 0.15f, false);     break;
            default:            transform.parent = null;                                    break;
        }

    }

    // получает координаты приземления в случае если мы врезались в статичный объект 
    public Vector3 GetPosition()
    {
        return new Vector3((int)(transform.position.x + 0.5),
                            1f,
                           (int)(transform.position.z + 0.5) - (transform.position.z < 0 ? 1 : 0));
    }
    public void TakeCoinFunction(GameObject gameObject)
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
    public void FinishHop()
    {
        isHop = false;
    }
}
