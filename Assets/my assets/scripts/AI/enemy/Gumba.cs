using System;	
using UnityEngine;

namespace UnityStandardAssets._2D
{
	public class Gumba: MonoBehaviour
	{
		private float m_moveDir;
		private float m_Speed = 4f;                    // The fastest the player can travel in the x axis.
		//private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
		[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

		private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
		private Transform m_WallCheck;
		const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
		private bool m_Grounded;            // Whether or not the player is grounded.
		private Animator m_Anim;            // Reference to the player's animator component.

		private bool chase;
		private Transform playerTrans;
		private BoxCollider2D m_standCollider;
		private SpriteRenderer m_shader;
		private Rigidbody2D m_Rigidbody2D;
		private bool m_FacingRight = true;  // For determining which way the player is currently facing.

		private void Awake()
		{
			// Setting up references.
			m_WallCheck = transform.FindChild("WallCheck");
			m_GroundCheck = transform.FindChild("GroundCheck");
			m_Anim = GetComponent<Animator>();
			m_standCollider = GetComponent<BoxCollider2D>();
			m_shader = GetComponent<SpriteRenderer>();
			m_Rigidbody2D = GetComponent<Rigidbody2D>();
			playerTrans = GameObject.FindGameObjectWithTag ("Player").transform;
			chase = false;
			m_moveDir = 1;
		}

		private void FixedUpdate()
		{
			Move ();

			m_Grounded = false;
			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
					m_Grounded = true;
			}
			bool bump = false;
			Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_WallCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < colliders2.Length; i++)
			{
				if (colliders2[i].gameObject != gameObject)
					bump = true;
			}
			if (!m_Grounded || bump) {
				EndChase ();
				m_moveDir = -m_moveDir;
			}
			
			m_Anim.SetBool("Ground", m_Grounded);
			m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
			m_Anim.SetFloat("Speed", Mathf.Abs( m_Rigidbody2D.velocity.x));
		}


		public void Move()
		{
			if (chase) 
			{
				m_moveDir = (playerTrans.position.x - m_Rigidbody2D.position.x)>0?1:-1;
				m_Rigidbody2D.velocity = new Vector2 (m_moveDir > 0 ? m_Speed : -m_Speed, m_Rigidbody2D.velocity.y);

			}
			else
				m_Rigidbody2D.velocity = new Vector2 (m_moveDir, m_Rigidbody2D.velocity.y);
			

			// If the input is moving the player right and the player is facing left...
			if (m_moveDir > 0 && !m_FacingRight) {
				// ... flip the player.
				Flip ();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (m_moveDir < 0 && m_FacingRight) {
				// ... flip the player.
				Flip ();
			}


		}
		public void OnMyTriggerEnter()
		{
			StartChase ();
		}


		public void OnMyTriggerExit()
		{
			EndChase ();
		}


		private void Flip()
		{
			// Switch the way the player is labelled as facing.
			m_FacingRight = !m_FacingRight;

			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		private void StartChase(){
			chase = true;
			m_GroundCheck.GetComponent<BoxCollider2D> ().enabled = true;

		}
		private void EndChase(){
			chase = false;
			m_GroundCheck.GetComponent<BoxCollider2D> ().enabled = false;
		}

	}
}
