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
            // ȭ�� ������ �̵�
            if (m_gageArrow.transform.position == m_leftPoint.position)
                m_desPosition = m_rightPoint.position;
            else if (m_gageArrow.transform.position == m_rightPoint.position)
                m_desPosition = m_leftPoint.position;

            m_gageArrow.transform.position = Vector2.MoveTowards(m_gageArrow.transform.position, m_desPosition, Time.deltaTime * ARROW_MOVE_SPEED); // �̵�
        }
    }

    public void PushFishingButton() // ���� ��ư Ŭ��
    {
        if (m_isPlaying)
        {
            float middlePositionX = (m_leftPoint.position.x + m_rightPoint.position.x) / 2; // �߾� ��ġ x��
            float distanceinterval = Mathf.Abs(middlePositionX - m_gageArrow.transform.position.x); // ��ġ ����

            // ���� ��� �� ��ȯ
            m_scoreTextMesh[m_chanceIdx++].text = PointCalc(distanceinterval).ToString();

            if (m_chanceIdx == MAX_TRY_COUNT)
            {
                EndMiniGame();
            }
        }
    }

    public int PointCalc(float _distanceinterval) // ��ư Ŭ���� ���� ��ġ�� ���� ���� ����
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
    Vector2 m_desPosition;   // ���� ��ǥ ��ġ

    bool m_isPlaying = false;

    int m_chanceIdx; // ���� ��ȸ �ε��� ��ȣ
    int m_totalScore;

    [SerializeField] TextMeshProUGUI[] m_scoreTextMesh;
    [SerializeField] GameObject m_gageArrow; // �����̴� �����
    [SerializeField] GameObject m_gageBar; // ������ ��
    [SerializeField] Transform m_rightPoint; // ������ ��
    [SerializeField] Transform m_leftPoint; // ���� ��

    const int MAX_TRY_COUNT = 3;
    const float ARROW_MOVE_SPEED = 20f;  // ���� �պ� ���ǵ�, �� 0.5��
    #endregion

    #region PrivateMethod
    
    protected override void EndMiniGame()
    {
        m_isPlaying = false;

        Debug.Log("���� ��");
        Debug.Log("���� ���� : " + m_totalScore);

        base.EndMiniGame();
    }

    #endregion
}