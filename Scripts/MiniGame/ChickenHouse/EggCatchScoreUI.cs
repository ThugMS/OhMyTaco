using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EggCatchScoreUI : MonoBehaviour
{
    #region PublicMethod
    void OnEnable()
    {
        SetDefaults();
        AddScore(0);
    }

    public void AddScore(int score)
    {
        this.score += score;
        m_scoreText.text = this.score.ToString();
    }
    #endregion

    #region PublicVariable
    public int score { get; set; }
    public Image scoreBackground { get { return m_scoreBackground; } }
    public TextMeshProUGUI scoreText { get { return m_scoreText; } }
    #endregion

    #region PrivateVariable
    [SerializeField] Image m_scoreBackground;
    [SerializeField] TextMeshProUGUI m_scoreText;
    #endregion

    #region PrivateMethod
    void SetDefaults()
    {
        score = 0;
    }
    #endregion
}
