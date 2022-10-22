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
            m_target.GoLeft();

        if (m_baseUI.rightButton.isButtonDown)
            m_target.GoRight();
    }
    #endregion

    #region PublicVariable
    public static BuildManager instance = null;
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
