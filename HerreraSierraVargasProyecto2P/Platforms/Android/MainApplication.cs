﻿using Android.App;
using Android.Runtime;
using HerreraSierraVargasProyecto2P;

namespace HerreraSierraVargasProyecto2P
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
