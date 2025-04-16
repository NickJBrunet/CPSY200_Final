using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EquipmentRental.Data;

public class ModalService
{
    public event Func<string, string, string, Task> OnShow;

    public Task ShowMessage(string title, string message, string titleColor = "black")
    {
        return OnShow?.Invoke(title, message, titleColor) ?? Task.CompletedTask;
    }
}



