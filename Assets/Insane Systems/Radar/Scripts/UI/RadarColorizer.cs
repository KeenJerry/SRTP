using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.Radar.UI
{
	public class RadarColorizer : MonoBehaviour
	{
		[SerializeField] Graphic[] interfaceElementsToColorize;

		void Start()
		{
			Color interfaceColor = RadarSystem.sceneSingleton.settings.customPrimaryUIColor;

			for (int i = 0; i < interfaceElementsToColorize.Length; i++)
				interfaceElementsToColorize[i].color = interfaceColor;
		}
	}
}