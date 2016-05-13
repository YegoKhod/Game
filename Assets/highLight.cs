using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class highLight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Color HighLightColor;
	public Color NotActiveColor;

	public void OnPointerEnter (PointerEventData eventData)
	{
		GetComponent<Text> ().color = HighLightColor;
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		GetComponent<Text> ().color = NotActiveColor;
	}

}
