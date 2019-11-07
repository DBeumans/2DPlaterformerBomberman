using UnityEngine;

namespace Splitten.Extensions
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                instance = (T)FindObjectOfType(typeof(T));

                if(instance == null)
                {
                    Debug.LogWarning("Couldn't find instance of: " + typeof(T).ToString() + " Returning NULL");
                    return null;
                }

                return instance;
            }
        }
    }
}