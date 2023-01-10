using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CowState
{
    Idle,
    Moo
}
public class PastureCow : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        m_cowImage = GetComponent<Image>();
    }
    public void Moo()
    {
        // 소리 재생도 필요

        StartCoroutine(Mooing());
    }
    #endregion

    #region PublicVariable

    public int m_cowIndex;

    #endregion

    #region Private Variable
    const float MOOINTERVAL = 0.5f; // 이미지 변경 간격

    Image m_cowImage;
    [SerializeField] Sprite[] m_cowSprite;
    #endregion

    #region PrivateMethod
    IEnumerator Mooing()
    {
        m_cowImage.sprite = m_cowSprite[(int)CowState.Moo];

        yield return new WaitForSeconds(MOOINTERVAL);

        m_cowImage.sprite = m_cowSprite[(int)CowState.Idle];
    }
    #endregion
}
