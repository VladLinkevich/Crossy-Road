using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float minDuratiun;
    [SerializeField] private float maxDuratiun;

    private float duration;
    private bool firstStart = true;

    void Start()
    {
        gameObject.SetActive(false);
        duration = Random.Range(minDuratiun, maxDuratiun);
    }

    public void OnEnable()
    {
        if (!firstStart)
        {
            transform
                .DOMoveZ(transform.position.z + 50 - (100 * transform.rotation.x), duration, false)
                .OnComplete(() => { gameObject.SetActive(false); });
        } else { firstStart = false; }
    }


}
