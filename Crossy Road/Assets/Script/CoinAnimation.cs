using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{

    [SerializeField] float animationDuration;
    [SerializeField] Ease ease;

    // Start is called before the first frame update
    void Start()
    {
        transform
            .DOMoveY(1.4f, animationDuration)
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo);

        transform
            .DORotate(new Vector3(0, 180, 90), animationDuration, RotateMode.Fast)
            .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
}
