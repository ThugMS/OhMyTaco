using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CornFieldManager : MiniGame
{
    #region PublicMethod
    void Awake()
    {
        instance = this;
    }

    public override void SettingBeforeStartGame()
    {
        m_cornFieldTimerBar.SetDefaults();
        m_cornFieldScoreUI.SetDefaults();

        base.SettingBeforeStartGame();
    }
    public override void StartMiniGame()
    {
        m_summonSunglassesMoleTimer = Random.Range(10, 30);
        m_summonTwiceSunglassesMoleTimer = Random.Range(30, 50) - m_summonSunglassesMoleTimer;

        m_score = 0;
        m_isCaughtSunglasses = false;
        m_summonSunglasses = false;

        m_isPlaying = true;

        StartCoroutine(MoleSummon());
        StartCoroutine(SunglassesMoleSummon());
    }

    public void AddScore(int _score)
    {
        m_score += _score;
        m_cornFieldScoreUI.ScoreIncrease(_score);
    }

    public IEnumerator FeverTimer()
    {
        Debug.Log("�ǹ�����");
        m_isCaughtSunglasses = true;
        m_feverSummonCount = 1;

        yield return new WaitForSeconds(10f);

        m_feverSummonCount = 0;

        Debug.Log("�ǹ�����");
    }
    public void TimeOver()
    {
        EndMiniGame();
    }

    #endregion

    #region PublicVariable
    public static CornFieldManager instance = null;

    public CornFieldObjectPool m_objectPool;

    public bool m_isPlaying = false;
    #endregion

    #region PrivateVariable
    bool m_summonSunglasses; // true = ���۶� ��ȯ�� Ÿ�̹�, false = ��ȯ�ϸ� �ȵǴ� Ÿ�̹�
    bool m_isCaughtSunglasses;

    int m_feverSummonCount = 0; // 0�̸� ���, �ǹ��� 1�� ����, for ������ ���ÿ� 2���� ��ȯ
    int m_score;

    float m_summonSunglassesMoleTimer;
    float m_summonTwiceSunglassesMoleTimer; // ù��° ���۶� ��� ���н� ���� ���۶� �δ��� ��ȯ �ð�
    const float FEVER_TIME = 10f;

    [SerializeField] CornFieldBoard[] m_cornFieldBoards;
    [SerializeField] CornFieldScoreUI m_cornFieldScoreUI;
    [SerializeField] CornFieldTimerBar m_cornFieldTimerBar;
    #endregion

    #region PrivateMethod
    

    protected override void EndMiniGame()
    {
        m_isPlaying = false;
        m_objectPool.ReturnAllTarget();

        Debug.Log("�δ��� ���� ��~");
        Debug.Log("���� ���� : " + m_score);

        base.EndMiniGame();
    }
    CornFieldBoard ChooseBoard() // ����ִ� ������ ��ȯ
    {
        List<int> boardIndexes = new List<int>();

        for (int idx = 0; idx < m_cornFieldBoards.Length; idx++)
            if (!m_cornFieldBoards[idx].isUsing) // ��������� ���� ����
                boardIndexes.Add(idx);

        if (boardIndexes.Count == 0) // ����� �� �ִ� ������ ���� ���
            return null;

        int decideBoardIndex = boardIndexes[Random.Range(0, boardIndexes.Count)];
        m_cornFieldBoards[decideBoardIndex].UseBoard();

        return m_cornFieldBoards[decideBoardIndex];
    }

    IEnumerator MoleSummon() // ���۶� �δ����� ������ �δ��� ���� �� ��ȯ
    {
        float summonInterval = 1.8f; // ��ü�� ���� ����

        while (m_isPlaying)
        {
            yield return new WaitForSeconds(summonInterval);

            summonInterval -= 0.03f;
            if (summonInterval < 0.7f)
                summonInterval = 0.7f;

            // �δ��� ��ȯ
            for (int cnt = 0; cnt < 1 + m_feverSummonCount; cnt++)
            {
                GameObject summonedObject;
                CornFieldBoard choosedBoard = ChooseBoard();

                if (m_summonSunglasses)
                {
                    m_summonSunglasses = false;
                    summonedObject = m_objectPool.BorrowSunglassesMole();
                }
                else
                {
                    int decideSummonObject = Random.Range(0, 100); // ���� ��ü ����(Ȯ��)

                    if (decideSummonObject < 65)
                        summonedObject = m_objectPool.BorrowMole();
                    else if (decideSummonObject < 85)
                        summonedObject = m_objectPool.BorrowSproutMole();
                    else
                        summonedObject = m_objectPool.BorrowWheatSprout();
                }

                summonedObject.transform.position = choosedBoard.transform.position;
                summonedObject.SetActive(true);
                summonedObject.GetComponent<CornFieldTarget>().Summon(choosedBoard);
            }
        }
    }
    IEnumerator SunglassesMoleSummon() // ���۶� �δ��� ����
    {
        yield return new WaitForSeconds(m_summonSunglassesMoleTimer);

        m_summonSunglasses = true;

        yield return new WaitForSeconds(m_summonTwiceSunglassesMoleTimer);

        if (!m_isCaughtSunglasses) // ù��° �õ����� ���۶� �δ��㸦 ���� ���� ���
            m_summonSunglasses = true;
    }
    
    #endregion
}
