using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CornFieldTimerBar : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        m_timerBar = GetComponent<Image>();
    }
    public void SetDefaults()
    {
        m_RemainTime = LIMIT_TIME;
        m_timerBar.fillAmount = 1.0f;
    }

    void FixedUpdate()
    {
        if (CornFieldManager.instance.m_isPlaying)
        {
            m_RemainTime -= Time.deltaTime;
            m_timerBar.fillAmount = m_RemainTime / LIMIT_TIME;

            if (m_RemainTime <= 0)
                CornFieldManager.instance.TimeOver();
        }
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    bool m_isGameStart;
    Image m_timerBar;
    float m_RemainTime = 0f;

    const float LIMIT_TIME = 60f;
    #endregion

    #region PrivateMethod
    #endregion
}
