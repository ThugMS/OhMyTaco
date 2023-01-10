using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopController : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        GetComponents();
        SetDefaults();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG) || collision.CompareTag(BASKET_TAG))
        {
            m_objectPool.ReturnPoop(gameObject);

            // 라이프 감소
            EggCatchManager.instance.LifeDecrease();
        }
        else if (collision.CompareTag(FLOOR_TAG))
        {
            // 바닥 충돌 애니메이션 추가

            m_objectPool.ReturnPoop(gameObject);
        }
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    Rigidbody2D m_rigidbody2D;
    CircleCollider2D m_circleCollider2D;

    EggObjectPool m_objectPool;

    const string BASKET_TAG = "Basket";
    const string PLAYER_TAG = "Player";
    const string FLOOR_TAG = "Floor";
    #endregion

    #region PrivateMethod
    void GetComponents()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void SetDefaults()
    {
        m_circleCollider2D.isTrigger = true;

        m_objectPool = EggCatchManager.instance.m_objectPool;
    }
    #endregion
}
