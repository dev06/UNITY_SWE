using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SearchResultController : PageController {

	public SearchContainer searchContainer;

	public GameObject searchResultTemplate;

	public Transform templateParent;
	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
	}



	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;
	}



	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{
		if (buttonID == SystemEnum.ButtonID.Search)
		{
			PopulateSearchResults();
			if (EventManager.OnPageLoad != null) {
				EventManager.OnPageLoad(pageType);
			}
		}
	}

	void PopulateSearchResults()
	{
		for (int i = 0; i < templateParent.childCount; i++)
		{
			Destroy(templateParent.GetChild(i).gameObject);
		}


		int index = 0;
		float offset = 1.1f;
		for (int i = 0; i < SystemController.Library.Count; i++)
		{

			if (SystemController.Library[i].Professor == searchContainer.professorSelected && searchContainer.searchType == SystemEnum.SearchType.Professor)
			{

				GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
				float height = clone.GetComponent<RectTransform>().rect.height;
				clone.transform.SetParent(templateParent);
				RectTransform rt = clone.GetComponent<RectTransform>();
				rt.offsetMin = new Vector2(0, 0);
				rt.offsetMax = new Vector2(0, height);
				rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * offset, 0);
				rt.localScale = new Vector3(1, 1, 1);
				index++;
				templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * offset);
				clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];

			} else if (SystemController.Library[i].Course == searchContainer.courseSelected && searchContainer.searchType == SystemEnum.SearchType.Course)
			{
				GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
				float height = clone.GetComponent<RectTransform>().rect.height;

				clone.transform.SetParent(templateParent);
				RectTransform rt = clone.GetComponent<RectTransform>();
				rt.offsetMin = new Vector2(0, 0);
				rt.offsetMax = new Vector2(0, height);
				rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * offset, 0);
				rt.localScale = new Vector3(1, 1, 1);

				index++;
				templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * offset);
				clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];
			} else
			{
				if (searchContainer.searchType == SystemEnum.SearchType.Keyword)
				{
					if (SystemController.Library[i].Course.ToLower().Contains(searchContainer.keyword) ||
					        SystemController.Library[i].Author.ToLower().Contains(searchContainer.keyword) ||
					        SystemController.Library[i].Title.ToLower().Contains(searchContainer.keyword))
					{
						GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
						float height = clone.GetComponent<RectTransform>().rect.height;

						clone.transform.SetParent(templateParent);
						RectTransform rt = clone.GetComponent<RectTransform>();
						rt.offsetMin = new Vector2(0, 0);
						rt.offsetMax = new Vector2(0, height);
						rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * offset, 0);
						rt.localScale = new Vector3(1, 1, 1);

						index++;
						templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * offset);
						clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];
					}
				}
			}
		}
	}
}
