using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoginController : PageController {


	public SystemEnum.PageType previousPage;
	public InputField userName;
	public InputField password;
	public DialogHandler dialogHandler;

	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
	}

	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;
	}

	void Start () {

	}



	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{

		if (buttonID == SystemEnum.ButtonID.CustomerLogin)
		{
			if (isLoginSucessful(userName.text, password.text))
			{

				userName.text = password.text = "";
				if (EventManager.OnPageLoad != null)
				{
					EventManager.OnPageLoad(PageLoader.PreviousPage);
				}
			} else
			{
				dialogHandler.ShowDialog("Incorrect Username or password. Try again");
			}


		} else if (buttonID == SystemEnum.ButtonID.Header_Login)
		{
			previousPage = PageLoader.CurrentPage;
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(pageType);
			}
		}
		else if (buttonID == SystemEnum.ButtonID.Header_Logout)
		{
			SystemController.LoggedStudent = null;
			SystemController.IsLoggedIn = false;

			if (EventManager.OnLogout != null)
			{
				EventManager.OnLogout();
			}
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(SystemEnum.PageType.Home);
			}
		}




	}

	bool isLoginSucessful(string userName, string password)
	{
		for (int i = 0; i < SystemController.Students.Count; i++)
		{
			if (userName == SystemController.Students[i].userName && password == SystemController.Students[i].password)
			{
				SystemController.LoggedStudent = SystemController.Students[i];
				SystemController.IsLoggedIn = true;

				if (EventManager.OnLogin != null)
				{
					EventManager.OnLogin();
				}
				return  true;
			}
		}

		return false;
	}



}
