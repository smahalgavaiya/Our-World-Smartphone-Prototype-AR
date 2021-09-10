using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ColourPIcker : MonoBehaviour, IPointerClickHandler
{
    public Color pickedColor;

    [System.Serializable]
    public class ColorEvent : UnityEvent<Color>
    {

    }
    public ColorEvent OnColorPicked = new ColorEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        pickedColor = PickColor(GetPickerUVPosition());
        OnColorPicked.Invoke(pickedColor);
    }

    private Color PickColor(Vector2 pos)
    {
        Texture2D texture = GetComponent<Image>().sprite.texture;
        Color picked = texture.GetPixelBilinear(pos.x, pos.y);
        picked.a = 1;
        return picked;
    }

    Vector2 GetPickerUVPosition()
    {
        Vector3[] imageCorners = new Vector3[4];
        gameObject.GetComponent<RectTransform>().GetWorldCorners(imageCorners);
        float textureWidth = imageCorners[2].x - imageCorners[0].x;
        float textureHeight = imageCorners[2].y - imageCorners[0].y;
        float uvPosX = (Input.mousePosition.x - imageCorners[0].x) / textureWidth;
        float uvPosY = (Input.mousePosition.y - imageCorners[0].y) / textureHeight;
        return new Vector2(uvPosX, uvPosY);
    }


}
