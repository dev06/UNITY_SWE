using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BookDescTemplate : MonoBehaviour {

	public Book book;
	public Image cover;
	public Text title;
	public Text author;
	public Text isbn;
	public Text description;
	public Text newBookText;
	public Text usedBookText;
	public Text rentBookText;
	public Text ebookText;

	void Start () {

	}


	public void SetBook(Book book)
	{
		title.text = book.Title;
		author.text = book.Author;
		isbn.text = book.ISBN;
		description.text = book.Description;
		newBookText.text = "New \n$" + book.NewPrice + "\nx" + book.NewStock;
		usedBookText.text = "Used \n$" + book.UsedPrice + "\nx" + book.UsedStock;
		rentBookText.text = "Rent \n$" + book.RentPrice + "\nx" + book.RentStock;
		ebookText.text = "Ebook \n$" + book.EbookPrice + "\n x999999";

		cover.sprite = book.Cover;
	}
}



