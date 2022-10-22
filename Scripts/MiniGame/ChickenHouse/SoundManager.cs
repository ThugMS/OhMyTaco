using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        instance = this;
    }
    #endregion

    #region PublicVariable
    public static SoundManager instance = null;
    #endregion

    #region PrivateVariable
    #endregion

    #region PrivateMethod
    #endregion
}
