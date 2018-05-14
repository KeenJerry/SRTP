using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.Radar
{
    public class RadarSystem : MonoBehaviour
    {
        const float pauseBetweenSearches = 1f;

        public static RadarSystem sceneSingleton { get; protected set; }

		[SerializeField] RadarSettings radarSettings;
		[SerializeField] UI.RadarDrawer radarDrawer;
        [SerializeField] [Range(0f, 1f)] float pauseBetweenUpdates = 0.1f;

        public RadarObject centerObject { get; protected set; }
		public RadarSettings settings { get { return radarSettings; } }

        List<RadarObject> actualRadarObjects = new List<RadarObject>();

        float timeToNextSearch;
        float timeToNextUpdate;

		public float pauseBetweenUpdatesValue
		{
			get { return pauseBetweenUpdates; }
		}

        private void Awake()
        {
            sceneSingleton = this;
        }

		private void Start()
		{
			if (!radarDrawer)
			{
				radarDrawer = FindObjectOfType<UI.RadarDrawer>();

				if (!radarDrawer)
				{
					Debug.LogWarning("[RadarSystem] No RadarDrawer setted up into RadarSystem. Please, setup it and restart scene.");
					enabled = false;
				}
			}
		}

		void Update()
        {
            FindNewRadarObjects();
            UpdateActualRadarObjects();
        }

        void UpdateActualRadarObjects()
        {
            if (timeToNextUpdate > 0)
            {
                timeToNextUpdate -= Time.deltaTime;
                return;
            }

            for (int i = actualRadarObjects.Count - 1; i >= 0; i--)
            {
				if (!actualRadarObjects[i])
				{
					actualRadarObjects.RemoveAt(i);
					continue;
				}

				radarDrawer.UpdateIconForObject(actualRadarObjects[i]);
            }

            timeToNextUpdate = pauseBetweenUpdates;
        }

        void FindNewRadarObjects()
        {
            if (timeToNextSearch > 0)
            {
                timeToNextSearch -= Time.deltaTime;
                return;
            }

            RadarObject[] radarObjects = FindObjectsOfType<RadarObject>();

            for (int i = 0; i < radarObjects.Length; i++)
            {
                if (!centerObject)
                {
                    if (!radarObjects[i].IsCenter)
                        continue;

                    centerObject = radarObjects[i];
                    centerObject.Initialize();

                    break;
                }

                if (!radarObjects[i].IsInitialized && !radarObjects[i].IsCenter)
                {
                    actualRadarObjects.Add(radarObjects[i]);

                    radarObjects[i].Initialize();
                }
            }

            timeToNextSearch = pauseBetweenSearches;
        }
    }
}