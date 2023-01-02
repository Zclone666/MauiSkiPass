using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
//using Poz1.NFCForms.Droid;
//using Poz1.NFCForms.Abstract;
using Android.Webkit;
using Microsoft.Maui.Controls.Xaml;
using Plugin.NFC;
using System.Text;
//namespace Xamarin;
//using Android.OS;
//using Android.Nfc;

namespace MauiSkiPass;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[IntentFilter(new[] { NfcAdapter.ActionTagDiscovered, NfcAdapter.ActionNdefDiscovered })]
[MetaData(NfcAdapter.ActionTechDiscovered, Resource = "@xml/nfc")]
public class MainActivity : MauiAppCompatActivity
{
    public static string nfc_tag;

    public NfcAdapter NFCdevice;
    //public NfcForms x;
    protected override void OnCreate(Bundle savedInstanceState)
    {
      //  MauiProgram.CreateMauiApp();
        base.OnCreate(savedInstanceState);

        NfcManager NfcManager = (NfcManager)Android.App.Application.Context.GetSystemService(Context.NfcService);
        NFCdevice = NfcManager.DefaultAdapter;
        //this.NFCdevice.
        //Xamarin.Forms.DependencyService.Register<INFC, NfcForms>();
      //  DependencyService.Register<NfcManager>();
        //x = Xamarin.Forms.DependencyService.Get<INfcForms>() as NfcForms;
        //LoadApplication(new App());
    }
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
        //  Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }


    protected override void OnResume()
    {
        base.OnResume();
        if (NFCdevice != null)
        {

            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            NFCdevice.EnableForegroundDispatch(this, pendingIntent, null, null);
            //NFCdevice.EnableForegroundDispatch
            //(
            //	this,
            //	PendingIntent.GetActivity(this, 0, intent, 0),
            //	new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
            //	new String[][] {new string[] {
            //			NFCTechs.Ndef,
            //		},
            //		new string[] {
            //			NFCTechs.MifareClassic,
            //		},
            //	}
            //);
        }
    }

    protected override void OnPause()
    {
        base.OnPause();
        //	NFCdevice.DisableForegroundDispatch(this);
    }

    protected override void OnNewIntent(Intent intent)
    {
        //this.OnCreate(null);
     //   this.Intent = intent;
        ////this.intent.Identifier
        base.OnNewIntent(intent);

        //x.OnNewIntent(this, intent);

        var droidTag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
        var nfcTag = droidTag.GetId();
        //List<string> nfcTag = new List<string>();
        string nfc_tag = Plugin.NFC.NFCUtils.ByteArrayToHexString(nfcTag);

        base.OnNewIntent(intent);

        System.Diagnostics.Debug.WriteLine("NEW INTENT");

        if (intent.Extras.IsEmpty)
        {
            System.Diagnostics.Debug.WriteLine("empty");
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Not empty");
        }

        //For start reading
        if (intent.Action == NfcAdapter.ActionTagDiscovered || intent.Action == NfcAdapter.ActionNdefDiscovered || intent.Action == NfcAdapter.ActionAdapterStateChanged
            || intent.Action == NfcAdapter.ActionTransactionDetected || intent.Action == NfcAdapter.ExtraNdefMessages || intent.Action == NfcAdapter.ExtraNdefMessages)
        {
            System.Diagnostics.Debug.WriteLine("DISCOVERD");
            var tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
            if (tag != null)
            {
                System.Diagnostics.Debug.WriteLine("TAG");
                // First get all the NdefMessage
                var rawMessages = intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);

                if (rawMessages != null)
                {
                    var msg = (NdefMessage)rawMessages[0];
                    System.Diagnostics.Debug.WriteLine("MESSAGE");
                    // Get NdefRecord which contains the actual data
                    var record = msg.GetRecords()[0];
                    if (record != null)
                    {
                        if (record.Tnf == NdefRecord.TnfWellKnown)
                        {
                            // Get the transfered data
                            var data = Encoding.ASCII.GetString(record.GetPayload());
                            System.Diagnostics.Debug.WriteLine("RECORD");
                        }
                    }
                }
            }
        }

        global::MauiSkiPass.MainActivity.nfc_tag = String.IsNullOrEmpty(nfc_tag) ? "" : nfc_tag.Substring(0, 8);
        vr.Tag = nfc_tag;
        vr.MainP.Label.Text = nfc_tag.Substring(0,8);
       // MauiApp Ma = MauiProgram.CreateMauiApp();
       // Ma
       // MainPage mp=new MainPage();
       // mp.Label.Text = nfc_tag;
       // mp.IsVisible = true;
       //// mp.LoadFromXaml<MainPage>(typeof(MainPage));
       //mp.S
       // mp.IsEnabled = true;
       // mp.ShowTag(nfc_tag);
        //ResourceSet ResSet = new ResourceSet(@"..\TestNFC_SkiPassApp\Resources\SkPass.resx");
        //ResourceManager.CreateFileBasedResourceManager("SkPass.resx", @"..\TestNFC_SkiPassApp\Resources\", ResSet.GetDefaultReader());
        //ResSet.
        //using (System.Resources.ResourceWriter resx = new ResourceWriter(@"..\TestNFC_SkiPassApp\Resources\SkPass.resx"))
        //{
        //	resx.AddResource("numb", global::TestNFC_SkiPassApp.Droid.MainActivity.nfc_tag);
        //}
        //	System.Resources.ResourceWriter RW = new System.Resources.ResourceWriter("SkiPassN  umb.resx");
        //System.Resources.ResourceSet RS = new System.Resources.ResourceSet(new System.Resources.ResourceReader(@"../TestNFC_SkiPassApp/Resources/SkPass.resx"));
        //RS.GetDefaultWriter().GetProperty("numb").SetValue(this, global::TestNFC_SkiPassApp.Droid.MainActivity.nfc_tag);

        //Views.AboutPage.tst Del = this.ContentScene.Class.
        //Views.AboutPage.Del();

        //TestNFC_SkiPassApp.ViewModels.AboutViewModel vm = new ViewModels.AboutViewModel();
        //vm.Title = TestNFC_SkiPassApp.ViewModels.AboutViewModel.nmb;

    }

    //[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    //[IntentFilter(new[] { NfcAdapter.ActionTagDiscovered, NfcAdapter.ActionNdefDiscovered })]
    //[MetaData(NfcAdapter.ActionTechDiscovered, Resource = "@xml/nfc")]
    //public class NFCScanActivity : MainActivity
    //{
    //}
}
