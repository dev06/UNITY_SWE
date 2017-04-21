using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BookDescriptionController : PageController {

	public BookDescTemplate template;
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if (SystemController.CurrentBook == null) { return; }
		template.SetBook(SystemController.CurrentBook);
	}
}
