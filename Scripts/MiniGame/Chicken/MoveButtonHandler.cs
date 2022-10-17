using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region PublicMethod
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonDown = false;
    }
    #endregion

    #region PublicVariable
    public bool isButtonDown { get; private set; } = false;
    #endregion

    #region PrivateVariable
    #endregion

    #region PrivateMethod
    #endregion
}
