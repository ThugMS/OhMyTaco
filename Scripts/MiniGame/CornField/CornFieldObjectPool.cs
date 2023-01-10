using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CornFieldObjectPool : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        m_moleQueue = new Queue<GameObject>();
        m_sproutMoleQueue = new Queue<GameObject>();
        m_sunglassesQueue = new Queue<GameObject>();
        m_wheatSproutQueue = new Queue<GameObject>();

        m_moleOrigin.SetActive(false);
        m_sproutMoleOrigin.SetActive(false);
        m_sunglassesOrigin.SetActive(false);
        m_wheatSproutOrigin.SetActive(false);

        for (int i = 0; i < 9; i++)
        {
            m_moleQueue.Enqueue(Instantiate(m_moleOrigin, transform));
            m_sproutMoleQueue.Enqueue(Instantiate(m_sproutMoleOrigin, transform));
            m_sunglassesQueue.Enqueue(Instantiate(m_sunglassesOrigin, transform));
            m_wheatSproutQueue.Enqueue(Instantiate(m_wheatSproutOrigin, transform));
        }
    }

    #region 일반 두더지 풀
    public GameObject BorrowMole()
    {
        if (m_moleQueue.Count == 0)
            return null;

        return m_moleQueue.Dequeue();
    }
    public void ReturnMole(GameObject _mole)
    {
        _mole.SetActive(false);
        m_moleQueue.Enqueue(_mole);
    }
    #endregion

    #region 새싹 두더지 풀
    public GameObject BorrowSproutMole()
    {
        if (m_sproutMoleQueue.Count == 0)
            return null;

        return m_sproutMoleQueue.Dequeue();
    }
    public void ReturnSproutMole(GameObject _sproutMole)
    {
        _sproutMole.SetActive(false);
        m_sproutMoleQueue.Enqueue(_sproutMole);
    }
    #endregion

    #region 선글라스 두더지 풀
    public GameObject BorrowSunglassesMole()
    {
        if (m_sunglassesQueue.Count == 0)
            return null;

        return m_sunglassesQueue.Dequeue();
    }
    public void ReturnSunglassesMole(GameObject _sunglassesMole)
    {
        _sunglassesMole.SetActive(false);
        m_sunglassesQueue.Enqueue(_sunglassesMole);
    }
    #endregion

    #region 밀 새싹 풀
    public GameObject BorrowWheatSprout()
    {
        if (m_wheatSproutQueue.Count == 0)
            return null;

        return m_wheatSproutQueue.Dequeue();
    }
    public void ReturnWheatSprout(GameObject _wheatSprout)
    {
        _wheatSprout.SetActive(false);
        m_wheatSproutQueue.Enqueue(_wheatSprout);
    }
    #endregion

    public void ReturnAllTarget()
    {
        foreach (Transform child in transform)
            if (child.gameObject.activeSelf)
                child.GetComponent<CornFieldTarget>().ReturnObject();
    }

    #endregion

    #region PublicVariable

    #endregion

    #region PrivateVariable
    Queue<GameObject> m_moleQueue;
    Queue<GameObject> m_sproutMoleQueue;
    Queue<GameObject> m_sunglassesQueue;
    Queue<GameObject> m_wheatSproutQueue;

    [Header("Origins")]
    [SerializeField] GameObject m_moleOrigin; // 일반 두더지
    [SerializeField] GameObject m_sproutMoleOrigin; // 새싹 두더지
    [SerializeField] GameObject m_sunglassesOrigin; // 선글라스 두더지
    [SerializeField] GameObject m_wheatSproutOrigin; // 밀 새싹
    #endregion

    #region PrivateMethod


    #endregion
}
