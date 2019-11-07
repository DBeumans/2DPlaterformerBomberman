using UnityEngine;
using System;
using System.Collections.Generic;

using Splitten.Extensions;
using Splitten.Audio;

namespace Splitten.UI
{
    public class UIController : Singleton<UIController>
    {
        private UIWindow activeWindow;

        [SerializeField]private List<UIWindow> windows = new List<UIWindow>();

        private void Awake()
	    {
            this.FetchWindows();

            this.activeWindow = this.windows[0];//replace this with a way to detect a default window, not simply drag and drop in editor ( maybe if nothing works )
            this.activeWindow.EnableWindow();
	    }

        /// <summary>
        /// Change from UI Window
        /// </summary>
        /// <param name="OnWindowChanged">Callback when window has been changed, returning the newly changed window</param>
        /// <typeparam name="T">UIWindow class to change to</typeparam>
        public void GoToWindow<T>(Action<UIWindow> OnWindowChanged = null)
        {
            this.GoToWindow(typeof(T), OnWindowChanged);
        }

        /// <summary>
        /// Change from UI Window
        /// </summary>
        /// <param name="type">UIWindow class to change to</param>
        /// <param name="OnWindowChanged">Callback when window has been changed, returning the newly changed window</param>
        public void GoToWindow(System.Type type, Action<UIWindow> OnWindowChanged = null)
        {
            this.activeWindow.DisableWindow();

            int windowsLength = this.windows.Count;
            for(int i = 0; i < windowsLength; i++)
            {
                UIWindow currentWindow = this.windows[i];
                if(!type.Equals(currentWindow.GetType()))
                    continue;
                
                this.activeWindow = currentWindow;
            }

            this.activeWindow.EnableWindow();
            OnWindowChanged?.Invoke(this.activeWindow);
        }

        /*
        popup functie
        overlay functie.. ?

        window is static venster
        
        verdeel ze in custom layers

        window altijd achtergrond
        popup altijd voorgrond
        indien overlay - tussen popup en window in
         */

    	/// <summary>
        /// Fetching all the UIWindows inside the UIController GameObject.
        /// </summary>
        private void FetchWindows()
        {
            UIWindow[] uiwindows = this.GetComponents<UIWindow>();

            int windowsLength = uiwindows.Length;
            for (int i = 0; i < windowsLength; i++)
            {
                this.windows.Add(uiwindows[i]);
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                this.GoToWindow<newWindow>();
            }

            if(Input.GetKeyDown(KeyCode.G))
            {
                this.GoToWindow<MainmenuWindow>();
            }
        }
    }
}