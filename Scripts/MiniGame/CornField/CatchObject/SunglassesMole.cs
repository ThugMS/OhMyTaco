using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunglassesMole : CornFieldTarget
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
        m_objectPool.ReturnSunglassesMole(gameObject);
    }

    protected override void Caught()
    {
        StartCoroutine(CornFieldManager.instance.FeverTimer());
        base.Caught();
    }
    #endregion
}
