using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggObjectPool : MonoBehaviour
{

    #region PublicMethod
    void Awake()
    {
        m_eggQueue = new Queue<GameObject>();
        m_poopQueue = new Queue<GameObject>();
    }

    public GameObject BorrowEgg()
    {
        GameObject egg;

        if (m_eggQueue.Count > 0)
            egg = m_eggQueue.Dequeue();
        else
            egg = Instantiate(m_eggOrigin, this.transform);

        return egg;
    }
    public void ReturnEgg(GameObject _egg)
    {
        _egg.SetActive(false);
        m_eggQueue.Enqueue(_egg);
    }

    public GameObject BorrowPoop()
    {
        GameObject poop;

        if (m_poopQueue.Count > 0)
            poop = m_poopQueue.Dequeue();
        else
            poop = Instantiate(m_poopOrigin, this.transform);

        return poop;
    }
    public void ReturnPoop(GameObject _poop)
    {
        _poop.SetActive(false);
        m_poopQueue.Enqueue(_poop);
    }

    public void ReturnAllTarget()
    {
        foreach (Transform child in transform)
            if (child.gameObject.activeSelf)
            {
                if (child.gameObject.GetComponent<EggController>() != null)
                    ReturnEgg(child.gameObject);
                else
                    ReturnPoop(child.gameObject);
            }
    }

    #endregion

    #region PublicVariable

    #endregion

    #region PrivateVariable
    Queue<GameObject> m_eggQueue;
    Queue<GameObject> m_poopQueue;

    [Header("Origins")]
    [SerializeField] GameObject m_eggOrigin; // 원본
    [SerializeField] GameObject m_poopOrigin; // 원본
    #endregion

    #region PrivateMethod


    #endregion
}
