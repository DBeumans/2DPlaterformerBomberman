using UnityEngine;
using UnityEngine.UI;
using System;

using Splitten.Extensions;
/*
/// IDEAS
/// Add a layer system inside the canvas itself.
/// Custom layer by panels inside the window
/// Custom PanelUI script
 */
namespace Splitten.UI
{
    public class UIWindow : Singleton<UIWindow>
    {
        [SerializeField]protected Canvas windowCanvas;

        protected UIController UIController;

        private void Awake()
        {
            if(this.windowCanvas == null)
                Debug.Log($"{this} has no screen assigned");
            
            this.UIController = UIController.Instance;
        }

        public virtual void EnableWindow(Action OnWindowEnabled = null)
        {
            this.windowCanvas.gameObject.SetActive(true);

            Debug.Log($"ENABLED {this} ");
            OnWindowEnabled?.Invoke();
        }

        public virtual void DisableWindow(Action OnWindowDisabled = null) 
        {
            this.windowCanvas.gameObject.SetActive(false);
            Debug.Log($"DISABLED {this} ");
            OnWindowDisabled?.Invoke();
        }

        public virtual void UpdateWindow(Action OnWindowUpdated = null)
        {

            OnWindowUpdated?.Invoke();
        }
    }
}
