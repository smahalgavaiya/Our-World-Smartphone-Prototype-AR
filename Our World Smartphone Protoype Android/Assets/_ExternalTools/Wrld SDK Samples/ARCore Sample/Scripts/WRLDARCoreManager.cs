using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using GoogleARCore; // Please import Google ARCore plugin if you are seeing a compiler error here.

namespace WRLD.ARCore
{
	public class WRLDARCoreManager : MonoBehaviour
	{
		public Transform wrldMap;
		public Transform wrldMapMask;
        public UnityEngine.UI.Button buttonSwitchView;

        // Please import Google ARCore plugin if you are seeing a compiler error here.
        private TrackedPlane m_trackedPlane;

		// Please import Google ARCore plugin if you are seeing a compiler error here.
		private List<TrackedPlane> m_allPlanes = new List<TrackedPlane>();

		private WRLDARCorePositioner m_wrldMapARCorePositioner;

		public void Start()
		{
			m_wrldMapARCorePositioner = wrldMap.GetComponent<WRLDARCorePositioner> ();

            UnityEngine.UI.Button btn1 = buttonSwitchView.GetComponent<UnityEngine.UI.Button>();
            

            //Calls the TaskOnClick/TaskWithParameters method when you click the Button
            btn1.onClick.AddListener(ChangeToStandardView);
          //  btn2.onClick.AddListener(delegate { TaskWithParameters("Hello"); });
        }

        public void ChangeToStandardView()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("UnityWorldSpace");
        }

		public void Update ()
		{

			// Please import Google ARCore plugin if you are seeing a compiler error here.
			if (GoogleARCore.Session.Status != SessionStatus.Tracking)
			{
				const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
				Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
				return;
			}

			Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // Please import Google ARCore plugin if you are seeing a compiler error here.
            GoogleARCore.Session.GetTrackables(m_allPlanes);

			// Please import Google ARCore plugin if you are seeing a compiler error here.
			TrackedPlane firstValidPlane = null;
			for (int i = 0; i < m_allPlanes.Count; i++)
			{
				// Please import Google ARCore plugin if you are seeing a compiler error here.
				if (m_allPlanes[i].TrackingState == TrackingState.Tracking)
				{
					firstValidPlane = m_allPlanes [i];
					break;
				}
			}

			if (firstValidPlane==null) 
			{
				if (m_trackedPlane != null) 
				{
					m_trackedPlane = null;
					m_wrldMapARCorePositioner.CurrentTrackedPlane = null;
				}
			}
			else if(m_trackedPlane==null)
			{
				m_trackedPlane = firstValidPlane;
				m_wrldMapARCorePositioner.CurrentTrackedPlane = m_trackedPlane;
			}

		}
	}
}
