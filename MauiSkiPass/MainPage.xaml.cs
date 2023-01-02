using System.Net;

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
				Bal.Text = Balance.userInfo.balance.ToString();
			}
		}catch(Exception ex)
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
            string Rsp = Methods.HttpPost.Send(String.Concat(Constants.URLs.Server, Constants.URLs.AddS), Req);
            if (!String.IsNullOrEmpty(Rsp))
            {
                Balance = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Response.GetBalanceOut>(Rsp);
                Bal.Text = Balance.balance.ToString();
            }
        }
        catch(Exception ex)
		{

		}


    }

    private void OnTrainingRefillClicked(object sender, EventArgs e)
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
            }
        }
        catch (Exception ex)
        {

        }


    }

}

public static class vr
{
	public static string Tag = "";
	public static MainPage MainP;
}

