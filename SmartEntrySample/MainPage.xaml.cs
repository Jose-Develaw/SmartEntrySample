using SmartEntrySample.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartEntrySample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //MyCustomControl.Behaviors.Add(new MyCustomValidator("My custom error message"));
        }
    }
}
