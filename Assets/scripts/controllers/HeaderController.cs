using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HeaderController : MonoBehaviour {

	public GameObject LoginButton;
	public Text StudentText;


	void OnEnable()
	{
		EventManager.OnLogin += OnLogin;

		EventManager.OnLogout += OnLogout;
		EventManager.OnPageLoad += OnPageLoad;
		EventManager.OnButtonClick += OnButtonClick;

	}



	void OnDisable()
	{
		EventManager.OnLogin -= OnLogin;

		EventManager.OnLogout -= OnLogout;
		EventManager.OnPageLoad -= OnPageLoad;
		EventManager.OnButtonClick -= OnButtonClick;

	}



	void OnLogin()
	{
		if (SystemController.IsLoggedIn && SystemController.LoggedStudent != null)
		{
			StudentText.text =  "Hello, " + SystemController.LoggedStudent.firstName;
			LoginButton.GetComponent<ButtonEventHandler>().buttonID = SystemEnum.ButtonID.Header_Logout;
			LoginButton.transform.GetComponentInChildren<Text>().text = "Logout";
			SetButtonActive(LoginButton.GetComponent<CanvasGroup>(), true);
		}
	}

	void OnLogout()
	{
		StudentText.text = "";
		LoginButton.GetComponent<ButtonEventHandler>().buttonID = SystemEnum.ButtonID.Header_Login;
		LoginButton.transform.GetComponentInChildren<Text>().text = "Login";
	}

	void OnPageLoad(SystemEnum.PageType page)
	{
		if (page == SystemEnum.PageType.Login)
		{
			SetButtonActive(LoginButton.GetComponent<CanvasGroup>(), false);
		} else
		{
			SetButtonActive(LoginButton.GetComponent<CanvasGroup>(), true);
		}
	}

	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{
		if (buttonID == SystemEnum.ButtonID.Header_Home)
		{
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(SystemEnum.PageType.Home);
			}
		}

		if (buttonID == SystemEnum.ButtonID.CloseApp)
		{
			Application.Quit();
		}

	}

	void SetButtonActive(CanvasGroup group, bool active)
	{
		group.alpha = active ? 1 : 0;
		group.blocksRaycasts = active;
	}
}
