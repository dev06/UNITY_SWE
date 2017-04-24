using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OrderReceiptTemplate : MonoBehaviour {


	public Book book;
	public Text title;
	public Text formatText;
	public Text unitPriceText;
	public Text quantityText;
	public Text totalText;
	public Text additionalInfo;

	void Start () {

	}



	public void SetBook(Book b)
	{
		this.book = b;
		title.text = book.Title;
		formatText.text = book.bookType + "";
		quantityText.text = book.Quantity + "";
		unitPriceText.text = book.bookType ==  SystemEnum.BookType.New ? "$" + b.NewPrice + "" :
		                     book.bookType ==  SystemEnum.BookType.Used ?  "$" + b.UsedPrice + ""  :
		                     book.bookType ==  SystemEnum.BookType.Rental ? "$" +  b.RentPrice + "" :
		                     "$" + book.EbookPrice + "" ;
		totalText.text = "$" + book.TotalPrice + "";
		additionalInfo.text = book.additionalInfo;
	}
}
