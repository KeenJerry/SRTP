using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.Radar
{
	public class SimplePlayer : MonoBehaviour
	{
		[SerializeField] float movingSpeed = 2f;

		public void Update()
		{
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");

			float increaseSpeed = Input.GetKey(KeyCode.LeftShift) ? 2.5f : 1f;

			transform.position += (transform.forward * y + transform.right * x) * Time.deltaTime * movingSpeed * increaseSpeed;

			float mouseX = Input.GetAxis("Mouse X");
			float mouseY = Input.GetAxis("Mouse Y");
			transform.localEulerAngles += new Vector3(-mouseY, mouseX);  
		}
	}
}