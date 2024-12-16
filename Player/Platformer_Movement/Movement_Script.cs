using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Script : MonoBehaviour
{
    [SerializeField] private float durationToMove;
    [SerializeField] private float durationToStay;
    [SerializeField] private int XValueToAdd;
    [SerializeField] private int YValueToAdd;
    private bool isMoving;
    private bool move;
    private float currentXPos;
    private float currentYPos;

    private void Awake()
    {
        currentXPos = transform.position.x;
        currentYPos = transform.position.y;
    }

    private void Update()
    {
        if (!move)
        {
            move = true;
            ChangePositions();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (isMoving)
            {
                collision.transform.position = new Vector3(transform.position.x, collision.transform.position.y, collision.transform.position.z);
            }
        }
    }

    public void ChangePositions()
    {
        StartCoroutine(changePositions(XValueToAdd, YValueToAdd));
    }

    private IEnumerator changePositions(int x, int y)
    {
        StartCoroutine(AnimateXIncrease(x)); // Start the X animation coroutine
        StartCoroutine(AnimateYIncrease(y)); // Start the Y animation coroutine
        yield return new WaitForSeconds(durationToMove + durationToStay);
        StartCoroutine(AnimateXIncrease(-x)); // Start the X animation coroutine
        StartCoroutine(AnimateYIncrease(-y)); // Start the Y animation coroutine
        yield return new WaitForSeconds(durationToMove + durationToStay);
        move = false;
    }

    private IEnumerator AnimateXIncrease(int amountToAdd)
    {
        float startX = currentXPos;
        float endX = startX + amountToAdd;
        float time = 0;

        while (time < durationToMove)
        {
            isMoving = true;
            time += Time.deltaTime;
            float fraction = time / durationToMove;

            currentXPos = Mathf.Lerp(startX, endX, fraction);
            transform.position = new Vector3(currentXPos, transform.position.y, transform.position.z);

            yield return null;
        }
        isMoving = false;
        transform.position = new Vector3(endX, transform.position.y, transform.position.z);
    }

    private IEnumerator AnimateYIncrease(int amountToAdd)
    {
        float startY = currentYPos;
        float endY = startY + amountToAdd;
        float time = 0;

        while (time < durationToMove)
        {
            isMoving = true;
            time += Time.deltaTime;
            float fraction = time / durationToMove;

            currentYPos = Mathf.Lerp(startY, endY, fraction);
            transform.position = new Vector3(transform.position.x, currentYPos, transform.position.z);

            yield return null;
        }

        isMoving = false;
        transform.position = new Vector3(transform.position.x, endY, transform.position.z);
    }
}
