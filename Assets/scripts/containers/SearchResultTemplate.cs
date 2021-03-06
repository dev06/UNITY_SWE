﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SearchResultTemplate : MonoBehaviour {


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



	void Start ()
	{
		if (book != null)
		{
			title.text = book.Title;
			author.text = book.Author;
			isbn.text = book.ISBN;
			description.text = book.Description;
			newBookText.text = "New \n $" + book.NewPrice + "\n x" + book.NewStock;
			usedBookText.text = "Used \n $" + book.UsedPrice + "\n x" + book.UsedStock;
			rentBookText.text = "Rent \n $" + book.RentPrice + "\n x"  + book.RentStock;
			ebookText.text = "Ebook \n $" + book.EbookPrice + "\n x999999";
			courseText.text = "Course " + book.Course[0];
			sectionText.text = "Section " + book.SectionNumber[0];
			cover.sprite = book.Cover;
		}
	}
}
