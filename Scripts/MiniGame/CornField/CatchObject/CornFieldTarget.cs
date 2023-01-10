using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MoleState
{
    Idle,
    Catch
}

public class CornFieldTarget : MonoBehaviour
{
    #region PublicMethod

    public void Summon(CornFieldBoard _board)
    {
        m_objectPool = CornFieldManager.instance.m_objectPool;
        m_board = _board;
        m_state = MoleState.Idle;
        m_movingCoroution = StartCoroutine(Moving());
    }
    public virtual void ReturnObject()
    {
        //return to pool
    }
    #endregion

    #region PublicVariable
    public int m_score; // 각 객체마다의 고유 점수
    public float m_roundTripTime; // 각 객체마다의 고유 왕복 시간
    #endregion

    #region PrivateVariable
    MoleState m_state;
    CornFieldBoard m_board;

    Coroutine m_movingCoroution;
    Coroutine m_catchingCoroution;

    protected CornFieldObjectPool m_objectPool;

    /*
     * 이미지용 보류
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private Sprite[] m_idleImages;
    [SerializeField] private Sprite[] m_catchImages;
    */

    //int m_animationCount = 0; //추후에 만약 여러개의 애니메이션이 추가된다면 각각의 애니메이션을 실행시키기 위한 변수
    #endregion

    #region PrivateMethod
    

    protected virtual void Caught() // 각 객체당 잡혔을때 효과 및 작업
    {
        m_state = MoleState.Catch;
        CornFieldManager.instance.AddScore(m_score);
        StopCoroutine(m_movingCoroution);
        m_catchingCoroution = StartCoroutine(Catching());
    }

    IEnumerator Moving()
    {
        // 편도 시간만큼 나오는 애니메이션 출력
        // 편도 시간만큼 들어가는 애니메이션 출력
        yield return new WaitForSeconds(m_roundTripTime);

        // 들어갔다 나왔으면 해당 발판 사용으로 바꾸고 오브젝트 풀에 자신 반환
        m_board.ReturnBoard();
        ReturnObject();
    }
    IEnumerator Catching()
    {
        // 잡히는 애니메이션 시간 만큼 애니메이션 출력
        yield return null;

        // 다 끝나면 발판 반납 및 풀에 반환
        m_board.ReturnBoard(); 
        ReturnObject();
    }

    void OnMouseDown()
    {
        if (m_state == MoleState.Idle)
            Caught();
    }
    #endregion
}
