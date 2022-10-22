using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Chickens : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        SetImages();
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    [Header("UI Variables")]
    [SerializeField]
    Image[] m_chickens;
    [SerializeField]
    Vector2 m_setPosition;
    [SerializeField]
    Vector3 m_imageInterval;
    #endregion

    #region PrivateMethod
    void SetImages()
    {
        if (m_chickens.Length == 0)
            return;

        m_chickens[0].rectTransform.position = m_setPosition;
        for (int i = 1; i < m_chickens.Length; i++)
            m_chickens[i].rectTransform.position = m_chickens[i - 1].rectTransform.position + m_imageInterval;
    }
    #endregion
}
