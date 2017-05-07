using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D{
	public class enterArea : MonoBehaviour {
		private void OnTriggerEnter2D(Collider2D other){
			
			if(other.tag == "Player")
			{
				transform.parent.gameObject.SendMessage("OnMyTriggerEnter");
				//GameObject.Find ("follow").SendMessage 
			}
		}
	}
}