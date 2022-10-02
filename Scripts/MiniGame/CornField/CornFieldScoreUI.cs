using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CornFieldScoreUI : MonoBehaviour
{
    #region PublicMethod
    void Start()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        SetText();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ScoreIncrease();   
        }
    }
    #endregion

    #region PublicVariable

    #endregion

    #region Private Variable
    private TextMeshProUGUI m_text;
    private int m_score = 0;
    #endregion

    #region PrivateMethod
    private void SetText()
    {
        m_text.text = "Score : " + m_score.ToString();
    }

    private void ScoreIncrease()
    {
        m_score += 100;
        SetText();
    }
    #endregion
}
