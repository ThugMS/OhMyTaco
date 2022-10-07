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

    void FixedUpdate() // Update로 변경될 가능성 있음
    {
        if ((Vector2)transform.position == leftPosition)
            desPosition = rightPosition;
        else if ((Vector2)transform.position == rightPosition)
            desPosition = leftPosition;

        transform.position = Vector2.MoveTowards(transform.position, desPosition, Time.deltaTime * ArrowMoveSpeed); // 이동
    }

    public void PushFishingButton() // 낚기 버튼 클릭
    {
        float middlePositionX = (leftPosition.x + rightPosition.x) / 2; // 중앙 위치 x값
        float distanceinterval = Mathf.Abs(middlePositionX - transform.position.x); // 위치 차이

        Debug.Log(distanceinterval); // 위치 차를 출력해봄

        gameManager.SetPoint(distanceinterval, chanceIdx++);
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    GameManager gameManager;

    private Vector2 rightPosition; // 게이지 우측 끝 위치
    private Vector2 leftPosition;  // 게이지 좌측 끝 위치
    private Vector2 desPosition;   // 막대 목표 위치

    private int chanceIdx; // 도전 기회 인덱스 번호

    private float ArrowMoveSpeed = 20f;  // 막대 왕복 스피드, 편도 0.5초
    private float GageBarLength = 10f;   // 게이지의 가로 길이
    #endregion

    #region PrivateMethod
    #endregion
}
