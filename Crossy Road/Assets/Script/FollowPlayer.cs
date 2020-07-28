using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    // [SerializeField] private Vector3 offset;

    bool play = true;


    public void fastUpMove()
    {

        if (player.gameObject != null)
        {
            if (player.transform.position.x - transform.position.x > 10)
            {
                transform
                        .DOMoveX(player.transform.position.x - 10, duration / 5)
                        .SetEase(ease)
                        .OnComplete(fastUpMove);
            }
            else
            {
                slowUpMove();
            }
        }
    }

    public void sideMove()
    {

        if (player.gameObject != null) {
            transform
                .DOMoveZ(player.transform.position.z - 3, 0.2f)
                .SetEase(ease);
        }
    }

    private void slowUpMove()
    {

        int choiceDuration = (int)(player.transform.position.x - transform.position.x);
        float newDuration = 0;


        switch (choiceDuration)
        {
            case 10: newDuration = duration;                        break;
            case 9:  newDuration = duration;                        break;
            case 8:  newDuration = duration * 3 / 4;                break;
            case 7:  newDuration = duration / 2;                    break;
            default: Debug.Log("Follow Player Error");              break;
        }

        transform
            .DOMoveX(player.transform.position.x - 7, newDuration)
            .SetEase(ease)
            .OnComplete(()=> { checkIsPlay(); });
        
    }

    private void checkIsPlay()
    {
        if (player.transform.position.x - transform.position.x <= 7)
        {
            play = false;
        }
    }

    public bool isPlay()
    {
        return play;
    }
}
