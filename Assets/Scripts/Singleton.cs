using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	private static T _Instance;
	
	public static T Instance
	{
		get
		{
			if(_Instance == null)
			{
				_Instance = FindObjectOfType<T>();
				if (_Instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					_Instance = obj.AddComponent<T>();
				}
			}
			return _Instance;
		}
	}

	protected virtual void Awake()
	{
		if(_Instance == null)
		{
			_Instance = this as T;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public static bool IsDestroyed
	{
		get
		{
			return _Instance == null;
		}
	}
}