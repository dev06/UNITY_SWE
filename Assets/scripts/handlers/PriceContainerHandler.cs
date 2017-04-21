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
	}

	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;
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
				quantityContainer.ResetQuantity();
				break;
			}
			case SystemEnum.ButtonID.UsedButton:
			{
				SystemController.CurrentBookTypeSelected = SystemEnum.BookType.Used;
				quantityContainer.ResetQuantity();

				break;
			}
			case SystemEnum.ButtonID.RentButton:
			{
				SystemController.CurrentBookTypeSelected = SystemEnum.BookType.Rental;
				quantityContainer.ResetQuantity();

				break;
			}
			case SystemEnum.ButtonID.EbookButton:
			{
				SystemController.CurrentBookTypeSelected = SystemEnum.BookType.Ebook;
				quantityContainer.ResetQuantity();
				break;
			}
		}

		UpdateButtonColor();

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
