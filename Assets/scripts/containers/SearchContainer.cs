using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SearchContainer : MonoBehaviour {

	public Dropdown searchBy;
	public Dropdown contentDropdown;
	public InputField searchTextField;

	public SystemEnum.SearchType searchType;
	public string courseSelected;
	public string professorSelected;
	public string keyword;


	void Start ()
	{
		UpdateDropdowns();

		SetContentDropDownContents();


	}


	void Update () {
	}


	public void OnSearchByValueChanged()
	{
		switch (searchBy.captionText.text)
		{
			case "Keyword":
				searchType = SystemEnum.SearchType.Keyword; break;
			case "Professor":
				searchType = SystemEnum.SearchType.Professor; break;
			case "Course":
				searchType = SystemEnum.SearchType.Course; break;
			default:
				searchType = SystemEnum.SearchType.Keyword; break;
		}

		UpdateDropdowns();
		SetContentDropDownContents();
	}

	public void OnContentValueChange()
	{
		if (searchType == SystemEnum.SearchType.Professor)
		{
			professorSelected = contentDropdown.captionText.text;
			contentDropdown.captionText.text = professorSelected;
		} else
		{
			if (searchType == SystemEnum.SearchType.Course)
			{
				courseSelected = contentDropdown.captionText.text;
				contentDropdown.captionText.text = courseSelected;

			}
		}
	}

	public void OnInputFieldValueChange()
	{
		keyword = searchTextField.text;
		if (EventManager.OnButtonClick != null)
		{
			EventManager.OnButtonClick(SystemEnum.ButtonID.Search);
		}


	}




	private void UpdateDropdowns()
	{
		switch (searchType)
		{
			case SystemEnum.SearchType.Keyword:
			{
				SetActive(contentDropdown.GetComponent<CanvasGroup>(), false);
				SetActive(searchTextField.GetComponent<CanvasGroup>(), true);
				break;
			}
			case SystemEnum.SearchType.Course:
			{
				SetActive(contentDropdown.GetComponent<CanvasGroup>(), true);
				SetActive(searchTextField.GetComponent<CanvasGroup>(), false);
				break;
			}

			case SystemEnum.SearchType.Professor:
			{

				SetActive(contentDropdown.GetComponent<CanvasGroup>(), true);
				SetActive(searchTextField.GetComponent<CanvasGroup>(), false);

				break;
			}
		}


	}

	private void SetActive(CanvasGroup group, bool active)
	{
		group.alpha = active ? 1 : 0;
		group.blocksRaycasts  = active;
	}


	private void SetContentDropDownContents()
	{
		contentDropdown.options.Clear();
		for (int i = 0 ; i < SystemController.Library.Count; i++)
		{

			if (searchType == SystemEnum.SearchType.Professor)
			{
				if (i == 0)
				{
					contentDropdown.options.Add(new Dropdown.OptionData() { text = "Choose a professor"});
					contentDropdown.captionText.text = "Choose a professor";
				}


				contentDropdown.options.Add(new Dropdown.OptionData() { text = SystemController.Library[i].Professor});



			} else
			{
				if (i == 0)
				{
					contentDropdown.options.Add(new Dropdown.OptionData() { text = "Choose a course"});
				}
				contentDropdown.options.Add(new Dropdown.OptionData() { text = SystemController.Library[i].Course});

			}
		}
	}
}
