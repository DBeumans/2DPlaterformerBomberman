using System;

using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 startPosition;
    public Vector2 StartPosition
    {
        get => this.startPosition;
    }
    private Vector2 endPosition;
    public Vector2 EndPosition
    {
        get => this.endPosition;
    }
    private Vector2 currentPosition;
    public Vector2 CurrentPosition
    {
        get => this.currentPosition;
    }
    public Action OnDragBegin;
    public Action<PointerEventData> OnDragHold;
    public Action OnDragEnd;

    private void Start()
    {
        this.startPosition = this.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.OnDragBegin?.Invoke();
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        this.currentPosition = eventData.position;
        this.OnDragHold?.Invoke(eventData);
    }
       
    public void OnEndDrag(PointerEventData eventData)
    {
        this.endPosition = this.transform.position;
        this.OnDragEnd?.Invoke();
    }

}
