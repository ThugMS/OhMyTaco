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
        if (collision.CompareTag(m_playerTag))
        {
            Destroy(gameObject);
            BuildManager.instance.scoreUI.AddScore(1);
        }
    }
    #endregion

    #region PublicVariable
    #endregion

    #region PrivateVariable
    Rigidbody2D m_rigidbody2D;
    CircleCollider2D m_circleCollider2D;

    string m_playerTag = "Player";
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
