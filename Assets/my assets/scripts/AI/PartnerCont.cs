/*//------------------------------------------------------------------------------

Script to control behaviour of your gay buddy following you.
Created 2016.8.1:
		-Follows main chara
		-jumps with main chara
		-teleports to main chara when out of certain range
		-ignores collision with all sprites
Update  2016.8.2:
		-Now lands at same spot with main chara after jumping
		-Now changes fucking color with distance between main Chara

*///------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	public class PartnerCont : MonoBehaviour
	{
		[SerializeField] private LayerMask m_WhatIsGround;		//determines what is ground for chara



		private Transform m_GroundCheck;
		private Transform playerTrans;
		private Rigidbody2D m_Rigidbody2D;
		private Animator m_Anim;
		private CircleCollider2D m_feet;
		private bool m_Grounded;
		private SpriteRenderer m_shader;
		private bool m_FacingRight = true;



		//debug variables
		//private float debug_max_tmp_dist;
		private void Awake()
		{
			//initialisations
			m_GroundCheck = transform.Find("GroundCheck");
			m_Rigidbody2D = GetComponent<Rigidbody2D> ();
			m_Anim = GetComponent<Animator>();
			m_feet = GetComponent<CircleCollider2D> ();
			m_shader = GetComponent<SpriteRenderer> ();
			playerTrans = GameObject.FindGameObjectWithTag ("Player").transform;
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Follower"), LayerMask.NameToLayer("Sprites"),true);

			//debug_max_tmp_dist = 0;
		}

		private void FixedUpdate()
		{
			Move();
			m_Grounded = false;
			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.1f, m_WhatIsGround);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
					m_Grounded = true;
			}
			m_Anim.SetBool("Ground", m_Grounded);
			m_Anim.SetFloat("Speed", Mathf.Abs (m_Rigidbody2D.velocity.x));
			m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
		}

		private void Move ()
		{	
			float tmp_dist = (playerTrans.position.x - m_Rigidbody2D.position.x);
			//Debugging
			/*if (Mathf.Abs (tmp_dist) > debug_max_tmp_dist) {
				debug_max_tmp_dist = Mathf.Abs (tmp_dist);
				Debug.Log (Mathf.Abs (tmp_dist));
			}*/
				

			m_shader.color = Color.HSVToRGB (Mathf.Abs(tmp_dist)/4.1f, 0.7f, 0.8f);
			float tmp_velocity;
			if(m_Grounded)
				tmp_velocity = Mathf.Abs(tmp_dist) < 0.8 ? 0 : (tmp_dist) * 2.5f;
			else
				tmp_velocity = (tmp_dist) * 3f;
			
			
			//gives copied movement to buddy
			m_Rigidbody2D.velocity = new Vector2(tmp_velocity,m_Rigidbody2D.velocity.y);
			
			
			//determines whether buddy should jump or not
			if (playerTrans.position.y - m_Rigidbody2D.transform.position.y > 1 && Mathf.Abs(playerTrans.position.x - m_Rigidbody2D.transform.position.x) > 2 && m_Grounded)
				m_Rigidbody2D.AddForce (new Vector2(0f, 340f));
			
			//buddy flip
			if (m_Rigidbody2D.velocity.x > 0 && !m_FacingRight) {
				Flip ();
			}
			else if (m_Rigidbody2D.velocity.x < 0 && m_FacingRight) {
				Flip ();
			}

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

		//used to determine whether buddy is too far from main chara
		public void OnMyTriggerExit()
		{
			m_Rigidbody2D.position = playerTrans.position;
		}

	}

}
