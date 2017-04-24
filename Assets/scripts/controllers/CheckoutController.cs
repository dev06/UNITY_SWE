﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CheckoutController : PageController
{

	public CanvasGroup billingAddRessMask;
	public CanvasGroup loginRentalPrompt;
	public CanvasGroup cartEmptyPrompt;
	public Toggle toggle;

	public ShoppingCartController cartController;
	public List<CanvasGroup> Steps = new List<CanvasGroup>();
	public static int CurrentStep = 0;


	public InputField shippingEmail, shippingName, shippingAddressOne, shippingAddressTwo, shippingCity, shippingZipcode;
	public Dropdown shippingState;

	public InputField billingName, billingAddressOne, billingAddressTwo, billingCity, billingZipcode;
	public Dropdown billingState;


	public CheckoutPaymentHandler paymentHandler;
	public OrderSummaryHandler  orderSummaryHandler;
	public OrderReceiptHandler orderReceiptHandler;
	public AddressInfo shippingAddressInfo;
	public AddressInfo billingAddressInfo;



	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
		EventManager.OnPageLoad += OnPageLoad;
		EventManager.OnLogout += OnLogout;
	}

	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;
		EventManager.OnPageLoad -= OnPageLoad;
		EventManager.OnLogout -= OnLogout;
	}

	void Start ()
	{
		InitializeDropDowns();
		UpdateStepPanel();
		SetPrompt(loginRentalPrompt, false);
		SetPrompt(cartEmptyPrompt, false);
		SetPrompt(billingAddRessMask, toggle.isOn);
	}


	void InitializeDropDowns()
	{
		for (int i = 0; i < SystemEnum.States.Length; i++)
		{
			shippingState.options.Add(new Dropdown.OptionData() { text = SystemEnum.States[i]});
			billingState.options.Add(new Dropdown.OptionData() { text = SystemEnum.States[i]});
		}
	}


	void UpdateStepPanel()
	{
		foreach (CanvasGroup grp in Steps)
		{
			SetGroupActive(grp, false);
		}

		if (CurrentStep > 0)
		{
			SetGroupActive(Steps[CurrentStep - 1], true);
		}
	}


	void OnPageLoad(SystemEnum.PageType type)
	{
		SetPrompt(loginRentalPrompt, false);
		SetPrompt(cartEmptyPrompt, false);
		SetPrompt(billingAddRessMask, false);
		if (type == SystemEnum.PageType.Login) { return; }
		if (type != pageType)
		{
			CurrentStep = 0;
			UpdateStepPanel();
		}
	}

	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{
		if (buttonID == SystemEnum.ButtonID.CheckoutNext)
		{
			if (CurrentStep == 1)
			{
				RecordShippingInfo();
				if (HaveAllShippingInfo())
				{
					CurrentStep++;
					UpdateStepPanel();
				}
			} else if (CurrentStep == 2)
			{

				if (!toggle.isOn)
				{

					if (HaveAllBillingInfo())
					{
						CurrentStep++;
						UpdateStepPanel();
					}
					RecordBillingInfo(false);

				} else
				{
					RecordBillingInfo(true);
					CurrentStep++;
					UpdateStepPanel();
				}


				paymentHandler.SetAddress();

			} else if (CurrentStep == 3)
			{
				if (paymentHandler.IsProcessingInfoSuccessfull())
				{
					CurrentStep++;
					UpdateStepPanel();
					orderSummaryHandler.PopulateOrderSummary();
				}
			} else if (CurrentStep == 4)
			{

				CurrentStep++;
				UpdateStepPanel();
				orderReceiptHandler.PopulateOrderReceipts();

				cartController.ClearShoppingCart();
				ClearCheckoutInformation();
				paymentHandler.ClearPaymentInformation();

			}
		}

		if (buttonID == SystemEnum.ButtonID.CheckoutBack)
		{
			ManageCheckoutBackButton();
		}

		if (buttonID == SystemEnum.ButtonID.Checkout)
		{
			if (cartController.IsCartEmpty())
			{
				SetPrompt(cartEmptyPrompt, true);
			} else
			{
				if (HasRentals() && !SystemController.IsLoggedIn)
				{
					SetPrompt(loginRentalPrompt, true);
				} else
				{
					if (EventManager.OnPageLoad != null)
					{
						EventManager.OnPageLoad(pageType);
					}
					if (CurrentStep < 1)
					{
						CurrentStep++;
						UpdateStepPanel();
					}
				}
			}


		}


		if (buttonID == SystemEnum.ButtonID.LoginRentalPrompt_Login)
		{
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(SystemEnum.PageType.Login);
			}
			SetPrompt(loginRentalPrompt, false);
		} else if ( buttonID == SystemEnum.ButtonID.LoginRentalPrompt_RemoveRental)
		{
			cartController.RemoveBooksOfType(SystemEnum.BookType.Rental);
			SetPrompt(loginRentalPrompt, false);

		} else if (buttonID == SystemEnum.ButtonID.LoginRentalPrompt_CloseDialog)
		{
			SetPrompt(loginRentalPrompt, false);
			SetPrompt(cartEmptyPrompt, false);

		}
	}

	void SetGroupActive(CanvasGroup grp, bool active)
	{
		grp.alpha = active ? 1 : 0;
		grp.blocksRaycasts = active;
	}


	void RecordShippingInfo()
	{

		shippingAddressInfo.name = shippingName.text;
		shippingAddressInfo.addressOne = shippingAddressOne.text;
		shippingAddressInfo.addressTwo = shippingAddressTwo.text;
		shippingAddressInfo.city = shippingCity.text;
		shippingAddressInfo.state = shippingState.captionText.text;
		shippingAddressInfo.zipcode = shippingZipcode.text;
	}

	void RecordBillingInfo(bool sameAsShipping)
	{
		if (!sameAsShipping)
		{
			billingAddressInfo.name = billingName.text;
			billingAddressInfo.addressOne = billingAddressOne.text;
			billingAddressInfo.addressTwo = billingAddressTwo.text;
			billingAddressInfo.city = billingCity.text;
			billingAddressInfo.state = billingState.captionText.text;
			billingAddressInfo.zipcode = billingZipcode.text;
		} else
		{
			billingAddressInfo.name = shippingAddressInfo.name;
			billingAddressInfo.addressOne = shippingAddressInfo.addressOne;
			billingAddressInfo.addressTwo = shippingAddressInfo.addressTwo;
			billingAddressInfo.city = shippingAddressInfo.city;
			billingAddressInfo.state = shippingAddressInfo.state;
			billingAddressInfo.zipcode = shippingAddressInfo.zipcode;
		}

	}

	bool HaveAllShippingInfo()
	{
		if (shippingName.text.Length <= 0) { return false; }
		if (shippingAddressOne.text.Length <= 0) { return false; }
		if (shippingCity.text.Length <= 0) { return false; }
		if (shippingState.value == 0) { return false; }
		if (shippingName.text.Length <= 0) { return false; }
		if (shippingZipcode.text.Length < 5) { return false; }
		return true;
	}
	bool HaveAllBillingInfo()
	{
		if (billingName.text.Length <= 0) { return false; }
		if (billingAddressOne.text.Length <= 0) { return false; }
		if (billingCity.text.Length <= 0) { return false; }
		if (billingState.value == 0) { return false; }
		if (billingName.text.Length <= 0) { return false; }
		if (billingZipcode.text.Length < 5) { return false; }

		return true;
	}



	private bool HasRentals() {
		for (int i = 0; i < ShoppingCartController.BooksInCart.Count; i++)
		{
			if (ShoppingCartController.BooksInCart[i].bookType ==  SystemEnum.BookType.Rental)
			{
				return true;
			}
		}

		return false;
	}

	public void SetPrompt(CanvasGroup group, bool active)
	{
		group.alpha = active ? 1 : 0;
		group.blocksRaycasts = active;
	}


	public void UpdateBillingAddressMask()
	{
		SetPrompt(billingAddRessMask, toggle.isOn);
		if (toggle.isOn)
		{
			billingName.text = shippingName.text;
			billingAddressOne.text = shippingAddressOne.text;
			billingAddressTwo.text = shippingAddressTwo.text;
			billingCity.text = shippingCity.text;
			billingZipcode.text = shippingZipcode.text;
			billingState.value = shippingState.value;
		}
	}


	public void ClearCheckoutInformation()
	{
		billingName.text = "";
		billingAddressOne.text = "";
		billingAddressTwo.text = "";

		billingCity.text = "";
		billingZipcode.text = "";
		billingState.value = 0;

		shippingName.text = "";
		shippingAddressOne.text = "";
		shippingAddressTwo.text = "";
		shippingCity.text = "";
		shippingZipcode.text = "";
		shippingState.value = 0;
		shippingEmail.text = "";
		toggle.isOn = false;
	}


	private void ManageCheckoutBackButton()
	{
		if (CurrentStep == 1)
		{
			CurrentStep--;
			UpdateStepPanel();
			if (EventManager.OnPageLoad != null)
			{
				EventManager.OnPageLoad(SystemEnum.PageType.ShoppingCart);
			}
		} else
		{
			CurrentStep--;
			UpdateStepPanel();
		}



	}

	private void OnLogout()
	{
		ClearCheckoutInformation();
	}









}