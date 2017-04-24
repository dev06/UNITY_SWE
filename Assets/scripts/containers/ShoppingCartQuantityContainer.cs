using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShoppingCartQuantityContainer : MonoBehaviour {

	public ShoppingCartController cartController;
	public Book book;
	public Text quantityText;
	public Text totalPriceText;
	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
	}



	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;
	}



	void Start () {
		cartController = FindObjectOfType<ShoppingCartController>();
	}


	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{

		if (book == null || SystemController.CurrentBookInCart == null) { return; }
		if (buttonID == SystemEnum.ButtonID.CartUpArrow)
		{
			if (SystemController.IsExactBook(SystemController.CurrentBookInCart, book))
			{
				if (book.Quantity < SystemController.GetStockByType(book))
				{
					book.Quantity++;

					cartController.GetBook(book).Quantity = book.Quantity;
					cartController.GetBook(book).TotalPrice = cartController.GetBook(book).Quantity * SystemController.GetUnitPriceByType(book);
					quantityText.text = cartController.GetBook(book).Quantity + "";
					totalPriceText.text = "$" + cartController.GetBook(book).TotalPrice + "";

				}
			}
		}

		if (buttonID == SystemEnum.ButtonID.CartDownArrow)
		{
			if (SystemController.IsExactBook(SystemController.CurrentBookInCart, book))
			{
				if (book.Quantity > 1)
				{
					book.Quantity--;
					cartController.GetBook(book).Quantity = book.Quantity;
					cartController.GetBook(book).TotalPrice = cartController.GetBook(book).Quantity * SystemController.GetUnitPriceByType(book);
					quantityText.text = cartController.GetBook(book).Quantity + "";
					totalPriceText.text = "$" + cartController.GetBook(book).TotalPrice + "";
				} else
				{
					cartController.RemoveBook(cartController.GetBook(book));
				}
			}

		}

		if (buttonID == SystemEnum.ButtonID.RemoveBook)
		{
			if (SystemController.IsExactBook(SystemController.CurrentBookInCart, book))
			{
				book.Quantity = 0;
				cartController.RemoveAllBooks(book);
			}
		}
		cartController.UpdateShoppingQuantityIcon();
	}
}
