using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SearchResultController : PageController {

	public SearchContainer searchContainer;

	public GameObject searchResultTemplate;

	public Transform templateParent;

	public Text matchesFoundText;
	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
		//	EventManager.OnPageLoad += OnPageLoad;
	}



	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;
		//EventManager.OnPageLoad -= OnPageLoad;
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

		if (buttonID == SystemEnum.ButtonID.BackArrow)
		{
			if (EventManager.OnPageLoad != null) {
				EventManager.OnPageLoad(PageLoader.PreviousPage);
			}
		}

	}

	void OnPageLoad(SystemEnum.PageType type)
	{
		if (type == this.pageType)
		{

			PopulateSearchResults();
		}

	}

	void PopulateSearchResults()
	{
		for (int i = 0; i < templateParent.childCount; i++)
		{
			Destroy(templateParent.GetChild(i).gameObject);
		}

		bool foundMatches = false;
		int index = 0;
		float offset = 1.1f;



		if (searchContainer.searchType == SystemEnum.SearchType.Course)
		{
			for (int i = 0; i < SystemController.Library.Count; i++)
			{
				Book currentBook = SystemController.Library[i];

				for (int j = 0; j < currentBook.Course.Count; j++)
				{
					if (currentBook.Course[j] == searchContainer.courseSelected)
					{
						GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
						float height = clone.GetComponent<RectTransform>().rect.height;

						clone.transform.SetParent(templateParent);
						RectTransform rt = clone.GetComponent<RectTransform>();
						rt.offsetMin = new Vector2(0, 0);
						rt.offsetMax = new Vector2(0, height);
						rt.localScale = new Vector3(1, 1, 1);
						rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * offset, 0);

						index++;
						templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * offset);
						clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];
						foundMatches = true;
					}
				}
			}
		} else if (searchContainer.searchType == SystemEnum.SearchType.Professor)
		{
			for (int i = 0; i < SystemController.Library.Count; i++)
			{
				Book currentBook = SystemController.Library[i];
				for (int j = 0; j < currentBook.Professor.Count; j++)
				{
					if (currentBook.Professor[j] == searchContainer.professorSelected)
					{
						GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
						float height = clone.GetComponent<RectTransform>().rect.height;
						clone.transform.SetParent(templateParent);
						RectTransform rt = clone.GetComponent<RectTransform>();
						rt.offsetMin = new Vector2(0, 0);
						rt.offsetMax = new Vector2(0, height);
						rt.localScale = new Vector3(1, 1, 1);
						rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * offset, 0);
						index++;
						templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * offset);
						clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];
						foundMatches = true;
					}
				}
			}
		} else if (searchContainer.searchType == SystemEnum.SearchType.Keyword)
		{
			// for (int i = 0; i < SystemController.Library.Count; i++)
			// {
			// 	Book currentBook = SystemController.Library[i];

			// 	if (currentBook.Title.ToLower().Contains(searchContainer.keyword) ||
			// 	        currentBook.Author.ToLower().Contains(searchContainer.keyword) ||
			// 	        currentBook.ISBN.Contains(searchContainer.keyword))
			// 	{
			// 		DisplayBook(currentBook, i );
			// 		foundMatches = true;
			// 	}
			// }
		}


		for (int i = 0; i < SystemController.Library.Count; i++)
		{
			if (searchContainer.searchType == SystemEnum.SearchType.Keyword)

			{
				Book currentBook = SystemController.Library[i];

				if (currentBook.Title.ToLower().Contains(searchContainer.keyword) ||
				        currentBook.Author.ToLower().Contains(searchContainer.keyword) ||
				        currentBook.ISBN.Contains(searchContainer.keyword))
				{

					GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
					float height = clone.GetComponent<RectTransform>().rect.height;

					clone.transform.SetParent(templateParent);
					RectTransform rt = clone.GetComponent<RectTransform>();
					rt.offsetMin = new Vector2(0, 0);
					rt.offsetMax = new Vector2(0, height);
					rt.localScale = new Vector3(1, 1, 1);
					rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * 1.1f, 0);

					index++;
					templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * 1.1f);
					clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];
					foundMatches = true;
				}
			}

		}


		for (int i = 0; i < SystemController.Library.Count; i++)
		{




			// if (SystemController.Library[i].Professor[0] == searchContainer.professorSelected && searchContainer.searchType == SystemEnum.SearchType.Professor)
			// {

			// 	GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
			// 	float height = clone.GetComponent<RectTransform>().rect.height;
			// 	clone.transform.SetParent(templateParent);
			// 	RectTransform rt = clone.GetComponent<RectTransform>();
			// 	rt.offsetMin = new Vector2(0, 0);
			// 	rt.offsetMax = new Vector2(0, height);
			// 	rt.localScale = new Vector3(1, 1, 1);
			// 	rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * offset, 0);
			// 	index++;
			// 	templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * offset);
			// 	clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];
			// 	foundMatches = true;
			// } else if (SystemController.Library[i].Course[0] == searchContainer.courseSelected && searchContainer.searchType == SystemEnum.SearchType.Course)
			// {
			// 	GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
			// 	float height = clone.GetComponent<RectTransform>().rect.height;

			// 	clone.transform.SetParent(templateParent);
			// 	RectTransform rt = clone.GetComponent<RectTransform>();
			// 	rt.offsetMin = new Vector2(0, 0);
			// 	rt.offsetMax = new Vector2(0, height);
			// 	rt.localScale = new Vector3(1, 1, 1);
			// 	rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * offset, 0);

			// 	index++;
			// 	templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * offset);
			// 	clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];
			// 	foundMatches = true;

			// } else
			// {
			// 	if (searchContainer.searchType == SystemEnum.SearchType.Keyword)
			// 	{
			// 		if (SystemController.Library[i].Course[0].ToLower().Contains(searchContainer.keyword) ||
			// 		        SystemController.Library[i].Author.ToLower().Contains(searchContainer.keyword) ||
			// 		        SystemController.Library[i].Title.ToLower().Contains(searchContainer.keyword))
			// 		{
			// 			GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
			// 			float height = clone.GetComponent<RectTransform>().rect.height;

			// 			clone.transform.SetParent(templateParent);
			// 			RectTransform rt = clone.GetComponent<RectTransform>();
			// 			rt.offsetMin = new Vector2(0, 0);
			// 			rt.offsetMax = new Vector2(0, height);
			// 			rt.localScale = new Vector3(1, 1, 1);
			// 			rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * offset, 0);

			// 			index++;
			// 			templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * offset);
			// 			clone.GetComponent<SearchResultTemplate>().book = SystemController.Library[i];
			// 			foundMatches = true;

			// 		}
			// 	}
			// }
		}


		matchesFoundText.enabled = !foundMatches;
	}


	void DisplayBook(Book book, int index)
	{
		GameObject clone = (GameObject)Instantiate(searchResultTemplate, Vector3.zero, Quaternion.identity) as GameObject;
		float height = clone.GetComponent<RectTransform>().rect.height;

		clone.transform.SetParent(templateParent);
		RectTransform rt = clone.GetComponent<RectTransform>();
		rt.offsetMin = new Vector2(0, 0);
		rt.offsetMax = new Vector2(0, height);
		rt.localScale = new Vector3(1, 1, 1);
		rt.anchoredPosition = new Vector3(0 , -rt.rect.height * index * 1.1f, 0);

		index++;
		templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * index * 1.1f);
		clone.GetComponent<SearchResultTemplate>().book = book;

	}
}
