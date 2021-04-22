using MvvmCross.Commands;

namespace HomeMQ.Core.ViewModels
{
    public interface ISensorInfoViewModel
    {
        //IMvxCommand LoadFromConfigCommand { get; }
        string SerialNumber { get; set; }
        string Status { get; set; }
    }
}