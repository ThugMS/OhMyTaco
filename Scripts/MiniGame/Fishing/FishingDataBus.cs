using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingDataBus : MonoBehaviour
{
    #region PublicMethod
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetScore(int _score) // ������ �� �����ؼ� ����
    {
        totalScore = _score;
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    [SerializeField]
    private int totalScore;
    #endregion

    #region PrivateMethod
    #endregion
}
