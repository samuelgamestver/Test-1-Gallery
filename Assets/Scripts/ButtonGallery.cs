using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGallery : MonoBehaviour, IPointerClickHandler
{
    public int number;

    public Gallery gal;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(gal)
            gal.OpenViewer(number);
    }
}
