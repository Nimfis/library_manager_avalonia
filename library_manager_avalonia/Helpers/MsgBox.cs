using MsBox.Avalonia;
using Avalonia.Controls;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace library_manager_avalonia.Helpers
{
    public class MsgBox
    {
        public static async Task SuccessAsync(string title, string message, Window owner = null)
        { 
            await ShowMessageBoxAsync(title, message, owner, Icon.Success);
        }

        public static async Task ErrorAsync(string title, string message, Window owner = null)
        {
            await ShowMessageBoxAsync(title, message, owner, Icon.Error);
        }

        public static async Task ShowMessageBoxAsync(string title, string message, Window owner = null, Icon icon = Icon.Info)
        {
            var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
            {
                ButtonDefinitions = ButtonEnum.Ok,
                ContentTitle = title,
                ContentMessage = message,
                Icon = icon,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                ShowInCenter = true
            });

            if (owner != null)
            {
                await messageBoxStandardWindow.ShowAsPopupAsync(owner);
            }
            else
            {
                await messageBoxStandardWindow.ShowAsync();
            }
        }
    }
}
