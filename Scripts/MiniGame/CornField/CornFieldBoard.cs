using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CornFieldBoard : MonoBehaviour
{
    #region PublicMethod
    public void UseBoard()
    {
        isUsing = true;
    }

    public void ReturnBoard()
    {
        isUsing = false;
    }
    #endregion

    #region PublicVariable
    public bool isUsing { get; private set; } = false;
    #endregion

    #region PrivateVariable
    #endregion

    #region PrivateMethod
    #endregion
}
