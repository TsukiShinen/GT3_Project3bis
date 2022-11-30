using UnityEngine;

namespace Library
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{

		#region Fields
		private static T _instance;
		#endregion Fields

		#region Properties
		public static T Instance
		{
			get
			{
				if (_instance) return _instance;
				_instance = FindObjectOfType<T>();

				if (!_instance)
				{
					throw new System.Exception(typeof(T) + " Trying to access a null instance of a singleton. Exiting.");
				}
				return _instance;
			}
		}
		#endregion Properties

		#region Methods
		#region MonoBehaviour
		protected virtual void Awake() { }

		protected virtual void Start()
		{
			DontDestroyOnLoad(this);
		}

		protected virtual void OnEnable() { }
		protected virtual void Update() { }
		protected virtual void LateUpdate() { }
		protected virtual void OnDisable() { }

		protected virtual void OnDestroy()
		{
			_instance = null;
		}
		#endregion MonoBehaviour
		#endregion Methods
	}
}