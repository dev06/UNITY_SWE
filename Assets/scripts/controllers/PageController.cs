using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PageController : MonoBehaviour {
	public bool showInEdit = true;

	public CanvasGroup group;

	public SystemEnum.PageType pageType;
	public static SystemEnum.PageType currentPage;

	public void Hide()
	{
		group.alpha = 0;
		group.blocksRaycasts = false;
	}
	public void Show()
	{
		group.alpha = 1;
		group.blocksRaycasts = true;
	}


	void OnValidate()
	{
		if (group == null)
		{
			group = GetComponent<CanvasGroup>();
		}
		if (showInEdit)
		{
			Show();
		} else
		{
			Hide();
		}
	}

}


