using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MoleState
{   
    None,
    Hide,
    Idle,
    Catch
}


public class CornFieldMole : MonoBehaviour
{
    #region PublicMethod
    private void Start()
    {
        InitialSetting();   
    }

    private void Update()
    {
        switch (m_state)
        {
            case MoleState.Catch:
                ShowCatchAnimation();
                break;

            case MoleState.None:
                break;

            default:
                break;
        }
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    private MoleState m_state;
    private IEnumerator m_waitCoroutine;
    private IEnumerator m_idleCoroutine;
    private IEnumerator m_catchCoroutine;

    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private Sprite[] m_idleImages;
    [SerializeField] private Sprite[] m_catchImages;
    [SerializeField] private Sprite[] m_hideImages;

    int m_animationCount = 0; //추후에 만약 여러개의 애니메이션이 추가된다면 각각의 애니메이션을 실행시키기 위한 변수
    #endregion

    #region PrivateMethod
    private void InitialSetting()
    {
        ChangeState(MoleState.Hide);
        ShowHideAnimation();
    }

    private void ChangeState(MoleState _state)
    {
        m_state = _state;
        m_animationCount = 0;

        if(m_state == MoleState.Idle)
        {
            StopCoroutine(m_waitCoroutine);
        }
        
        if(m_state == MoleState.Hide)
        {   
            if(m_idleCoroutine != null)
                StopCoroutine(m_idleCoroutine);

            if (m_catchCoroutine != null)
                StopCoroutine(m_catchCoroutine);
        }
    }

    private void ShowIdleAnimation()
    {
        m_sprite.sprite = m_idleImages[m_animationCount];

        m_animationCount++;

        if(m_animationCount >= m_idleImages.Length)
        {
            m_idleCoroutine = Idle();
            StartCoroutine(m_idleCoroutine);
        }
    }

    private void ShowHideAnimation()
    {
        m_sprite.sprite = m_hideImages[m_animationCount];

        m_animationCount++;

        if (m_animationCount >= m_idleImages.Length)
        {
            m_waitCoroutine = Wait();
            StartCoroutine(m_waitCoroutine);
        } 
    }

    private void ShowCatchAnimation()
    {   
        //수정예정
        ScoreUpdate();

        m_sprite.sprite = m_catchImages[m_animationCount];

        m_animationCount++;

        if (m_animationCount >= m_catchImages.Length)
        {
            m_catchCoroutine = Catch();
            StartCoroutine(m_catchCoroutine);
        }
    }

    private IEnumerator Wait()
    {
        m_animationCount = 0;

        float hideTime = Random.Range(0.5f, 10f);
        yield return new WaitForSeconds(hideTime);

        ChangeState(MoleState.Idle);
        ShowIdleAnimation();
    }

    private IEnumerator Idle()
    {
        m_animationCount = 0;

        float idleTime = Random.Range(1.0f, 2.0f);
        yield return new WaitForSeconds(idleTime);

        ChangeState(MoleState.Hide);
        ShowHideAnimation();
    }

    private IEnumerator Catch()
    {
        m_state = MoleState.None;
        m_animationCount = 0;

        float idleTime = 1f;
        yield return new WaitForSeconds(idleTime);

        ChangeState(MoleState.Hide);
        ShowHideAnimation();
    }

    private void OnMouseDown()
    {
        if(m_state == MoleState.Idle)
        {
            ChangeState(MoleState.Catch);
        }
    }
    #endregion
    //수정할 코드
    [SerializeField] CornFieldScoreUI m_scoreIncrease;
    
    private void ScoreUpdate()
    {
        m_scoreIncrease.ScoreIncrease();
    }
    //이 부분까지
}
