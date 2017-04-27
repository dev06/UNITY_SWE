using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ShoppingCartController : PageController {

	public static float OrderTotal;
	public static float Subtotal;
	public static float Shipping;
	public static float SalesTax;
	public Text shoppingCartButtonText;
	public Text quantityToAddButtonText;
	public Text subTotalText;
	public Text shippingText;
	public Text taxText;
	public Text OrderTotalText;
	public Text emptyShoppingCartText;
	public Text shoppingCartTotalText;
	public GameObject CartTemplate;
	public Transform templateParent;


	public static List<Book> BooksInCart = new List<Book>();

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


	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{
		if (buttonID == SystemEnum.ButtonID.ShoppingCart)
		{
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(SystemEnum.PageType.ShoppingCart);
			}
			PopulateShoppingCart();
		}

		if (buttonID == SystemEnum.ButtonID.AddToCart)
		{
			if (SystemController.CurrentBook != null)
			{
				AddBookToCart(SystemController.CurrentBook, int.Parse(quantityToAddButtonText.text));
			}
		}

		if (buttonID == SystemEnum.ButtonID.ContinueShopping)
		{
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(SystemEnum.PageType.SearchResults);
			}
		}

		if (buttonID == SystemEnum.ButtonID.EmptyShoppingCart)
		{
			ClearShoppingCart();
			PopulateShoppingCart();
		}


	}

	void Update()
	{



	}

	void OnPageLoad(SystemEnum.PageType pageType)
	{
		if (pageType == this.pageType)
		{
			PopulateShoppingCart();
		}

	}



	public void AddBookToCart(Book book, int quantity)
	{
		if (quantity <= 0) { return; }

		if (!HasExactbook(book))
		{
			Book b = new Book();
			b.Course = new List<string>();
			b.Cover = book.Cover;
			b.Author = book.Author;
			b.NewPrice = book.NewPrice;
			b.UsedPrice = book.UsedPrice;
			b.EbookPrice = book.EbookPrice;
			b.RentPrice = book.RentPrice;
			b.Course.Add(book.Course[0]);
			b.Title = book.Title;
			b.Quantity = quantity;
			b.bookType = book.bookType;
			b.NewStock = book.NewStock;
			b.UsedStock = book.UsedStock;
			b.RentStock = book.RentStock;
			b.ISBN = book.ISBN;

			if (b.bookType == SystemEnum.BookType.New)
			{
				if (b.Quantity > b.NewStock)
				{
					b.Quantity = b.NewStock;
				}
			}

			if (b.bookType == SystemEnum.BookType.Used)
			{
				if (b.Quantity > b.UsedStock)
				{
					b.Quantity = b.UsedStock;
				}
			}

			if (b.bookType == SystemEnum.BookType.Rental)
			{
				if (b.Quantity > b.RentStock)
				{
					b.Quantity = b.RentStock;
				}
			}


			BooksInCart.Add(b);
		} else
		{

			if (GetBook(book).bookType == SystemEnum.BookType.New)
			{
				GetBook(book).Quantity += quantity;
				if (GetBook(book).Quantity > GetBook(book).NewStock)
				{
					GetBook(book).Quantity = GetBook(book).NewStock;
				}
			}

			if (GetBook(book).bookType == SystemEnum.BookType.Used)
			{
				GetBook(book).Quantity += quantity;
				if (GetBook(book).Quantity > GetBook(book).UsedStock)
				{
					GetBook(book).Quantity = GetBook(book).UsedStock;
				}
			}

			if (GetBook(book).bookType == SystemEnum.BookType.Rental)
			{
				GetBook(book).Quantity += quantity;
				if (GetBook(book).Quantity > GetBook(book).RentStock)
				{
					GetBook(book).Quantity = GetBook(book).RentStock;
				}
			}
		}

		UpdateShoppingQuantityIcon();

	}

	public bool HasExactbook(Book book)
	{
		for (int i = 0; i < BooksInCart.Count; i++)
		{
			if (BooksInCart[i].Title == book.Title && BooksInCart[i].bookType == book.bookType)
			{
				return true;
			}
		}

		return false;
	}

	public Book GetBook(Book book)
	{
		for (int i = 0; i < BooksInCart.Count; i++)
		{
			if (BooksInCart[i].Title == book.Title && BooksInCart[i].bookType == book.bookType)
			{
				return BooksInCart[i];
			}
		}
		return null;
	}



	public  int GetQuantity()
	{
		int count = 0;
		for (int i = 0; i < BooksInCart.Count; i++)
		{
			if (BooksInCart[i].Quantity > 1)
			{
				count += BooksInCart[i].Quantity;
			} else
			{
				count++;
			}
		}

		return count;
	}



	private void PopulateShoppingCart()
	{
		foreach (Transform t in templateParent)
		{
			Destroy(t.gameObject);
		}
		float offset = 1.2f;
		templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  0);

		for (int i = 0; i < BooksInCart.Count; i++)
		{
			GameObject clone = (GameObject)Instantiate(CartTemplate, Vector3.zero, Quaternion.identity) as GameObject;
			float height = clone.GetComponent<RectTransform>().rect.height;
			clone.transform.SetParent(templateParent);
			RectTransform rt = clone.GetComponent<RectTransform>();
			rt.offsetMin = new Vector3(0, 0);
			rt.offsetMax = new Vector3(0, height);
			rt.localScale = new Vector3(1, 1, 1);
			rt.anchoredPosition = new Vector3(0, -rt.rect.height * i * offset, 0);
			templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * (i + 1) * offset);
			clone.GetComponent<ShoppingCartTemplate>().SetBook(BooksInCart[i]);
			clone.GetComponent<ShoppingCartTemplate>().quantityContainer.book = BooksInCart[i];
		}

		UpdateInfoContainer();

	}

	public void RemoveBook(Book book)
	{
		BooksInCart.Remove(GetBook(book));
		PopulateShoppingCart();
		UpdateShoppingQuantityIcon();
	}

	public void UpdateShoppingQuantityIcon()
	{
		if (shoppingCartButtonText != null)
		{
			shoppingCartButtonText.text = GetQuantity() + "";
		}





		UpdateInfoContainer();
	}

	public void UpdateInfoContainer()
	{
		float subTotal = 0;
		float shipping = 0;
		float tax = 0;
		float orderTotal = 0;
		for (int i = 0 ; i < BooksInCart.Count; i++)
		{
			subTotal += SystemController.GetUnitPriceByType(BooksInCart[i]) * GetBook(BooksInCart[i]).Quantity;
		}



		if (HasBookType(SystemEnum.BookType.New) || HasBookType(SystemEnum.BookType.Used) || HasBookType(SystemEnum.BookType.Rental))
		{
			shipping = 14.99f;
		} else
		{
			shipping = 0;
		}


		float temp = (int)(((subTotal + shipping) * 0.07f) * 100.0f) / 100.0f;
		tax = temp;
		orderTotal = (int)(((subTotal + shipping + tax) * 100f)) / 100.0f;

		subTotalText.text = "$" + subTotal;
		shippingText.text = "$" + shipping;
		taxText.text = "$" + tax;
		OrderTotalText.text = "$" + orderTotal;
		OrderTotal = orderTotal;
		Subtotal = subTotal;
		Shipping = ((int)(shipping * 100.0f) / 100.0f);
		SalesTax = tax;
		emptyShoppingCartText.enabled = BooksInCart.Count == 0;

		shoppingCartTotalText.text = "$" + Subtotal;


	}

	public void RemoveAllBooks(Book book)
	{
		if (HasExactbook(book))
		{
			GetBook(book).Quantity = 0;
			RemoveBook(book);
		}
	}

	public bool HasBookType(SystemEnum.BookType type)
	{
		for (int i = 0; i < BooksInCart.Count; i++)
		{
			if (BooksInCart[i].bookType == type)
			{
				return true;
			}
		}

		return false;

	}

	public void RemoveBooksOfType(SystemEnum.BookType type)
	{
		for (int i = 0; i < BooksInCart.Count; i++)
		{
			if (BooksInCart[i].bookType == type)
			{
				GetBook(BooksInCart[i]).Quantity = 0;
				RemoveBook(BooksInCart[i]);
			}
		}
	}

	public bool IsCartEmpty()
	{
		return BooksInCart.Count <= 0;
	}


	public void ClearShoppingCart()
	{
		BooksInCart.Clear();
		UpdateShoppingQuantityIcon();
	}

	public void UpdateInventory(Book book)
	{

		for (int i = 0; i < SystemController.Library.Count; i++)
		{
			Book currentBook = SystemController.Library[i];

			if (currentBook.Title == book.Title && currentBook.bookType == book.bookType && currentBook.Course[0] == book.Course[0])
			{
				switch (book.bookType)
				{
					case SystemEnum.BookType.New:
					{
						SystemController.Library[i].NewStock -= book.Quantity;
						break;
					}

					case SystemEnum.BookType.Used:
					{
						SystemController.Library[i].UsedStock -= book.Quantity;
						break;
					}

					case SystemEnum.BookType.Rental:
					{
						SystemController.Library[i].RentStock -= book.Quantity;
						break;
					}
				}
			}
		}
	}

	public void UpdateFinancialAid(float orderTotal)
	{
		if (SystemController.LoggedStudent != null)
		{
			SystemController.LoggedStudent.aid -= orderTotal;
		}
	}
}
