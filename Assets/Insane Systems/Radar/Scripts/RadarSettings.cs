using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.Radar
{
    [CreateAssetMenu(fileName = "RadarSettings", menuName = "Insane Systems/Radar/Settings")]
    public class RadarSettings : ScriptableObject
    {
		[Tooltip("UI template of radar icon object. You can change this prefab settings to change some icon parameters, like size, pivot point, etc.")]
        public GameObject radarIconTemplate;
		[Tooltip("Radar zoom value. Default value is 1 - real scale on radar. Bigger values will increase scaling.")]
		[Range(0.1f, 4f)] public float radarScale = 0.1f;
		[Tooltip("If you need bigger icons on radar, increase this value.")]
		[Range(1f, 4f)] public float iconsScale = 1f;
		[Tooltip("If you're making an Action or RPG and this toggle checked, radar will rotate with player, otherwise only player icon will rotate.")]
		public bool rotateRadar;
		public Color customPrimaryUIColor = Color.white;
	}
}