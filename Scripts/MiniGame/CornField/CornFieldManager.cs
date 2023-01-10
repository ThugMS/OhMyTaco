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
        Debug.Log("피버시작");
        m_isCaughtSunglasses = true;
        m_feverSummonCount = 1;

        yield return new WaitForSeconds(10f);

        m_feverSummonCount = 0;

        Debug.Log("피버종료");
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
    bool m_summonSunglasses; // true = 선글라스 소환할 타이밍, false = 소환하면 안되는 타이밍
    bool m_isCaughtSunglasses;

    int m_feverSummonCount = 0; // 0이면 평소, 피버땐 1로 변경, for 문에서 동시에 2개씩 소환
    int m_score;

    float m_summonSunglassesMoleTimer;
    float m_summonTwiceSunglassesMoleTimer; // 첫번째 선글라스 잡기 실패시 사용될 선글라스 두더쥐 소환 시간
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

        Debug.Log("두더지 게임 끝~");
        Debug.Log("최종 점수 : " + m_score);

        base.EndMiniGame();
    }
    CornFieldBoard ChooseBoard() // 비어있는 발판을 반환
    {
        List<int> boardIndexes = new List<int>();

        for (int idx = 0; idx < m_cornFieldBoards.Length; idx++)
            if (!m_cornFieldBoards[idx].isUsing) // 사용중이지 않은 발판
                boardIndexes.Add(idx);

        if (boardIndexes.Count == 0) // 사용할 수 있는 발판이 없을 경우
            return null;

        int decideBoardIndex = boardIndexes[Random.Range(0, boardIndexes.Count)];
        m_cornFieldBoards[decideBoardIndex].UseBoard();

        return m_cornFieldBoards[decideBoardIndex];
    }

    IEnumerator MoleSummon() // 선글라스 두더지를 제외한 두더지 선택 및 소환
    {
        float summonInterval = 1.8f; // 객체들 생성 간격

        while (m_isPlaying)
        {
            yield return new WaitForSeconds(summonInterval);

            summonInterval -= 0.03f;
            if (summonInterval < 0.7f)
                summonInterval = 0.7f;

            // 두더지 소환
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
                    int decideSummonObject = Random.Range(0, 100); // 출현 객체 결정(확률)

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
    IEnumerator SunglassesMoleSummon() // 선글라스 두더지 선택
    {
        yield return new WaitForSeconds(m_summonSunglassesMoleTimer);

        m_summonSunglasses = true;

        yield return new WaitForSeconds(m_summonTwiceSunglassesMoleTimer);

        if (!m_isCaughtSunglasses) // 첫번째 시도에서 선글라스 두더쥐를 잡지 못한 경우
            m_summonSunglasses = true;
    }
    
    #endregion
}
