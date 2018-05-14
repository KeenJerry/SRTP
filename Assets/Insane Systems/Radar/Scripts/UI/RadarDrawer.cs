using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.Radar.UI
{
    public class RadarDrawer : MonoBehaviour
    {
		[Tooltip("Bring here RectTransform, which contains all radar UI Elements.")]
		[SerializeField] RectTransform mainRadarWindow;
		[Tooltip("Bring here objectsPanel parent RectTransform.")]
		[SerializeField] RectTransform radarPanelParent;
		[Tooltip("Bring here RectTransform, which will contain all radar icons objects.")]
		[SerializeField] RectTransform objectsPanel;
		[Tooltip("Bring here RectTransform, which will present center map icon (like player icon).")]
		[SerializeField] RectTransform directionArrow;

        Vector2 startPanelCenter; 

        private void Start()
        {
            startPanelCenter = objectsPanel.localPosition;
        }

        void Update()
        {
            if (RadarSystem.sceneSingleton.centerObject)
            {
				if (RadarSystem.sceneSingleton.settings.rotateRadar)
				{
					radarPanelParent.localEulerAngles = new Vector3(0, 0, RadarSystem.sceneSingleton.centerObject.GetEulerRotation());
					if (directionArrow.localEulerAngles.z != 0)
						directionArrow.localEulerAngles = new Vector3(0, 0, 0);
				}
				else
				{
					if (radarPanelParent.localEulerAngles.z != 0)
						radarPanelParent.localEulerAngles = new Vector3(0, 0, 0);
					directionArrow.localEulerAngles = new Vector3(0, 0, -RadarSystem.sceneSingleton.centerObject.GetEulerRotation());
				}

                objectsPanel.localPosition = startPanelCenter + RadarSystem.sceneSingleton.centerObject.GetOffsetFromStart() * RadarSystem.sceneSingleton.settings.radarScale; 
            }
        }

        public void DestroyIcon(Image icon)
        {
            Destroy(icon.gameObject);
        }

        public void UpdateIconForObject(RadarObject radarObject, bool isFirstDraw = false)
        {
            if (!radarObject.SelfIconTransform)
            {
                GameObject spawnedIcon = Instantiate(RadarSystem.sceneSingleton.settings.radarIconTemplate, objectsPanel);
                Image iconImage = spawnedIcon.GetComponent<Image>();
                RectTransform iconTransform = spawnedIcon.GetComponent<RectTransform>();

                iconImage.sprite = radarObject.IconSprite;
				iconImage.color = radarObject.CustomColor;
                radarObject.SetSelfIcon(iconTransform);
				iconTransform.SetAsFirstSibling();
				iconTransform.sizeDelta *= RadarSystem.sceneSingleton.settings.iconsScale; 

				isFirstDraw = true;
			}

			if (!isFirstDraw && (radarObject.IsStatic || !IsPointOnRadar(radarObject.SelfIconTransform.localPosition)))
				return;

            Vector2 centerObjectPosition = RadarSystem.sceneSingleton.centerObject.GetPosition();
            Vector2 radarObjectOffsetFromCenterObject = radarObject.GetPosition() - centerObjectPosition;
            Vector2 actualRadarTransformOffset = RadarSystem.sceneSingleton.centerObject.GetOffsetFromStart();

			// У нас есть смещение объекта относительно игрока. И оно корректно. Но у нас так же смещён трансформ радара. Чтобы исключить
			// двойное смещение, вычитаем смещение радара.
			// Using the offset locally by player, and it is right. But we also have ofsetted transform of the radar. 
			// To exclude double-offset, subtract the radar offset.
            radarObject.SelfIconTransform.localPosition = startPanelCenter + (radarObjectOffsetFromCenterObject - actualRadarTransformOffset) * RadarSystem.sceneSingleton.settings.radarScale;

			if (radarObject.ShouldBeVisibleAllTime)
			{
				Vector2 parentSize = mainRadarWindow.sizeDelta;
				Vector2 position = radarObject.SelfIconTransform.position - mainRadarWindow.position;

				if (position.x >= parentSize.x)
					position.x = parentSize.x;
				else if (position.x <= 0)
					position.x = 0;

				if (position.y >= parentSize.y)
					position.y = parentSize.y;
				else if (position.y <= 0)
					position.y = 0;

				radarObject.SelfIconTransform.position = (Vector3)position + mainRadarWindow.position;
			}
		}

		bool IsPointOnRadar(Vector2 point)
		{
			return point.x >= 0 && point.x <= objectsPanel.sizeDelta.x && point.y >= 0 && point.y <= objectsPanel.sizeDelta.y;
		}
    }
}