namespace PhoneBook;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	string translatedNumber;
    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;
        translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);
        if (!string.IsNullOrWhiteSpace(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Call " + translatedNumber;
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
        }
    }
    
    async void OnCall(object sender, EventArgs e)
    {
        if (await this.DisplayAlert(
            "Dial a Number",
            "Would you like to call " + translatedNumber + "?",
            "Yes",
            "No"))
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(translatedNumber);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to open Dialer", "Invalid Phone Number", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Unable to Dial", "Could'nt open Dial", "Ok");
            }
        }
    
    }
}

