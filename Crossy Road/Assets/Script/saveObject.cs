using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveObject : MonoBehaviour
{

    [SerializeField] private GameObject coinTextObject;
    // Start is called before the first frame update
    private Text coinText;
    void Start()
    {
        coinText = coinTextObject.GetComponent<Text>();
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }


}
