using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingManager : MiniGame
{
    #region PublicMethod
    public override void SettingBeforeStartGame()
    {
        m_gageArrow.transform.position = m_leftPoint.position;

        for (int i = 0; i < MAX_TRY_COUNT; i++)
        {
            m_scoreTextMesh[i].text = "-";
        }

        base.SettingBeforeStartGame();
    }
    public override void StartMiniGame()
    {
        m_desPosition = m_rightPoint.position;

        m_chanceIdx = 0;
        m_totalScore = 0;

        m_isPlaying = true;
    }
    void Update()
    {
        if (m_isPlaying)
        {
            // 화살 지속적 이동
            if (m_gageArrow.transform.position == m_leftPoint.position)
                m_desPosition = m_rightPoint.position;
            else if (m_gageArrow.transform.position == m_rightPoint.position)
                m_desPosition = m_leftPoint.position;

            m_gageArrow.transform.position = Vector2.MoveTowards(m_gageArrow.transform.position, m_desPosition, Time.deltaTime * ARROW_MOVE_SPEED); // 이동
        }
    }

    public void PushFishingButton() // 낚기 버튼 클릭
    {
        if (m_isPlaying)
        {
            float middlePositionX = (m_leftPoint.position.x + m_rightPoint.position.x) / 2; // 중앙 위치 x값
            float distanceinterval = Mathf.Abs(middlePositionX - m_gageArrow.transform.position.x); // 위치 차이

            // 점수 계산 및 반환
            m_scoreTextMesh[m_chanceIdx++].text = PointCalc(distanceinterval).ToString();

            if (m_chanceIdx == MAX_TRY_COUNT)
            {
                EndMiniGame();
            }
        }
    }

    public int PointCalc(float _distanceinterval) // 버튼 클릭시 에임 위치에 따른 점수 산출
    {
        int score;

        if (_distanceinterval < 0.21f)
            score = 7;
        else if (_distanceinterval < 0.61f)
            score = 6;
        else if (_distanceinterval < 1.41f)
            score = 5;
        else if (_distanceinterval < 2.21f)
            score = 4;
        else if (_distanceinterval < 3.41f)
            score = 3;
        else if (_distanceinterval < 3.81f)
            score = 2;
        else
            score = 1;

        m_totalScore += score;

        return score;
    }


    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    Vector2 m_desPosition;   // 막대 목표 위치

    bool m_isPlaying = false;

    int m_chanceIdx; // 도전 기회 인덱스 번호
    int m_totalScore;

    [SerializeField] TextMeshProUGUI[] m_scoreTextMesh;
    [SerializeField] GameObject m_gageArrow; // 움직이는 막대기
    [SerializeField] GameObject m_gageBar; // 게이지 바
    [SerializeField] Transform m_rightPoint; // 오른쪽 끝
    [SerializeField] Transform m_leftPoint; // 왼쪽 끝

    const int MAX_TRY_COUNT = 3;
    const float ARROW_MOVE_SPEED = 20f;  // 막대 왕복 스피드, 편도 0.5초
    #endregion

    #region PrivateMethod
    
    protected override void EndMiniGame()
    {
        m_isPlaying = false;

        Debug.Log("낚시 끝");
        Debug.Log("최종 점수 : " + m_totalScore);

        base.EndMiniGame();
    }

    #endregion
}