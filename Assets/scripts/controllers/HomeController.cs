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
