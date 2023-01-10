using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CornFieldScoreUI : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }

    public void SetDefaults()
    {
        m_score = 0;
        ScoreIncrease(0);
    }

    public void ScoreIncrease(int _score)
    {
        m_score += _score;
        SetText();
    }
    #endregion

    #region PublicVariable

    #endregion

    #region Private Variable
    TextMeshProUGUI m_text;
    int m_score = 0;
    #endregion
    
    #region PrivateMethod
    private void SetText()
    {
        m_text.text = "Score : " + m_score.ToString();
    }
    #endregion
}
