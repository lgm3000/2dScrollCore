using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D{
	public class comfortZone : MonoBehaviour {
		private void OnTriggerExit2D(Collider2D other){

			if(other.tag == "Player")
			{
				transform.parent.gameObject.SendMessage("OnMyTriggerExit");
				//GameObject.Find ("follow").SendMessage 
			}
		}
	}
}