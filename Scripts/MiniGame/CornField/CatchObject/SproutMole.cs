using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SproutMole : CornFieldTarget
{
    #region PublicMethod

    #endregion

    #region PublicVariable

    #endregion

    #region Private Variable

    #endregion

    #region PrivateMethod
    public override void ReturnObject()
    {
        m_objectPool.ReturnSproutMole(gameObject);
    }
    #endregion
}
