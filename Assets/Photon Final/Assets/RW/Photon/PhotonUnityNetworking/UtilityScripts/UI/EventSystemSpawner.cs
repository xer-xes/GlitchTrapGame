using UnityEngine;
using UnityEngine.EventSystems;

namespace Photon.Pun.UtilityScripts
{
	/// <summary>
	/// Event system spawner. Will add an EventSystem GameObject with an EventSystem component and a StandaloneInputModule component
	/// Use this in additive scene loading context where you would otherwise get a "Multiple eventsystem in scene... this is not supported" error from Unity
	/// </summary>
	public class EventSystemSpawner : MonoBehaviour 
	{
		void OnEnable()
		{
			EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
			if (sceneEventSystem == null)
			{
				GameObject eventSystem = new GameObject("EventSystem");

				eventSystem.AddComponent<EventSystem>();
				eventSystem.AddComponent<StandaloneInputModule>();
			}
		}
	}
}