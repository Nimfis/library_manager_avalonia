using Avalonia.Controls;
using Avalonia.ReactiveUI;
using library_manager_avalonia.Core.Enums;
using library_manager_avalonia.ViewModels;

namespace library_manager_avalonia.Core.Windows
{
    public class ReactiveWindowBase<T> : ReactiveWindow<T> where T : ViewModelBase
    {
        public WindowResult Result { get; protected set; }

        public ReactiveWindowBase()
        {
            Result = WindowResult.None;
        }

        protected void SetResultAndClose(WindowResult result)
        {
            Result = result;
            Close();
        }

        protected override void OnClosing(WindowClosingEventArgs e)
        {
            if (Result == WindowResult.None)
            {
                Result = WindowResult.Closed;
            }

            base.OnClosing(e);
        }
    }
}
