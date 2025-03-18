using UnityEngine;
using UnityEngine.EventSystems;

// Class = MonoBehaviour
// IPointerEnterHandler and IPointerExitHandler are interfaces.
public class UICursorCapture : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool cursorOverUI = false;
    // Must implement for IPointerEnterHandler
    public void OnPointerEnter(PointerEventData eventData){
        cursorOverUI = true;
    }   

    // Must implement for IPointerExitHandler
    public void OnPointerExit(PointerEventData eventData){
        cursorOverUI = false;
    }
}