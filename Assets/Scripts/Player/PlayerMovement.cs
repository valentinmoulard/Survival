using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField]
    private Rigidbody m_rigidbodyReference;
    [SerializeField]
    private Transform m_groundCheckerTransformReference;
    [SerializeField]
    private Collider m_colliderReference;

    [Header("Player Settings")]
    [SerializeField]
    private float m_moveSpeed = 5f;
    [SerializeField]
    private float m_jumpHeight = 2f;
    [SerializeField]
    private float m_groundDistance = 0.2f;
    [SerializeField]
    private float m_dashDistance = 5f;
    [SerializeField]
    private float m_jumpRecoveryTime = 2.0f;
    [SerializeField]
    private float m_dashForce = 10.0f;

    private Vector3 m_inputVector = Vector3.zero;
    private bool m_isGrounded = true;
    private bool m_canJump = true;

    private Vector3 m_dashVelocityBuffer;

    void FixedUpdate()
    {
        GetInputAxisValues();

        if (m_inputVector != Vector3.zero)
            transform.forward = m_inputVector;

        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded && m_canJump)
            Jump();

        if (Input.GetKeyDown(KeyCode.A))
            Dash();

        m_rigidbodyReference.MovePosition(m_rigidbodyReference.position + m_inputVector * m_moveSpeed * Time.fixedDeltaTime);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            m_isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            m_isGrounded = false;
    }

    private void GetInputAxisValues()
    {
        m_inputVector = Vector3.zero;
        m_inputVector.x = Input.GetAxis("Horizontal");
        m_inputVector.z = Input.GetAxis("Vertical");
    }

    private void Jump()
    {
        m_canJump = false;
        m_rigidbodyReference.AddForce(Vector3.up * Mathf.Sqrt(m_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        StartCoroutine(JumpRecoveryCoroutine());
    }

    private void Dash()
    {
        m_rigidbodyReference.AddForce(transform.forward * m_dashForce, ForceMode.VelocityChange);
    }


    private IEnumerator JumpRecoveryCoroutine()
    {
        yield return new WaitForSeconds(m_jumpRecoveryTime);
        m_canJump = true;
    }

}
