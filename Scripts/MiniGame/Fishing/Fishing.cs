using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fishing : MonoBehaviour
{
    GameManager gameManager;

    Vector2 rightPosition; // ������ ���� �� ��ġ
    Vector2 leftPosition;  // ������ ���� �� ��ġ
    Vector2 endPosition;   // ���� ��ǥ ��ġ

    int chanceIdx; // ���� ��ȸ �ε��� ��ȣ

    float ArrowMoveSpeed = 20f;  // ������ �պ� ���ǵ�, �� 0.5��
    float GageBarLength = 10f; // �������� ���� ����

    private void Start()
    {
        gameManager = Camera.main.GetComponent<GameManager>();

        leftPosition = transform.position;
        rightPosition = leftPosition + Vector2.right * GageBarLength;

        endPosition = rightPosition;

        chanceIdx = 0;
    }

    private void FixedUpdate()
    {
        if ((Vector2)transform.position == leftPosition)
            endPosition = rightPosition;
        else if ((Vector2)transform.position == rightPosition)
            endPosition = leftPosition;

        transform.position = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * ArrowMoveSpeed); // �̵�
    }

    public void PushFishingButton() // ���� ��ư Ŭ��
    {
        float middlePositionX = (leftPosition.x + rightPosition.x) / 2; // �߾� ��ġ x��
        float distanceinterval = Mathf.Abs(middlePositionX - transform.position.x); // ��ġ ����

        Debug.Log(distanceinterval); // ��ġ ���� ����غ�

        gameManager.SetPoint(distanceinterval, chanceIdx++);
    }
}
