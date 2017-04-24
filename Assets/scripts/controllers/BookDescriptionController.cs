using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BookDescriptionController : PageController {

	public BookDescTemplate template;
	public GameObject BookDescTemplatePrefab;
	public Transform templateParent;
	void OnEnable()
	{
		EventManager.OnPageLoad += OnPageLoad;

	}

	void OnDisable()
	{
		EventManager.OnPageLoad -= OnPageLoad;
	}

	void OnPageLoad(SystemEnum.PageType type)
	{
		// if (type == this.pageType)
		// {
		// 	GameObject clone = (GameObject)Instantiate(BookDescTemplatePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		// 	float height = clone.GetComponent<RectTransform>().rect.height;
		// 	clone.transform.SetParent(templateParent);
		// 	RectTransform rt = clone.GetComponent<RectTransform>();
		// 	rt.offsetMin = new Vector3(0, 0);
		// 	rt.offsetMax = new Vector3(0, height);
		// 	rt.localScale = new Vector3(1, 1, 1);
		// 	rt.anchoredPosition = new Vector3(0, 0, 0);
		// 	templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height);
		// 	clone.GetComponent<BookDescTemplate>().SetBook(SystemController.CurrentBook);
		// }
	}
	void Start () {

	}


	void Update ()
	{
		if (SystemController.CurrentBook == null) { return; }
		template.SetBook(SystemController.CurrentBook);
	}


}
