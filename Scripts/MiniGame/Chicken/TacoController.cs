using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class TacoController : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void GoLeft()
    {
        m_rigidbody2D.AddForce(Vector3.left * m_speed);
    }

    public void GoRight()
    {
        m_rigidbody2D.AddForce(Vector3.right * m_speed);
    }
    #endregion

    #region PublicVariable
    public float speed { get { return m_speed; } }
    #endregion

    #region PrivateVariable
    [SerializeField][Range(0f, 1000f)]
    float m_speed;

    Rigidbody2D m_rigidbody2D;
    #endregion

    #region PrivateMethod
    #endregion
}
