using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    #region PublicMethod
    #endregion

    #region PublicVariable
    public Image backgroundImage { get { return m_backgroundImage; } }
    public MoveButtonHandler leftButton { get { return m_leftButton; } }
    public MoveButtonHandler rightButton { get { return m_rightButton; } }
    #endregion

    #region PrivateVariable
    [SerializeField]
    Image m_backgroundImage;
    [SerializeField]
    MoveButtonHandler m_leftButton;
    [SerializeField]
    MoveButtonHandler m_rightButton;
    #endregion

    #region PrivateMethod
    #endregion
}
