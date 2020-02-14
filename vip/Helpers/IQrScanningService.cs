using System;
using System.Threading.Tasks;

namespace vip.Helpers
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
