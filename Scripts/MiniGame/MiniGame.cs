using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniGame : MonoBehaviour
{
    #region PublicMethod
    public virtual void SettingBeforeStartGame()
    {
        // init0
    }
    public abstract void StartMiniGame();

    #endregion

    #region PublicVariable

    #endregion

    #region Private Variable
    #endregion

    #region PrivateMethod

    protected virtual void EndMiniGame()
    {
       gameObject.SetActive(false);
        StopAllCoroutines();

        //for test
        MiniGameManager.instance.ShowButtons();
    }
    #endregion
}
