using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SystemController : MonoBehaviour {

	//SEARCH CONTROLLER
	public static List<Book> Library = new List<Book>();

	public static List<Student> Students = new List<Student>();

	public static List<string>Course = new List<string>();
	public static List<string>Professor = new List<string>();

	public static bool IsLoggedIn;
	public static Student LoggedStudent;
	public static Book CurrentBook;
	public static SystemEnum.BookType CurrentBookTypeSelected;
	public static Book CurrentBookInCart;



	void Start()
	{

		for (int i = 0; i < Library.Count; i++)
		{
			Book currentBook = Library[i];
			for (int j = 0; j < currentBook.Course.Count; j++)
			{
				if (Course.Contains(currentBook.Course[j]))
				{
					continue;
				}


				Course.Add(currentBook.Course[j]);
			}
		}


		for (int i = 0; i < Library.Count; i++)
		{
			Book currentBook = Library[i];
			for (int j = 0; j < currentBook.Professor.Count; j++)
			{
				if (Professor.Contains(currentBook.Professor[j]))
				{
					continue;
				}


				Professor.Add(currentBook.Professor[j]);
			}
		}
	}


	public static bool IsExactBook(Book book_one, Book book_two)
	{
		return (book_one.Title == book_two.Title) && (book_one.bookType == book_two.bookType);
	}

	public static float GetUnitPriceByType(Book book)
	{
		switch (book.bookType)
		{
			case SystemEnum.BookType.New: return book.NewPrice;
			case SystemEnum.BookType.Used: return book.UsedPrice;
			case SystemEnum.BookType.Rental: return book.RentPrice;
			case SystemEnum.BookType.Ebook: return book.EbookPrice;
		}

		return -1;
	}

	public static int GetStockByType(Book book)
	{
		switch (book.bookType)
		{
			case SystemEnum.BookType.New: return book.NewStock;
			case SystemEnum.BookType.Used: return book.UsedStock;
			case SystemEnum.BookType.Rental: return book.RentStock;
			case SystemEnum.BookType.Ebook: return 999999;
		}

		return -1;

	}
	public static float GetShippingCostByType(Book book)
	{
		return book.bookType == SystemEnum.BookType.Ebook ? 0 : 14.99f;
	}



	// void Update()
	// {
	// 	if (CurrentBookInCart != null) {
	// 		Debug.Log(CurrentBookInCart.Title + " " + CurrentBookInCart.bookType);
	// 	}
	// }

}
