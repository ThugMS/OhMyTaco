using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fishing : MonoBehaviour
{
    #region PublicMethod
    void Start()
    {
        gameManager = Camera.main.GetComponent<GameManager>();

        leftPosition = transform.position;
        rightPosition = leftPosition + Vector2.right * GageBarLength;

        desPosition = rightPosition;

        chanceIdx = 0;
    }

    void FixedUpdate() // Update�� ����� ���ɼ� ����
    {
        if ((Vector2)transform.position == leftPosition)
            desPosition = rightPosition;
        else if ((Vector2)transform.position == rightPosition)
            desPosition = leftPosition;

        transform.position = Vector2.MoveTowards(transform.position, desPosition, Time.deltaTime * ArrowMoveSpeed); // �̵�
    }

    public void PushFishingButton() // ���� ��ư Ŭ��
    {
        float middlePositionX = (leftPosition.x + rightPosition.x) / 2; // �߾� ��ġ x��
        float distanceinterval = Mathf.Abs(middlePositionX - transform.position.x); // ��ġ ����

        Debug.Log(distanceinterval); // ��ġ ���� ����غ�

        gameManager.SetPoint(distanceinterval, chanceIdx++);
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    GameManager gameManager;

    private Vector2 rightPosition; // ������ ���� �� ��ġ
    private Vector2 leftPosition;  // ������ ���� �� ��ġ
    private Vector2 desPosition;   // ���� ��ǥ ��ġ

    private int chanceIdx; // ���� ��ȸ �ε��� ��ȣ

    private float ArrowMoveSpeed = 20f;  // ���� �պ� ���ǵ�, �� 0.5��
    private float GageBarLength = 10f;   // �������� ���� ����
    #endregion

    #region PrivateMethod
    #endregion
}
