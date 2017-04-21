using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class PageLoader : MonoBehaviour {

	public List<PageController> Pages = new List<PageController>();
	public SystemEnum.PageType CurrentPage;
	void OnEnable()
	{
		EventManager.OnPageLoad += OnPageLoad;
	}

	void OnDisable()
	{
		EventManager.OnPageLoad -= OnPageLoad;
	}


	void Start () {

	}


	void OnPageLoad(SystemEnum.PageType page)
	{
		for (int i = 0 ; i < Pages.Count; i++)
		{
			if (Pages[i].pageType == page)
			{
				Show(Pages[i].transform.GetComponent<CanvasGroup>());
				CurrentPage = page;
			} else
			{
				Hide(Pages[i].transform.GetComponent<CanvasGroup>());
			}
		}
	}

	public void Hide(CanvasGroup group)
	{
		group.alpha = 0;
		group.blocksRaycasts = false;
	}
	public void Show(CanvasGroup group)
	{
		group.alpha = 1;
		group.blocksRaycasts = true;
	}



}
