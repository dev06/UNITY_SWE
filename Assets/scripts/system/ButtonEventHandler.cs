using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonEventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {


	public SystemEnum.ButtonID buttonID;
	public bool effect = true;
	public Color hoverColor;
	public Color restColor;

	void Start () {
		if (GetComponent<Image>() != null && effect)
		{
			GetComponent<Image>().color = restColor;


		}

	}


	public virtual void OnPointerClick(PointerEventData data)
	{
		if (EventManager.OnButtonClick != null)
		{
			EventManager.OnButtonClick(buttonID);
		}


	}
	public virtual void OnPointerEnter(PointerEventData data)
	{
		if (GetComponent<Image>() != null && effect)
		{
			GetComponent<Image>().color = hoverColor;


		}



	}
	public virtual void OnPointerExit(PointerEventData data)
	{
		if (GetComponent<Image>() != null && effect)
		{
			GetComponent<Image>().color = restColor;


		}


	}
}


