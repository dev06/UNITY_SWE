using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DialogHandler : MonoBehaviour {

	private CanvasGroup canvasGroup;
	public Text diaText;

	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
		EventManager.OnPageLoad += OnPageLoad;
	}

	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;
		EventManager.OnPageLoad -= OnPageLoad;
	}

	void Start () {
		canvasGroup = GetComponent<CanvasGroup>();
		HideDialog();

	}

	void OnPageLoad(SystemEnum.PageType type)
	{
		HideDialog();
		SetText("");
	}

	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{
		if (buttonID == SystemEnum.ButtonID.DialogClose)
		{
			HideDialog();
		}
	}

	void HideDialog()
	{
		canvasGroup.alpha = 0;
		canvasGroup.blocksRaycasts = false;
	}


	public void ShowDialog()
	{
		canvasGroup.alpha = 1;
		canvasGroup.blocksRaycasts = true;
	}
	public void ShowDialog(string text)
	{
		canvasGroup.alpha = 1;
		canvasGroup.blocksRaycasts = true;
		SetText(text);
	}

	void SetText(string text)
	{
		diaText.text = text;
	}

	void Update () {

	}

	public bool showInEdit;
	void OnValidate()
	{
		if (canvasGroup == null) { canvasGroup = GetComponent<CanvasGroup>(); }
		if (showInEdit)
		{
			ShowDialog();
		} else
		{
			HideDialog();
		}
	}
}
