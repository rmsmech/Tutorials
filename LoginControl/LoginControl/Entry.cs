using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haley.IOC;
using LoginControl.Controls;


namespace LoginControl
{
    public static class Entry
    {
        public static void Initialize(IContainerFactory containerFactory)
        {
            //Base registrations
            if (containerFactory.Services is IBaseContainer bsContainer)
            {
                bsContainer.Register<AuthenticationViewModel>(); //resolve this.
            }

            //UIRgistrations'
            containerFactory.Controls.Register<AuthenticationViewModel, LoginPage>(ViewEnums.LoginPage);
            containerFactory.Controls.Register<AuthenticationViewModel, LoginHelperPage>(ViewEnums.HelperPage);
            containerFactory.Controls.Register<AuthenticationViewModel, SignupPage>(ViewEnums.SingUpPage);

            //WindowRegistrations
            containerFactory.Windows.Register<AuthenticationViewModel, Authenticationwindow>("authmainWindow");
        }
    }
}
