using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player = null;
    [SerializeField] private Ease ease = 0;
    [SerializeField] private float duration = 0;
    // [SerializeField] private Vector3 offset;

    bool play = true;

    // Описывает движение камеры если она далеко от игрока.
    // После приближения вызывает SlowUpMove()
    public void FastUpMove()
    {

        if (player.gameObject != null)
        {
            if (player.transform.position.x - transform.position.x > 10)
            {
                transform
                        .DOMoveX(player.transform.position.x - 10, duration / 5)
                        .SetEase(ease)
                        .OnComplete(FastUpMove);
            }
            else
            {
                SlowUpMove();
            }
        }
    }

    // Движение камеры в бок
    public void SideMove()
    {

        if (player.gameObject != null) {
            transform
                .DOMoveZ(player.transform.position.z - 3, 0.2f)
                .SetEase(ease);
        }
    }

    // Движение камеры когда она близко к игроку
    private void SlowUpMove()
    {

        int choiceDuration = (int)(player.transform.position.x - transform.position.x);
        float newDuration = 0;


        switch (choiceDuration)
        {
            case 10: newDuration = duration;                        break;
            case 9:  newDuration = duration;                        break;
            case 8:  newDuration = duration * 3 / 4;                break;
            case 7:  newDuration = duration / 2;                    break;
            default: CheckIsPlay();                                 break;
        }

        transform
            .DOMoveX(player.transform.position.x - 7, newDuration)
            .SetEase(ease)
            .OnComplete(()=> { CheckIsPlay(); });
        
    }

    // Проверка если игрок долго стоит то убивает его
    private void CheckIsPlay()
    {
        if (player.transform.position.x - transform.position.x <= 7)
        {
            play = false;
        }
    }

    public bool IsPlay()
    {
        return play;
    }
}
