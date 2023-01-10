using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class EggController : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        GetComponents();
        SetDefaults();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BASKET_TAG))
        {
            EggCatchManager.instance.m_objectPool.ReturnEgg(gameObject);
            EggCatchManager.instance.scoreUI.AddScore(1);
        }
        else if (collision.CompareTag(FLOOR_TAG))
        {
            // 바닥 충돌 애니메이션 추가

            EggCatchManager.instance.m_objectPool.ReturnEgg(gameObject);
        }
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    Rigidbody2D m_rigidbody2D;
    CircleCollider2D m_circleCollider2D;

    const string BASKET_TAG = "Basket";
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
    }
    #endregion
}
