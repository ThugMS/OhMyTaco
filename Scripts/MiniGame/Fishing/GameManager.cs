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
    [SerializeField]
    FishingDataBus _fishDataBus;   // Scene간 데이터 이동을 원활히 하기 위한 스크립트
    [SerializeField]
    TextMeshProUGUI[] _scoreTextMeshPro; // 점수를 기록하는 텍스트

    int totalScore; // 총 얻은 점수

    private void Start()
    {
        totalScore = 0;
    }

    public void SetPoint(float _distanceinterval, int _chanceIdx)
    {
        /*
         * 위치 차이에 따른 점수 목록(임의 지정)
         * 0.2      7점
         * --------------
         * 0.6      6점
         * --------------
         * 1
         * 1.4      5점
         * --------------
         * 1.8
         * 2.2      4점
         * --------------
         * 2.6
         * 3        3점
         * --------------
         * 3.4
         * 3.8      2점
         * --------------
         * 4.2
         * 4.6
         * 5        1점
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

        if(_chanceIdx == 2)
            FinishFishing(); // 다 했으면 종료
    }
    public void FinishFishing() // 점수 등록 후 Scene변경
    {
        _fishDataBus.SetScore(totalScore); 

        SceneManager.LoadScene("SampleStart");
    }
}
