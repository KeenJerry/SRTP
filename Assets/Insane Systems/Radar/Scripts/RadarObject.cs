using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.Radar
{
    public class RadarObject : MonoBehaviour
    {
		[Tooltip("Icon, which will be drawn on radar for this object.")]
		[SerializeField] Sprite icon;
		[Tooltip("If icon need custom color, you can change this parameter.")]
		[SerializeField] Color customColor = Color.white;
		[Tooltip("Turn on this on the object, which the radar should follow. For example, on a player.")]
        [SerializeField] [RadarObjectCenter] bool isCenter;
		bool shouldBeVisibleAllTime; // temporary hidden 
		[Tooltip("Turn on this, if this object will stay on one position during all game.")]
		[SerializeField] bool isStatic;

        RectTransform selfIconTransform;

        new Transform transform;
        bool isInitialized = false;

        Vector2 startPositon;
        Vector2 lastPosition;

        public Sprite IconSprite
        {
            get { return icon; }
        }


		public Color CustomColor
		{
			get { return customColor; }
		}

        public bool IsInitialized
        {
            get { return isInitialized; }
        }

        public bool IsCenter
        {
            get { return isCenter; }
        }

		public bool ShouldBeVisibleAllTime
		{
			get { return shouldBeVisibleAllTime; }
		}

		public RectTransform SelfIconTransform
        {
            get { return selfIconTransform; }
        }

		public bool IsStatic
		{
			get { return isStatic; }
		}

        public Vector2 GetPosition()
        {
            return new Vector2(transform.position.x, transform.position.z);
        }

        public Vector2 GetMoveDelta()
        {
            Vector2 delta = GetPosition() - lastPosition;
            lastPosition = GetPosition();

            return delta;
        }

        public Vector2 GetOffsetFromStart()
        {
            return startPositon - GetPosition();
        }

        public float GetEulerRotation()
        {
            return transform.localEulerAngles.y;
        }

        public void Initialize()
        {
            isInitialized = true;

            transform = GetComponent<Transform>();
            startPositon = GetPosition();
            lastPosition = startPositon;
        }

        public void SetSelfIcon(RectTransform transform)
        {
            selfIconTransform = transform;
        }

        public void Destruct()
        {
            if (selfIconTransform)
                Destroy(selfIconTransform.gameObject);

            Destroy(this);
        }

        public void OnDestroy()
        {
            Destruct();
        }
    }

	public class RadarObjectCenterAttribute : PropertyAttribute { }
}