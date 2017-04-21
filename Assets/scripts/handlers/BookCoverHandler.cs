﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class BookCoverHandler : MonoBehaviour, IPointerClickHandler {

	public SearchResultTemplate template;
	public virtual void OnPointerClick(PointerEventData data)
	{
		SystemController.CurrentBook = template.book;

		if (EventManager.OnPageLoad != null)
		{
			EventManager.OnPageLoad(SystemEnum.PageType.BookDescription);
		}
	}
}
