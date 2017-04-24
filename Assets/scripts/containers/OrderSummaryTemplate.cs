using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OrderSummaryTemplate : MonoBehaviour {


	public Book book;
	public Image cover;
	public Text title;
	public Text author;

	public Text formatText;
	public Text quantityText;
	public Text unitPriceText;
	public Text totalPriceText;



	void Start()
	{

	}



	public void SetBook(Book b)
	{
		this.book = b;
		cover.sprite = book.Cover;
		title.text = book.Title;
		author.text = book.Author;
		formatText.text = book.bookType + "";
		quantityText.text = book.Quantity + "";
		unitPriceText.text = book.bookType ==  SystemEnum.BookType.New ? "$" + b.NewPrice + "" :
		                     book.bookType ==  SystemEnum.BookType.Used ?  "$" + b.UsedPrice + ""  :
		                     book.bookType ==  SystemEnum.BookType.Rental ? "$" +  b.RentPrice + "" :
		                     "$" + book.EbookPrice + "" ;
		totalPriceText.text = "$" + book.TotalPrice + "";
	}
}
