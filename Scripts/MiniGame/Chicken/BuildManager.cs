using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (m_baseUI.leftButton.isButtonDown)
        {
            isMoving = true;
            m_target.GoLeft();
        }
        else
            isMoving = false;

        if (m_baseUI.rightButton.isButtonDown)
        {
            isMoving = true;
            m_target.GoRight();
        }
        else
            isMoving = false;
    }
    #endregion

    #region PublicVariable
    public static BuildManager instance = null;
    public bool isMoving { get; private set; } = false;
    #endregion

    #region PrivateVariable
    [SerializeField]
    BaseUI m_baseUI;
    [SerializeField]
    TacoController m_target;
    #endregion

    #region PrivateMethod
    #endregion
}
