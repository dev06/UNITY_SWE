using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PriceContainerHandler : MonoBehaviour {

	public Image NewButton, UsedButton, RentButton, EbookButton;
	public QuantityContainer quantityContainer;
	public Color selectedColor;

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

	void Update()
	{
		UpdateButtonColor();

	}

	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{

		switch (buttonID)
		{

			case SystemEnum.ButtonID.NewButton:
			{
				SystemController.CurrentBookTypeSelected = SystemEnum.BookType.New;
				SystemController.CurrentBook.bookType = SystemController.CurrentBookTypeSelected;

				quantityContainer.ResetQuantity();

				break;
			}
			case SystemEnum.ButtonID.UsedButton:
			{
				SystemController.CurrentBookTypeSelected = SystemEnum.BookType.Used;
				SystemController.CurrentBook.bookType = SystemController.CurrentBookTypeSelected;

				quantityContainer.ResetQuantity();

				break;
			}
			case SystemEnum.ButtonID.RentButton:
			{
				SystemController.CurrentBookTypeSelected = SystemEnum.BookType.Rental;
				SystemController.CurrentBook.bookType = SystemController.CurrentBookTypeSelected;

				quantityContainer.ResetQuantity();

				break;
			}
			case SystemEnum.ButtonID.EbookButton:
			{
				SystemController.CurrentBookTypeSelected = SystemEnum.BookType.Ebook;
				SystemController.CurrentBook.bookType = SystemController.CurrentBookTypeSelected;

				quantityContainer.ResetQuantity();
				break;
			}


		}

		UpdateButtonColor();

	}

	void OnPageLoad(SystemEnum.PageType page)
	{
		if (page != SystemEnum.PageType.BookDescription)
		{
			quantityContainer.ResetQuantity();
		}
	}

	void UpdateButtonColor()
	{
		EbookButton.color = Color.white;
		UsedButton.color = Color.white;
		RentButton.color = Color.white;
		NewButton.color = Color.white;;
		switch (SystemController.CurrentBookTypeSelected)
		{
			case SystemEnum.BookType.New:
			{
				NewButton.color = selectedColor;
				break;
			}
			case SystemEnum.BookType.Used:
			{
				UsedButton.color = selectedColor;
				break;
			}
			case SystemEnum.BookType.Rental:
			{
				RentButton.color = selectedColor;
				break;
			}
			case SystemEnum.BookType.Ebook:
			{
				EbookButton.color = selectedColor;
				break;
			}

		}
	}
}
