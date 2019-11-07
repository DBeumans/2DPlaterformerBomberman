using UnityEngine;
using Splitten.UI;

public class MainmenuWindow : UIWindow
{
    [SerializeField]private UIButton play;

    [SerializeField]private UIButton quit;

    private void Start()
    {
        play.OnButtonClick += ()=> {
            Debug.Log($"Play!");
            
        };

        quit.OnButtonClick += ()=> {
            Debug.Log($"Quit!");
            
        };
        
    }

    public override void EnableWindow(System.Action OnWindowEnabled = null)
    {
        base.EnableWindow(OnWindowEnabled);
    }
    public override void DisableWindow(System.Action OnWindowDisabled = null)
    {
        base.DisableWindow(OnWindowDisabled);
    }

}
