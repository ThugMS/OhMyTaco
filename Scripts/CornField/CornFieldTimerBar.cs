using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CornFieldTimerBar : MonoBehaviour
{
    #region PublicMethod
    void Start()
    {
        m_LimitTime = 60f;
        m_RemainTime = m_LimitTime;
        TimerBarInitSetting();
    }

    void FixedUpdate()
    {
        m_RemainTime -= Time.deltaTime;
        m_timerBar.fillAmount = m_RemainTime / m_LimitTime;
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    private Image m_timerBar;
    private float m_LimitTime = 0f;
    private float m_RemainTime = 0f;
    #endregion

    #region PrivateMethod
    private void TimerBarInitSetting()
    {
        m_timerBar = GetComponent<Image>();
        m_timerBar.fillAmount = 1.0f;
    }
    #endregion
}
