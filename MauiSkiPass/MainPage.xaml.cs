using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System.Net;
using System.Timers;

namespace MauiSkiPass;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
		vr.MainP = this;
	}
	public void ShowTag(string Tag)
	{
		Label.Text = Tag;
	}

	private void OnGetBalanceClicked(object sender, EventArgs e)
	{
		try
		{
            Models.Request.GetBalanceIn GetB = new Models.Request.GetBalanceIn();
            GetB.authkey = Constants.Keys.authkey;
            GetB.key = Label.Text;
            string Req = Newtonsoft.Json.JsonConvert.SerializeObject(GetB);
            Models.Response.User Balance = new Models.Response.User();
			string Rsp = Methods.HttpPost.Send(String.Concat(Constants.URLs.Server, Constants.URLs.GetB), Req);
			if (!String.IsNullOrEmpty(Rsp))
			{
                Balance = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Response.User>(Rsp);
                DisplayAlert("Your Balace", Balance.userInfo.balance.ToString().ToString(), "OK");
                Bal.Text = Balance.userInfo.balance.ToString();
			}
		}catch(Exception ex)
		{

		}

		
	}

    private void OnGetServicesClicked(object sender, EventArgs e)
    {
        try
        {
            Page pg=new Page();
            pg.Layout(new Rect(0, 0, this.Width, this.Height));
            Models.Request.UserServicesReq GetB = new Models.Request.UserServicesReq();
            GetB.authkey = Constants.Keys.authkey;
            GetB.key = Label.Text;
            string Req = Newtonsoft.Json.JsonConvert.SerializeObject(GetB);
            Models.Response.UserServicesResp Balance = new Models.Response.UserServicesResp();
            string Rsp = Methods.HttpPost.Send(String.Concat(Constants.URLs.Server, Constants.URLs.GetSrv), Req);
            if (!String.IsNullOrEmpty(Rsp))
            {
                ListS.Header = "Services";
                Balance = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Response.UserServicesResp>(Rsp);
                ListS.ItemsSource = Balance.services.Select(X=>X.servName);
            }
        }
        catch (Exception ex)
        {

        }


    }

    private void OnRefillClicked(object sender, EventArgs e)
    {
		try
		{
            Models.Request.FillBalanceIn GetB = new Models.Request.FillBalanceIn();
            GetB.authkey = Constants.Keys.authkey;
            GetB.key = Label.Text;
			GetB.add_sum = decimal.TryParse(Sum.Text, out decimal x)?x:0;
            string Req = Newtonsoft.Json.JsonConvert.SerializeObject(GetB);
            Models.Response.GetBalanceOut Balance = new Models.Response.GetBalanceOut();
            string orderN=Methods.Rnd.RandomString(10);
            string SberRefillBaseURL = $"https://3dsec.sberbank.ru/payment/rest/register.do?userName=T502711359027-api&password=T502711359027&orderNumber={orderN}&amount={GetB.add_sum * 100}&returnUrl=http://176.107.244.8:49549/Refill?key={GetB.key}%26add_sum={GetB.add_sum}";
            string Rsp = Methods.HttpGet.Send(SberRefillBaseURL); //Methods.HttpPost.Send(String.Concat(Constants.URLs.Server, Constants.URLs.AddS), Req);
            if (!String.IsNullOrEmpty(Rsp))
            {
                
                Models.Response.SberResp Sber = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Response.SberResp>(Rsp);

                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };

                //WebView webvView = new WebView
                //{
                //    Source = Sber.formUrl
                //};
                //vr.MainView = this.Content;
                //this.Content = new WebView
                //{
                    
                //    Source = "https://www.google.com"//Sber.formUrl
                //};
                Launcher.OpenAsync(Sber.formUrl);

                //  this.Navigation.PushAsync(new Page() {  Window.Page.webvView });
                //  Balance = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Response.GetBalanceOut>(Rsp);
                //  Bal.Text = Balance.balance.ToString();
            }
        }
        catch(Exception ex)
		{

		}


    }

    //public delegate void TBC(object sender, EventArgs e);

    //TBC tbc = OnTrainingRefillClicked;

    private async void OnTrainingRefillClicked(object sender, EventArgs e)
    {
        try
        {
            Models.Request.AddServiceReq GetB = new Models.Request.AddServiceReq();
            GetB.authkey = Constants.Keys.authkey;
            GetB.key = Label.Text;
            GetB.amount = 1;
            GetB.categoryID = 196243;
          // GetB.add_sum = decimal.TryParse(Sum.Text, out decimal x) ? x : 0;
            string Req = Newtonsoft.Json.JsonConvert.SerializeObject(GetB);
            Models.Response.AddServiceResp Balance = new Models.Response.AddServiceResp();
            string Rsp = Methods.HttpPost.Send(String.Concat(Constants.URLs.Server, Constants.URLs.AddSrv), Req);
            if (!String.IsNullOrEmpty(Rsp))
            {
                Balance = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Response.AddServiceResp>(Rsp);
                Bal.Text = "Услуга Добавлена! ";
                DisplayAlert("Succcess", Bal.Text,"OK!");
       //         WC wc = WaitAndClean;
              //  aTimer.Interval= 1000;
                aTimer.Elapsed += Clear;
                aTimer.Enabled = true;
                aTimer.Start();
              //  Thread t = new Thread(new ThreadStart(wc));
            //    t.Start();
           //     t.Join();
               
                //       WaitAndClean();
            }
        }
        catch (Exception ex)
        {

        }

    }

    private static System.Timers.Timer aTimer=new System.Timers.Timer(1000);

    public delegate void WC();

    private async void WaitAndClean()
    {
        System.Threading.Thread.Sleep(10000);

//        this.Bal.Text= string.Empty; 
        return;

    }

    private void Clear(Object source, ElapsedEventArgs e)
    {
        Bal.Text = "";
    }

}

public static class vr
{
	public static string Tag = "";
	public static MainPage MainP;
    public static View MainView;
}

