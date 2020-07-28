using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveObject : MonoBehaviour
{

    [SerializeField] private GameObject coinTextObject;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
 

    private Text coinText;
    void Start()
    {
        coinText = coinTextObject.GetComponent<Text>();
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();

        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore");
    }
        


}
