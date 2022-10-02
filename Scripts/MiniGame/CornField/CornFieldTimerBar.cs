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
        TimerBarInitSetting();
    }

    void FixedUpdate()
    {
        m_timerBar.value -= Time.fixedDeltaTime;
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    private Slider m_timerBar;
    private float m_LimitTime = 0f;
    #endregion

    #region PrivateMethod
    private void TimerBarInitSetting()
    {
        m_timerBar = GetComponent<Slider>();
        m_timerBar.maxValue = m_LimitTime;
        m_timerBar.minValue = 0.0f;
        m_timerBar.value = m_LimitTime;
    }
    #endregion
}
