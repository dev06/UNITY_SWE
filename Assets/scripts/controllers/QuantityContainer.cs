using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QuantityContainer : MonoBehaviour {

	public Text quantityText;

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

		if (buttonID !=  SystemEnum.ButtonID.UpArrow && buttonID != SystemEnum.ButtonID.DownArrow)
		{
			return;
		}

		Book book = SystemController.CurrentBook;

		SystemEnum.BookType currentBookType = SystemController.CurrentBookTypeSelected;
		if (buttonID == SystemEnum.ButtonID.UpArrow)
		{


			if (currentBookType == SystemEnum.BookType.New)
			{
				if (book.Quantity < book.NewStock)
				{
					book.Quantity++;
				} else
				{
					book.Quantity = book.NewStock;
				}
			} else	if (currentBookType == SystemEnum.BookType.Used)
			{
				if (book.Quantity < book.UsedStock)
				{
					book.Quantity++;
				} else
				{
					book.Quantity = book.UsedStock;
				}
			} else	if (currentBookType == SystemEnum.BookType.Rental)
			{
				if (book.Quantity < book.RentStock)
				{
					book.Quantity++;
				} else
				{
					book.Quantity = book.RentStock;
				}
			} else if (currentBookType == SystemEnum.BookType.Ebook)
			{
				book.Quantity++;
			}

		}
		if (buttonID == SystemEnum.ButtonID.DownArrow)
		{

			if (book.Quantity > 0)
			{
				book.Quantity--;
			}
		}
		quantityText.text = book.Quantity + "";

	}


	public void ResetQuantity()
	{
		if (SystemController.CurrentBook == null) return;
		SystemController.CurrentBook.Quantity = 0;
		quantityText.text = SystemController.CurrentBook.Quantity + "";

	}


}
