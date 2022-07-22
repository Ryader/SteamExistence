using UnityEngine;

public class Weapon : MonoBehaviour 
{
	public Transform targetLook;
	public Transform lHandTarget;
	public GameObject cameraMain;

	void Update () 
	{
		Vector3 dir = targetLook.position;
		RaycastHit hit;
		Debug.DrawLine(cameraMain.transform.position, dir, Color.black);

	}
}
