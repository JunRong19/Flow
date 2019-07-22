using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	//Reference: https://stackoverflow.com/questions/16745629/how-to-abstract-a-singleton-class

	// Stores the script intended to be a singleton, T allows any script to be stored (as long as they have MonoBehaviour)
	private static T instance;

	// Used by other scripts to get the instance of stored script
	public static T Instance {
		get {
			// Find and store the instance reference if none is set already
			if(instance == null) {
				instance = FindObjectOfType<T>();

				if(instance == null)
					Debug.LogWarning("No instance exists of type " + nameof(Singleton<T>));
			}

			return instance;
		}
	}
}