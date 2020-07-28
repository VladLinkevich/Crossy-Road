using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObject : MonoBehaviour
{

    private float duration;
    private bool firstStart = true;

    void Start()
    {
        gameObject.SetActive(false);
      
    }

    public void OnEnable()
    {
        if (!firstStart)
        {
            transform
                .DOMoveZ(transform.position.z + 40 - (80 * transform.rotation.y), duration, false)
                .OnComplete(() => { gameObject.SetActive(false); });
        } else { firstStart = false; }
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

}
