using UnityEngine;
using Splitten.UI;

public class newWindow : UIWindow
{
    public override void EnableWindow(System.Action OnWindowEnabled = null)
    {
        base.EnableWindow(OnWindowEnabled);
    }
    public override void DisableWindow(System.Action OnWindowDisabled = null)
    {
        base.DisableWindow(OnWindowDisabled);
    }
}
