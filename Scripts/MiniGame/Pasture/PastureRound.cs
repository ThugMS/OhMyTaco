using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PastureRound : MonoBehaviour
{
    #region PublicMethod
    public void SetDefault() // �ʱ�ȭ
    {
        gameObject.SetActive(false);

        foreach(Image image in m_tries)
            image.color = Color.white;
    }
    public void WrongAnswer(int _index)
    {
        m_tries[_index].color = Color.red;
    }
    public void CorrectAnswer(int _index)
    {
        m_tries[_index].color = Color.green;
    }

    #endregion

    #region PublicVariable
    public Image[] m_tries; // ������
    #endregion

    #region Private Variable

    #endregion

    #region PrivateMethod

    #endregion
}
