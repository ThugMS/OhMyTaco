using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    #region PublicMethod
    void Start()
    {
        totalScore = 0;
    }

    public void SetPoint(float _distanceinterval, int _chanceIdx) // ��ư Ŭ���� ���� ��ġ�� ���� ���� ����
    {
        /*
         * ��ġ ���̿� ���� ���� ���(���� ����)
         * 0.2      7��
         * --------------
         * 0.6      6��
         * --------------
         * 1
         * 1.4      5��
         * --------------
         * 1.8
         * 2.2      4��
         * --------------
         * 2.6
         * 3        3��
         * --------------
         * 3.4
         * 3.8      2��
         * --------------
         * 4.2
         * 4.6
         * 5        1��
         */

        if (_distanceinterval < 0.21f)
            _scoreTextMeshPro[_chanceIdx].text = "7";
        else if (_distanceinterval < 0.61f)
            _scoreTextMeshPro[_chanceIdx].text = "6";
        else if (_distanceinterval < 1.41f)
            _scoreTextMeshPro[_chanceIdx].text = "5";
        else if (_distanceinterval < 2.21f)
            _scoreTextMeshPro[_chanceIdx].text = "4";
        else if (_distanceinterval < 3.41f)
            _scoreTextMeshPro[_chanceIdx].text = "3";
        else if (_distanceinterval < 3.81f)
            _scoreTextMeshPro[_chanceIdx].text = "2";
        else
            _scoreTextMeshPro[_chanceIdx].text = "1";

        totalScore += Int32.Parse(_scoreTextMeshPro[_chanceIdx].text);

        if (_chanceIdx == 2)
            FinishFishing(); // �� ������ ����
    }

    public void FinishFishing() // ���� ��� �� Scene����
    {
        _fishDataBus.SetScore(totalScore);

        SceneManager.LoadScene("SampleStart");
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    [SerializeField]
    private FishingDataBus _fishDataBus;   // Scene�� ������ �̵��� ��Ȱ�� �ϱ� ���� ��ũ��Ʈ(�ӽ�)

    [SerializeField]
    private TextMeshProUGUI[] _scoreTextMeshPro; // ������ ����ϴ� �ؽ�Ʈ

    private int totalScore; // �� ���� ����
    #endregion

    #region PrivateMethod
    #endregion
}
