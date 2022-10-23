using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        instance = this;

        SetDefaults();
    }

    void Start()
    {
        StartCoroutine(LayEggs());
    }

    void Update()
    {
        if (m_targetTr.position.x < m_screenLeftSide.x)
            m_targetTr.position = m_screenLeftSide;
        else
        {
            if (m_baseUI.leftButton.isButtonDown)
                m_target.GoLeft();
        }

        if (m_targetTr.position.x > m_screenRightSide.x)
            m_targetTr.position = m_screenRightSide;
        else
        {
            if (m_baseUI.rightButton.isButtonDown)
                m_target.GoRight();
        }
    }
    #endregion

    #region PublicVariable
    public static BuildManager instance = null;
    public bool isGameEnd { get; set; } = false;
    public BaseUI baseUI { get { return m_baseUI; } }
    public ScoreUI scoreUI { get { return m_scoreUI; } }
    public Vector2 screenRightSideX { get { return m_screenRightSide; } }
    public Vector2 screenLeftSideX { get { return m_screenLeftSide; } }
    #endregion

    #region PrivateVariable
    [Header("UI Variables")]
    [SerializeField]
    BaseUI m_baseUI;
    [SerializeField]
    ScoreUI m_scoreUI;
    [SerializeField]
    SerialImagesHandler m_chickens;
    [SerializeField]
    Vector2 m_screenRightSide;
    [SerializeField]
    Vector2 m_screenLeftSide;

    [Header("Object Variables")]
    [SerializeField]
    TacoController m_target;

    Transform m_targetTr = null;

    [Header("Coroutine Variables")]
    [SerializeField]
    float m_layingInterval;

    [Header("Prefabs")]
    [SerializeField]
    GameObject m_eggPrefab;
    #endregion

    #region PrivateMethod
    void SetDefaults()
    {
        m_targetTr = m_target.transform;
    }

    IEnumerator LayEggs()
    {
        WaitForSeconds layingInterval = new WaitForSeconds(m_layingInterval);
        int chickenCount = m_chickens.images.Length;

        while (!isGameEnd)
        {
            GameObject eggClone = Instantiate(m_eggPrefab, m_chickens.images[Random.Range(0, chickenCount)].transform.position, Quaternion.identity);
            yield return layingInterval;
        }
    }
    #endregion
}
