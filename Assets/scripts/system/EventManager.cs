using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void ButtonClick(SystemEnum.ButtonID buttonID);
	public static ButtonClick OnButtonClick;

	public delegate void System();
	public static System OnLogin;
	public static System OnLogout;


	public delegate void Page(SystemEnum.PageType page);
	public static Page OnPageLoad;
}
