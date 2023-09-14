/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggeable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    Transform parentToReturnTo = null;
     public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log ("OnBeginDrag");
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.SetParent(this.transform.parent.parent);

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        this.transform.SetParent(parentToReturnTo);
    }

}
*/