using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class SerialImagesHandler : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        SetImages();
    }
    #endregion

    #region PublicVariable
    public Image[] images { get { return m_images; } }
    public Vector2 initialPosition { get { return m_initialPosition; } }
    public Vector2 imageInterval { get { return m_imageInterval; } }
    #endregion

    #region PrivateVariable
    [Header("UI Variables")]
    [SerializeField]
    Image[] m_images;
    [SerializeField]
    Vector2 m_initialPosition;
    [SerializeField]
    Vector2 m_imageInterval;
    #endregion

    #region PrivateMethod
    void SetImages()
    {
        if (m_images.Length == 0)
            return;

        m_images[0].rectTransform.position = m_initialPosition;
        for (int i = 1; i < m_images.Length; i++)
            m_images[i].rectTransform.position = m_images[i - 1].rectTransform.position + (Vector3)m_imageInterval;
    }
    #endregion
}
