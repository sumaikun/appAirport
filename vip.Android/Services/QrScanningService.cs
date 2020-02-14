using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;
using vip.Helpers;


[assembly: Dependency(typeof(vip.Droid.Services.QrScanningService))]

namespace vip.Droid.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };
            
            var scanResult = await scanner.Scan(optionsCustom);

            if (scanResult != null)
            {
                return scanResult.Text;
            }
            else{
                return null;
            }
           
        }
    }
}