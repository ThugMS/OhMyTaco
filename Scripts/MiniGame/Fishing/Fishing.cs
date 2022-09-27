using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fishing : MonoBehaviour
{
    GameManager gameManager;

    Vector2 rightPosition; // 게이지 우측 끝 위치
    Vector2 leftPosition;  // 게이지 좌측 끝 위치
    Vector2 endPosition;   // 막대 목표 위치

    int chanceIdx; // 도전 기회 인덱스 번호

    float ArrowMoveSpeed = 20f;  // 게이지 왕복 스피드, 편도 0.5초
    float GageBarLength = 10f; // 게이지의 가로 길이

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

        transform.position = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * ArrowMoveSpeed); // 이동
    }

    public void PushFishingButton() // 낚기 버튼 클릭
    {
        float middlePositionX = (leftPosition.x + rightPosition.x) / 2; // 중앙 위치 x값
        float distanceinterval = Mathf.Abs(middlePositionX - transform.position.x); // 위치 차이

        Debug.Log(distanceinterval); // 위치 차를 출력해봄

        gameManager.SetPoint(distanceinterval, chanceIdx++);
    }
}
