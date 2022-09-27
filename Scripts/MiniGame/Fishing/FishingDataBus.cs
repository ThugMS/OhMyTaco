using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingDataBus : MonoBehaviour
{
    [SerializeField]
    int totalScore;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetScore(int _score) // 점수를 총 정리해서 저장
    {
        totalScore = _score;
    }
}
