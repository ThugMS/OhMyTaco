using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EggCatchManager : MiniGame
{
    #region PublicMethod

    void Awake()
    {
        instance = this;
        m_targetTr = m_target.transform;
        m_startPosition = m_target.transform.position;
        //SoundManager.instance.PlayBgm(SoundManager.Bgm.MAIN);
    }
    public override void SettingBeforeStartGame()
    {
        m_target.transform.position = m_startPosition;

        foreach(Image life in m_lifeUI)
        {
            life.color = Color.white;
        }
    }

    public override void StartMiniGame()
    {
        m_remainLife = 3;

        m_isPlaying = true;

        StartCoroutine(LayEggs());
    }

    void Update()
    {
        if(m_isPlaying)
            TacoMove();
    }

    public void LifeDecrease()
    {
        lifeUI[--m_remainLife].color = Color.red; // 임시 이미지

        if (m_remainLife == 0)
            EndMiniGame();
    }
    #endregion

    #region PublicVariable
    public static EggCatchManager instance = null;
    public bool m_isPlaying { get; set; } = false;
    public EggCatchScoreUI scoreUI { get { return m_scoreUI; } }

    public Image[] lifeUI { get { return m_lifeUI; } }

    public EggObjectPool m_objectPool;
    #endregion

    #region PrivateVariable
        
    [SerializeField] MoveButtonHandler m_rightButton;
    [SerializeField] MoveButtonHandler m_leftButton;
    [SerializeField] EggCatchScoreUI m_scoreUI;
    [SerializeField] Image[] m_lifeUI;

    [Header("Object Variables")]
    [SerializeField] TacoController m_target;
    [SerializeField] Transform[] m_chickens;

    Transform m_targetTr = null;
    Vector3 m_startPosition;

    [Header("Coroutine Variables")]
    [SerializeField] float m_layingInterval;

    int m_remainLife;

    const float SCREEN_RIGHT_SIDE = 6;
    const float SCREEN_LEFT_SIDE = -6;
    #endregion

    #region PrivateMethod
   
    protected override void EndMiniGame()
    {
        m_isPlaying = false;

        m_objectPool.ReturnAllTarget();

        Debug.Log("닭장 게임 끝~");
        Debug.Log("최종 점수 : " + m_scoreUI.score);

        base.EndMiniGame();
    }

    void TacoMove()
    {
        if (m_targetTr.position.x < SCREEN_LEFT_SIDE)
            m_targetTr.position = new Vector2(SCREEN_LEFT_SIDE, m_targetTr.position.y);
        else
        {
            if (m_leftButton.isButtonDown)
            {
                m_target.GoLeft();
            }
        }

        if (m_targetTr.position.x > SCREEN_RIGHT_SIDE)
            m_targetTr.position = new Vector2(SCREEN_RIGHT_SIDE, m_targetTr.position.y);
        else
        {
            if (m_rightButton.isButtonDown)
            {
                m_target.GoRight();
            }
        }
    }

    IEnumerator LayEggs()
    {
        WaitForSeconds layingInterval = new WaitForSeconds(m_layingInterval);

        while (m_isPlaying)
        {
            float decideFallingObject = Random.Range(0f, 3.5f);
            GameObject clone;

            //2.5 : 1 = 계란 : 똥
            if (decideFallingObject < 2.5f)
                clone = m_objectPool.BorrowEgg();
            else
                clone = m_objectPool.BorrowPoop();

            clone.transform.position = m_chickens[Random.Range(0, m_chickens.Length)].position;
            clone.SetActive(true);
            yield return layingInterval;
        }
    }
    #endregion
}
