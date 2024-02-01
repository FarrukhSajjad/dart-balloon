using BaloonDart;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    public bool isMoving = false;

    public MoveDirection direction;

    public TargetTag targetTarget;

    public enum TargetTag
    {
        Red,
        Green,
        Blue,
        Orange,
        Pink,
        Purple,
        Yellow
    }

    private void OnMouseDown()
    {
        if (Level.Instance.isLevelEnded) return;
        isMoving = true;
        if (isMoving)
        {
            //MoveTowardsTarget();
            //transform.
            if (direction.positiveXAxis)
            {
                transform.DOLocalMoveX(5, 0.5f);
            }
            else if (direction.negativeXAxis)
            {
                transform.DOLocalMoveX(-5, 0.5f);
            }
            else if (direction.positiveYAxis)
            {
                transform.DOLocalMoveY(5, 0.5f);
            }
            else
            {
                transform.DOLocalMoveY(-5, 0.5f);
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isMoving)
        {
            if (other.gameObject.tag.Contains(targetTarget.ToString()))
            {
                FXManager.Instance.PlayBalloonPopSound();
                FXManager.Instance.OnVibrateEvent();
                //play particle effect
                var particleEffect = Instantiate(LevelManager.Instance.baloonPopParticles, other.gameObject.transform.position, Quaternion.identity);
                Destroy(particleEffect, 2f);

                other.gameObject.SetActive(false);

                Level.Instance.currentBaloonPopped++;

                if(Level.Instance.currentBaloonPopped == Level.Instance.maxNumberOfBalloonsInThisLevel)
                {
                    FXManager.Instance.PlayVictorySound();
                    LevelManager.Instance.OnLevelCompleteEvent();
                    Level.Instance.isLevelEnded = true;
                }
            }
            else
            {
                FXManager.Instance.PlayLevelFailedSound();
                Level.Instance.isLevelEnded = true;
                LevelManager.Instance.OnLevelFailedEvent();
                Debug.Log("LEVEL FAILED");
                FXManager.Instance.OnVibrateEvent();
                isMoving = false;
            }
        }
    }



}

[Serializable]
public class MoveDirection
{
    public bool positiveXAxis;
    public bool negativeXAxis;
    public bool positiveYAxis;
    public bool negativeYAxis;
}
