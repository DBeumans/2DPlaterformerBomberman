using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Splitten.UI
{
    public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector2 buttonSize;
        private Rect buttonBounds;
        private RectTransform buttonRectTransform;
        public RectTransform ButtonRectTransform
        {
            get => this.buttonRectTransform;
        }

        [SerializeField]private bool interactable = true;
        [SerializeField]private bool draggable = false;
        private UIDraggable uIDraggable;
        public UIDraggable UIDraggable
        {
            get => this.uIDraggable;
        }
        [SerializeField]private ButtonTransitionType transitionType = ButtonTransitionType.COLOR;
        [SerializeField]private ButtonState buttonState = ButtonState.RELEASED;

        public AudioClip ButtonAudio
        {
            get => this.buttonAudio; 
        }
        [SerializeField]private AudioClip buttonAudio = default;
        
        private Vector2 clickPosition;

        private Image imageComponent;

        #region Sprite
        [SerializeField]private Sprite defaultSprite = default;
        [SerializeField]private Sprite pressedSprite = default;
        [SerializeField]private Sprite disabledSprite = default;
        #endregion

        #region Color
        [SerializeField]private Color defaultColor = default;
        [SerializeField]private Color pressedColor = default;
        [SerializeField]private Color disabledColor = default;
        #endregion
        
        #region Actions
        public Action OnButtonDown;
        public Action OnButtonUp;
        public Action OnButtonClick;
        #endregion
        // edit label button toevoegen in inspector ?
        // om naar text component te gaan. Of text component properties kunnen laten zien.
        [SerializeField]private string buttonLabel;

        #region Methods
        private void Awake()
        {
            if(this.draggable == true)
            {
                this.uIDraggable = this.GetComponent<UIDraggable>();
                    
                if(this.uIDraggable == null)
                    Debug.LogError($"{this} No UIDraggable component detected!");
                
            }
                

            this.CalculateButtonInformation();
            //Debug.Log($"{this.name} // Setup: {this.InitializeTransitionTypes(this.transitionType)} ");
            this.InitializeTransitionTypes(this.transitionType);
        }

        #region IPointerMethods
        public void OnPointerDown(PointerEventData eventData)
        {
            if(this.interactable == false)
                return;
            
            //Checking if the player used the left mouse button to click. If not, no click will be detected.
            if(eventData.button != PointerEventData.InputButton.Left || this.enabled == false)
                return;

            //Saving the click position. Used to calculate if its a click press or not.
            this.clickPosition = eventData.position;

            this.buttonState = ButtonState.PRESSED;
            this.Transition(this.transitionType);
            this.OnButtonDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData) 
        {
            if(this.interactable == false)
                return;
            
            if(this.buttonState != ButtonState.PRESSED)
                return;

            //Checking if the player used the left mouse button to click. If not, no click will be detected.
            if(eventData.button != PointerEventData.InputButton.Left || this.enabled == false)
                return;
            
            if(this.buttonBounds.Contains(this.clickPosition))
            {
                this.OnClick();
                this.clickPosition = default;
            }

            this.buttonState = ButtonState.RELEASED;
            this.Transition(this.transitionType);
            this.OnButtonUp?.Invoke();
        }
        #endregion

        /// <summary>
        /// Method called when button click is detected.
        /// </summary>
        private void OnClick()
        {
            Debug.Log($"{this.name} has been clicked");     
            this.OnButtonClick?.Invoke();
        }

        private void Transition(ButtonTransitionType type)
        {
            switch(type)
            {
                case ButtonTransitionType.COLOR:
                {
                    if(this.buttonState == ButtonState.PRESSED)
                    {
                        this.imageComponent.color = this.pressedColor;
                        return; 
                    }
                    if(this.buttonState == ButtonState.RELEASED)
                    {
                        this.imageComponent.color = this.defaultColor;
                        return;
                    }
                    break;
                }
                case ButtonTransitionType.SPRITE:
                {
                    if(this.buttonState == ButtonState.PRESSED)
                    {
                        this.imageComponent.sprite = this.pressedSprite;
                        return; 
                    }
                    if(this.buttonState == ButtonState.RELEASED)
                    {
                        this.imageComponent.sprite = this.defaultSprite;
                        return;
                    }
                    break;
                }
            }
        }
                

        /// <summary>
        /// Calculating the button bounds.
        /// </summary>
        private void CalculateButtonInformation()
        {
            float width = 0;
            float height = 0;
            float posX = 0;
            float posY = 0;

            this.buttonRectTransform = this.GetComponent<RectTransform>();

            width = this.buttonRectTransform.sizeDelta.x;
            height = this.buttonRectTransform.sizeDelta.y;
            this.buttonSize = new Vector2(width, height);

            posX = this.buttonRectTransform.position.x;
            posY = this.buttonRectTransform.position.y;

            this.buttonBounds = new Rect(posX - (width/2), posY - (height/2), width, height);

        }

        private bool InitializeTransitionTypes(ButtonTransitionType type)
        {
            if(this.imageComponent == null)
                this.imageComponent = this.GetComponent<Image>();

            switch(type)
            {
                case ButtonTransitionType.COLOR:
                {
                    /*usefull? */
                    if(this.defaultColor == null)
                        return false;

                    if(this.pressedColor == null)
                        return false;

                    if(this.disabledColor == null)
                        return false;
                    /*end */

                    this.imageComponent.color = this.defaultColor;
                    break;
                }

                case ButtonTransitionType.SPRITE:
                {
                    if(this.defaultSprite == null)
                        return false;
            
                    if(this.pressedSprite == null)
                        return false;
                    if(this.disabledSprite == null)
                        return false;

                    this.imageComponent.sprite = this.defaultSprite;
                    break;
                }

            }
            return true;
            
        }

        #endregion
    }
}

