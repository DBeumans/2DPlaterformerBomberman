using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Splitten.UI;
using Splitten.Extensions;

public class VirtualJoystick : Singleton<VirtualJoystick>
{
    [SerializeField]private Image backgroundImage;
    [SerializeField]private UIButton thumbstick;
    private UIDraggable uIDraggable;

    private Vector2 input;
    public Vector2 JoystickOutput
    {
        get => this.input;
    }

    private void Start()
    {
        this.uIDraggable = this.thumbstick.UIDraggable;

        this.uIDraggable.OnDragHold += this.MoveThumbstick;
        this.uIDraggable.OnDragEnd += this.Placeback;
    }

    private void MoveThumbstick(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            this.backgroundImage.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            //Getting the position
            pos.x = ( pos.x / this.backgroundImage.rectTransform.sizeDelta.x);
            pos.y = ( pos.y / this.backgroundImage.rectTransform.sizeDelta.y);

            //Checking left or right
            float x = (this.backgroundImage.rectTransform.pivot.x ==1 ) ? pos.x *2 + 1 : pos.x *2 - 1;
            //Checking up or down
            float y = (this.backgroundImage.rectTransform.pivot.y ==1 ) ? pos.y *2 + 1 : pos.y *2 - 1;
            
            this.input = new Vector3 (x, y , 0);
            this.input = (this.input.magnitude > 1) ? this.input.normalized : this.input;
            
            this.thumbstick.ButtonRectTransform.anchoredPosition = new Vector3(
                this.input.x * (this.backgroundImage.rectTransform.sizeDelta.x/3),
                0,0);
        }
        

    }

    private void Placeback()
    {
        this.thumbstick.transform.position = this.uIDraggable.StartPosition;
        this.input = Vector2.zero;
    }
}
