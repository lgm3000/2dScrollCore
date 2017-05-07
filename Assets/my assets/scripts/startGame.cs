using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	public class startGame : MonoBehaviour
	{
		private GUILayer test;
		void Start()
		{
			test = Camera.main.GetComponent<GUILayer>();//获取主摄像机对应的guilayer 
		}
		void OnMouseDown()
		{
			Application.LoadLevel("hoho");
		}
	}
}
