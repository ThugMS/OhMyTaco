using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PastureManager : MiniGame
{
    #region PublicMethod

    void Awake()
    {
        instance = this;

        m_answer = new List<int>();
    }

    public override void SettingBeforeStartGame()
    {
        foreach (PastureRound round in m_rounds)
            round.SetDefault();

        m_timerText.text = m_limitTime.ToString();

        base.SettingBeforeStartGame();
    }
    public override void StartMiniGame()
    {
        m_life = 2;
        m_roundIndex = 0;
        m_isChooseTime = false;

        StartCoroutine(RaiseQuestion()); // 문제 출력
    }

    public void SelectCow() // Cow Button click Event
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        if (m_isChooseTime) // 의도치 않은 버튼 클릭 이벤트를 막기 위해 설정
        {
            int cowIndex = clickedButton.GetComponent<PastureCow>().m_cowIndex;


            if (m_answer[m_curChooseIndex] == cowIndex) // 정답
            {
                m_rounds[m_roundIndex].CorrectAnswer(m_curChooseIndex);
            }
            else // 오답
            {
                m_rounds[m_roundIndex].WrongAnswer(m_curChooseIndex);

                m_lifeImages[2 - m_life].color = Color.black;
                m_life--;

                if (m_life == 0)
                {
                    StartCoroutine(GameOverTimer()); // 게임오버
                }
            }

            m_curChooseIndex++;

            if (m_curChooseIndex == m_rounds[m_roundIndex].m_tries.Length) // 모든 선택 완료
            {
                StopCoroutine(m_timerCoroutine);

                Debug.Log("스테이지를 클리어 했습니다");
                m_isStageClear = true;
                m_rounds[m_roundIndex].gameObject.SetActive(false);


                // 스테이지 클리어

                m_roundIndex++;

                if (m_roundIndex == m_rounds.Length) // 모든 라운드가 끝났으면 종료
                {
                    StartCoroutine(GameOverTimer());
                }
                else // 다음 라운드 시작
                {
                    m_isChooseTime = false;
                    StartCoroutine(RaiseQuestion());
                }
            }
        }
    }

    #endregion

    #region PublicVariable
    public static PastureManager instance = null;

    #endregion

    #region Private Variable
    int m_life;

    int m_curChooseIndex; // 현재 선택순서(0 ~)

    bool m_isStageClear;
    bool m_isChooseTime; // 정답 선택 시간인지 판단, 버튼 사용 가능, 불가능 판단

    int m_limitTime = 15; // 일단 15 테스트용

    List<int> m_answer;
    int m_roundIndex = 0;

    Coroutine m_timerCoroutine;

    [SerializeField] Button[] m_cowButtons; // 하단 소 버튼
    [SerializeField] PastureRound[] m_rounds; // 1 ~ 5 라운드
    [SerializeField] Image[] m_lifeImages; // 좌상단 목숨 이미지
    [SerializeField] TextMeshProUGUI m_timerText; // 우상단 타이머
    #endregion

    #region PrivateMethod
    

    protected override void EndMiniGame()
    {
        // 게임 끝내기
        // 결과 출력하기

        Debug.Log("목장 게임 끝~");

        base.EndMiniGame();
    }
    IEnumerator RaiseQuestion()
    {
        m_rounds[m_roundIndex].gameObject.SetActive(true);
        m_answer.Clear();
        m_curChooseIndex = 0;

        yield return new WaitForSeconds(2f); // 스테이지 간 간격을 좀 두자

        m_isStageClear = false;
        float mooInterval = 0.8f;
            
        // 특정 라운드의 신호등 개수
        int tries = m_rounds[m_roundIndex].m_tries.Length;

        for (int i = 0; i < tries; i++)
        {
            int decideMooCow = Random.Range(0, 4); // 울 소를 선택

            m_answer.Add(decideMooCow);

            // cow animation + sound
            m_cowButtons[decideMooCow].GetComponent<PastureCow>().Moo();

            yield return new WaitForSeconds(mooInterval);
        }

        m_timerCoroutine = StartCoroutine(ChooseLimitTimer());
    }

    IEnumerator ChooseLimitTimer() // 선택 시간 제한
    {
        Debug.Log("타이머 시작");

        m_isChooseTime = true;

        for (int i = 0; i <= m_limitTime; i++)
        {
            m_timerText.text = (m_limitTime - i).ToString();

            yield return new WaitForSeconds(1f);
        }

        if (!m_isStageClear) // 시간이 다 지나도록 정답을 입력하지 않음
        {
            StartCoroutine(GameOverTimer()); // 게임 오버
        }
    }

    IEnumerator GameOverTimer()
    {
        StopCoroutine(m_timerCoroutine);
        m_isChooseTime = false;

        yield return new WaitForSeconds(1);

        EndMiniGame();
    }
    #endregion
}
