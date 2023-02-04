using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour
{
	void Start()
	{
		// If you want it object be destroyed, destroy it manually via script.
		DontDestroyOnLoad(this.gameObject);
	}
}
