using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CheckoutPaymentHandler : MonoBehaviour {



	public List<CanvasGroup> PaymentContainers = new List<CanvasGroup>();
	public static SystemEnum.PaymentType selectedPaymentType;
	public Dropdown paymentTypeDropDown;
	public CheckoutController checkoutController;
	public Text shippingAddressText;
	public Text billingAddressText;
	public CanvasGroup loginFAPrompt;


	private bool hasSufficentAmount;
	private int previousValue;


	public InputField cardNumber, cardName, securityCode, payPalUserName, payPalPassword, studentEmail, studentPassword;
	public Dropdown cardExpMonth, cardExpYear;



	void OnEnable()
	{
		EventManager.OnPageLoad += OnPageLoad;
		EventManager.OnButtonClick += OnButtonClick;
		EventManager.OnLogout += OnLogout;
	}
	void OnDisable()
	{
		EventManager.OnPageLoad -= OnPageLoad;
		EventManager.OnButtonClick -= OnButtonClick;
		EventManager.OnLogout -= OnLogout;
	}

	void OnPageLoad(SystemEnum.PageType type)
	{
		if (type != SystemEnum.PageType.Checkout)
		{
			SetGroupActive(loginFAPrompt, false);
		}
		paymentTypeDropDown.value = previousValue = 0;
	}

	void FixedUpdate()
	{
		SetFinancialAidText();
	}

	void Start () {
		UpdateSelectedPayment();
		PopulateCreditCardDates();
	}



	public void OnPaymentDropDownChange()
	{

		if (paymentTypeDropDown.value == 2 && !SystemController.IsLoggedIn)
		{
			SetGroupActive(loginFAPrompt, true);
			paymentTypeDropDown.value = previousValue;

		} else
		{

			if (!hasSufficentAmount && paymentTypeDropDown.value == 2)
			{
				paymentTypeDropDown.value = previousValue;
				UpdateSelectedPayment();
			} else
			{
				UpdateSelectedPayment();
				previousValue = paymentTypeDropDown.value;
			}
		}

	}

	public void OnButtonClick(SystemEnum.ButtonID buttonID)
	{
		if (buttonID == SystemEnum.ButtonID.LoginRentalPrompt_CloseDialog && PageLoader.CurrentPage == SystemEnum.PageType.Checkout)
		{
			SetGroupActive(loginFAPrompt, false);
			paymentTypeDropDown.value = previousValue;
		}
	}

	private void UpdateSelectedPayment()
	{
		switch (paymentTypeDropDown.value)
		{

			case 0:
			{

				selectedPaymentType = SystemEnum.PaymentType.PayPal;
				break;
			}
			case 1:
			{

				selectedPaymentType = SystemEnum.PaymentType.CreditCard;
				break;
			}
			case 2:
			{

				selectedPaymentType = SystemEnum.PaymentType.FinancialAid;
				break;
			}

		}
		UpdatePaymentUI();
	}

	private void UpdatePaymentUI()
	{
		foreach (CanvasGroup grp in PaymentContainers)
		{
			SetGroupActive(grp, false);
		}

		switch (selectedPaymentType)
		{
			case SystemEnum.PaymentType.CreditCard:
			{
				SetGroupActive(PaymentContainers[0], true);
				break;
			}
			case SystemEnum.PaymentType.PayPal:
			{
				SetGroupActive(PaymentContainers[1], true);
				break;
			}

			case SystemEnum.PaymentType.FinancialAid:
			{
				SetGroupActive(PaymentContainers[2], true);
				break;
			}
		}
	}

	private void SetGroupActive(CanvasGroup grp, bool active)
	{
		grp.alpha = active ? 1 : 0;
		grp.blocksRaycasts = active;
	}

	public void SetAddress()
	{

		shippingAddressText.text = checkoutController.shippingAddressInfo.name + "\n" +
		                           checkoutController.shippingAddressInfo.addressOne + "\n" +
		                           checkoutController.shippingAddressInfo.city + ", " +
		                           checkoutController.shippingAddressInfo.state + ", " +
		                           checkoutController.shippingAddressInfo.zipcode + "," + checkoutController.shippingAddressInfo.country ;


		billingAddressText.text = checkoutController.billingAddressInfo.name + "\n" +
		                          checkoutController.billingAddressInfo.addressOne + "\n" +
		                          checkoutController.billingAddressInfo.city + ", " +
		                          checkoutController.billingAddressInfo.state + ", " +
		                          checkoutController.billingAddressInfo.zipcode + "," + checkoutController.billingAddressInfo.country;
	}


	public void SetFinancialAidText()
	{
		if (SystemController.LoggedStudent != null && SystemController.IsLoggedIn)
		{
			hasSufficentAmount = ShoppingCartController.OrderTotal <= SystemController.LoggedStudent.aid;

			if (!hasSufficentAmount)
			{
				paymentTypeDropDown.options[2].text = "Financial Aid [Insufficient Amounts]" + " $" + SystemController.LoggedStudent.aid;
			} else
			{
				paymentTypeDropDown.options[2].text = "Financial Aid" + " [$" + SystemController.LoggedStudent.aid + "]";
			}


			studentEmail.text = SystemController.LoggedStudent.userName + "@students.kennesaw.edu";
			studentPassword.text = SystemController.LoggedStudent.password;
		} else
		{
			studentEmail.text = studentPassword.text =  "";
		}
	}

	private void PopulateCreditCardDates()
	{
		for (int i = 1; i <= 12; i++)
		{
			cardExpMonth.options.Add(new Dropdown.OptionData() { text = i + ""});
		}

		int currentYear = int.Parse(System.DateTime.Now.Year.ToString());
		for (int i = currentYear; i <= currentYear + 50; i++)
		{
			cardExpYear.options.Add(new Dropdown.OptionData() { text = i + ""});
		}
	}


	public bool IsProcessingInfoSuccessfull()
	{
		if (selectedPaymentType == SystemEnum.PaymentType.CreditCard)
		{
			if (cardNumber.text.Length < 16) { return false; }
			if (cardName.text.Length <= 0) { return false; }
			if (securityCode.text.Length <= 0) { return false; }
			if (cardExpMonth.value == 0 || cardExpYear.value == 0) { return false; }
		} else if (selectedPaymentType == SystemEnum.PaymentType.PayPal)
		{
			if (payPalUserName.text.Length <= 0) { return false; }
			if (payPalPassword.text.Length <= 0) { return false; }
		} else if (selectedPaymentType == SystemEnum.PaymentType.FinancialAid)
		{
			if (!hasSufficentAmount) { return false; }
		}

		return true;
	}

	public void  ClearPaymentInformation()
	{
		cardNumber.text =  cardName.text =  securityCode.text =  payPalUserName.text =  payPalPassword.text =  studentEmail.text =  studentPassword.text = "";
		cardExpMonth.value =  cardExpYear.value = 0;
	}

	private void OnLogout()
	{
		ClearPaymentInformation();
	}
}
