using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace InsaneSystems.Radar
{
	[CustomEditor(typeof(RadarSystem))]
	public class RadarSystemEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			RadarSystem radarSystem = target as RadarSystem;

			if (radarSystem.pauseBetweenUpdatesValue < 0.1f)
				EditorGUILayout.HelpBox("Small update pause can decrease performance.", MessageType.Warning);
		}
	}
}