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

        StartCoroutine(RaiseQuestion()); // ���� ���
    }

    public void SelectCow() // Cow Button click Event
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        if (m_isChooseTime) // �ǵ�ġ ���� ��ư Ŭ�� �̺�Ʈ�� ���� ���� ����
        {
            int cowIndex = clickedButton.GetComponent<PastureCow>().m_cowIndex;


            if (m_answer[m_curChooseIndex] == cowIndex) // ����
            {
                m_rounds[m_roundIndex].CorrectAnswer(m_curChooseIndex);
            }
            else // ����
            {
                m_rounds[m_roundIndex].WrongAnswer(m_curChooseIndex);

                m_lifeImages[2 - m_life].color = Color.black;
                m_life--;

                if (m_life == 0)
                {
                    StartCoroutine(GameOverTimer()); // ���ӿ���
                }
            }

            m_curChooseIndex++;

            if (m_curChooseIndex == m_rounds[m_roundIndex].m_tries.Length) // ��� ���� �Ϸ�
            {
                StopCoroutine(m_timerCoroutine);

                Debug.Log("���������� Ŭ���� �߽��ϴ�");
                m_isStageClear = true;
                m_rounds[m_roundIndex].gameObject.SetActive(false);


                // �������� Ŭ����

                m_roundIndex++;

                if (m_roundIndex == m_rounds.Length) // ��� ���尡 �������� ����
                {
                    StartCoroutine(GameOverTimer());
                }
                else // ���� ���� ����
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

    int m_curChooseIndex; // ���� ���ü���(0 ~)

    bool m_isStageClear;
    bool m_isChooseTime; // ���� ���� �ð����� �Ǵ�, ��ư ��� ����, �Ұ��� �Ǵ�

    int m_limitTime = 15; // �ϴ� 15 �׽�Ʈ��

    List<int> m_answer;
    int m_roundIndex = 0;

    Coroutine m_timerCoroutine;

    [SerializeField] Button[] m_cowButtons; // �ϴ� �� ��ư
    [SerializeField] PastureRound[] m_rounds; // 1 ~ 5 ����
    [SerializeField] Image[] m_lifeImages; // �»�� ��� �̹���
    [SerializeField] TextMeshProUGUI m_timerText; // ���� Ÿ�̸�
    #endregion

    #region PrivateMethod
    

    protected override void EndMiniGame()
    {
        // ���� ������
        // ��� ����ϱ�

        Debug.Log("���� ���� ��~");

        base.EndMiniGame();
    }
    IEnumerator RaiseQuestion()
    {
        m_rounds[m_roundIndex].gameObject.SetActive(true);
        m_answer.Clear();
        m_curChooseIndex = 0;

        yield return new WaitForSeconds(2f); // �������� �� ������ �� ����

        m_isStageClear = false;
        float mooInterval = 0.8f;
            
        // Ư�� ������ ��ȣ�� ����
        int tries = m_rounds[m_roundIndex].m_tries.Length;

        for (int i = 0; i < tries; i++)
        {
            int decideMooCow = Random.Range(0, 4); // �� �Ҹ� ����

            m_answer.Add(decideMooCow);

            // cow animation + sound
            m_cowButtons[decideMooCow].GetComponent<PastureCow>().Moo();

            yield return new WaitForSeconds(mooInterval);
        }

        m_timerCoroutine = StartCoroutine(ChooseLimitTimer());
    }

    IEnumerator ChooseLimitTimer() // ���� �ð� ����
    {
        Debug.Log("Ÿ�̸� ����");

        m_isChooseTime = true;

        for (int i = 0; i <= m_limitTime; i++)
        {
            m_timerText.text = (m_limitTime - i).ToString();

            yield return new WaitForSeconds(1f);
        }

        if (!m_isStageClear) // �ð��� �� �������� ������ �Է����� ����
        {
            StartCoroutine(GameOverTimer()); // ���� ����
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
