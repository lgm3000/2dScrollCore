using UnityEngine;
using System.Collections;

public class endroll : StateMachineBehaviour {

	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

	private BoxCollider2D m_collider;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool ("Roll", false);
		if (Physics2D.OverlapCircle(animator.gameObject.transform.Find("CeilingCheck").position, 0.1f, m_WhatIsGround))
		{
			animator.SetBool("Crouch",true);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool ("noControl", false);
		m_collider =  animator.gameObject.GetComponent<BoxCollider2D>();
		m_collider.enabled = true;

	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
