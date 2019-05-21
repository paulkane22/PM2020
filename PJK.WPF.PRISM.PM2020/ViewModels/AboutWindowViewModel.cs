using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PJK.WPF.PRISM.PM2020.ViewModels
{
    public class AboutWindowViewModel : BindableBase
    {
        public DelegateCommand<Window> CloseWindowCommand { get; private set; }


        public AboutWindowViewModel()
        {
            CloseWindowCommand = new DelegateCommand<Window>(OnCloseAbout);
            Message = "Haemophilia Clinical Information Service by MDSAS";
        }


        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }


        private void OnCloseAbout(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
