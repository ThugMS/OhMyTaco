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
    public int m_score; // �� ��ü������ ���� ����
    public float m_roundTripTime; // �� ��ü������ ���� �պ� �ð�
    #endregion

    #region PrivateVariable
    MoleState m_state;
    CornFieldBoard m_board;

    Coroutine m_movingCoroution;
    Coroutine m_catchingCoroution;

    protected CornFieldObjectPool m_objectPool;

    /*
     * �̹����� ����
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private Sprite[] m_idleImages;
    [SerializeField] private Sprite[] m_catchImages;
    */

    //int m_animationCount = 0; //���Ŀ� ���� �������� �ִϸ��̼��� �߰��ȴٸ� ������ �ִϸ��̼��� �����Ű�� ���� ����
    #endregion

    #region PrivateMethod
    

    protected virtual void Caught() // �� ��ü�� �������� ȿ�� �� �۾�
    {
        m_state = MoleState.Catch;
        CornFieldManager.instance.AddScore(m_score);
        StopCoroutine(m_movingCoroution);
        m_catchingCoroution = StartCoroutine(Catching());
    }

    IEnumerator Moving()
    {
        // �� �ð���ŭ ������ �ִϸ��̼� ���
        // �� �ð���ŭ ���� �ִϸ��̼� ���
        yield return new WaitForSeconds(m_roundTripTime);

        // ���� �������� �ش� ���� ������� �ٲٰ� ������Ʈ Ǯ�� �ڽ� ��ȯ
        m_board.ReturnBoard();
        ReturnObject();
    }
    IEnumerator Catching()
    {
        // ������ �ִϸ��̼� �ð� ��ŭ �ִϸ��̼� ���
        yield return null;

        // �� ������ ���� �ݳ� �� Ǯ�� ��ȯ
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
