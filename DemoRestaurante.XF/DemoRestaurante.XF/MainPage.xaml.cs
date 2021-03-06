﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DemoRestaurante.XF
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnBtnStack_Clicked(object sender, EventArgs e)
        {
            await  Navigation.PushAsync(new Pantalla1());
        }

        private async void OnBtnGrid_Clicked(object sender, EventArgs e)
        {
            await  Navigation.PushAsync(new Pantalla2());
        }
    }
}
