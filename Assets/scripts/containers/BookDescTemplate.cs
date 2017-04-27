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
	public Text courseText;
	public Text sectionText;
	public Text importanceText;

	void Start () {

	}


	public void SetBook(Book book)
	{
		title.text = book.Title;
		author.text = "by: " + book.Author;
		isbn.text = "ISBN: " + book.ISBN;
		description.text = book.Description;
		newBookText.text = "New \n$" + book.NewPrice + "\nx" + book.NewStock;
		usedBookText.text = "Used \n$" + book.UsedPrice + "\nx" + book.UsedStock;
		rentBookText.text = "Rent \n$" + book.RentPrice + "\nx" + book.RentStock;
		ebookText.text = "Ebook \n$" + book.EbookPrice + "\n x999999";
		courseText.text = "Course: " + book.Course[0];
		sectionText.text = "Section: " + book.SectionNumber[0];
		importanceText.text = "This book is " + book.Importance + " in " + book.Course[0] + " with " + book.Professor[0]  + ".";
		cover.sprite = book.Cover;

	}
}



