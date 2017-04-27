using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HomeController : PageController {



	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
	}
	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;
	}

	void Start () {
		group = GetComponent<CanvasGroup>();
	}

	void Update () {

	}

	void OnButtonClick(SystemEnum.ButtonID buttonID) {
		if (buttonID == SystemEnum.ButtonID.Search)
		{
			Hide();
		}

		if (buttonID == SystemEnum.ButtonID.ContactUs)
		{
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(SystemEnum.PageType.ContactUs);
			}

		}

		if (buttonID == SystemEnum.ButtonID.AboutUs)
		{
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(SystemEnum.PageType.AboutUs);
			}

		}

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
