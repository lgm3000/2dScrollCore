using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class wallDetection:MonoBehaviour
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			Debug.Log("collide!");
			transform.parent.gameObject.SendMessage("OnMyCollisionEnter");
		}
	}
}

