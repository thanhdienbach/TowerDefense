using UnityEngine;

namespace TowerDefense.UI
{
	/// <summary>
	/// A simple component that applies a constant rotation to a transform
	/// </summary>
	public class Rotator : MonoBehaviour
	{
		public Vector3 rotationSpeed;
		public bool isRotation;

		void Start ()
		{
			isRotation = true;
		}
		void Update ()
		{
            if (isRotation)
            {
                transform.localEulerAngles += rotationSpeed;
            }
            else if (!isRotation)
			{
				transform.rotation = Quaternion.identity;
			}
		}
	}
}
