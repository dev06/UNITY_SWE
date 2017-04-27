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

	}
	void Start () {

	}


	void Update ()
	{
		if (SystemController.CurrentBook == null) { return; }
		template.SetBook(SystemController.CurrentBook);
	}
}
