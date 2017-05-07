using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	[RequireComponent(typeof (Chara2d))]
	public class CharaContr2d : MonoBehaviour
	{
		private Chara2d m_Character;
		private bool m_Jump;
		
		
		private void Awake()
		{
			m_Character = GetComponent<Chara2d>();
		}
		
		
		private void Update()
		{
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}
		
		
		private void FixedUpdate()
		{	
			// Read the inputs.
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			
			bool crouch = Input.GetKey(KeyCode.K);
			bool modeChange = Input.GetKey (KeyCode.L);
			bool roll = Input.GetKey (KeyCode.J);
			
			// Pass all parameters to the character control script.
			m_Character.Move(h, crouch, m_Jump, modeChange, roll);
			m_Jump = false;
		}
	}
}
