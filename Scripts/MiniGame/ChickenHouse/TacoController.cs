using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class TacoController : MonoBehaviour
{
    #region PublicMethod
    void OnEnable()
    {
        GetComponents();
        SetDefaults();
    }

    public void GoLeft()
    {
        if (m_rigidbody2D.velocity.x < -m_maxSpeed)
            return;
        m_rigidbody2D.AddForce(Vector3.left * m_speed, ForceMode2D.Force);

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void GoRight()
    {
        if (m_rigidbody2D.velocity.x > m_maxSpeed)
            return;
        m_rigidbody2D.AddForce(Vector3.right * m_speed, ForceMode2D.Force);

        transform.rotation = Quaternion.Euler(0, -180, 0);
    }
    #endregion

    #region PublicVariable
    public float speed { get { return m_speed; } }
    #endregion

    #region PrivateVariable
    [SerializeField][Range(0f, 10f)] float m_speed;
    [SerializeField][Range(0f, 10f)] float m_maxSpeed;

    Rigidbody2D m_rigidbody2D;
    CircleCollider2D m_circleCollider2D;
    #endregion

    #region PrivateMethod
    void GetComponents()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void SetDefaults()
    {
        if (m_speed > m_maxSpeed)
            m_speed = m_maxSpeed;
        m_circleCollider2D.isTrigger = true;
        tag = "Player";
    }
    #endregion
}
