using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonEventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {


	public SystemEnum.ButtonID buttonID;
	public bool disabled;
	public bool effect = true;
	public Color hoverColor;
	public Color restColor;
	public Color disabledColor;
	private Color defaultColor;
	private Image image;
	void Start () {
		image = GetComponent<Image>();
		if (image != null)
		{	defaultColor = image.color;

		}
		if (GetComponent<Image>() != null && effect)
		{
			GetComponent<Image>().color = restColor;


		}

	}

	void Update()
	{

	}


	public virtual void OnPointerClick(PointerEventData data)
	{
		if (!disabled)
		{
			if (EventManager.OnButtonClick != null)
			{
				EventManager.OnButtonClick(buttonID);
			}

		}


	}
	public virtual void OnPointerEnter(PointerEventData data)
	{
		if (!disabled)
		{
			if (GetComponent<Image>() != null && effect)
			{
				GetComponent<Image>().color = hoverColor;
			}
		} else
		{
			if (GetComponent<Image>() != null)
			{
				GetComponent<Image>().color = disabledColor;
			}
		}


	}
	public virtual void OnPointerExit(PointerEventData data)
	{

		if (!disabled)
		{
			if (GetComponent<Image>() != null && effect)
			{
				GetComponent<Image>().color = restColor;


			}
		} else
		{
			if (GetComponent<Image>() != null)
			{
				GetComponent<Image>().color = disabledColor;
			}
		}



	}



}


