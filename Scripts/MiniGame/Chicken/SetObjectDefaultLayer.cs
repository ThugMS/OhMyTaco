using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetObjectDefaultLayer : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        m_sortingLayerID = SortingLayer.NameToID(m_sortingLayerName);

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
            transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerID = m_sortingLayerID;
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    [SerializeField]
    string m_sortingLayerName;

    int m_sortingLayerID;
    #endregion

    #region PrivateMethod
    #endregion
}
