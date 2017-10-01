using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SimpleBudgetMvvmLight.PlatformServices;
using SimpleBudgetMvvmLight.iOS.PlatformServices;
using SimpleBudgetMvvmLight.Config;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace SimpleBudgetMvvmLight.iOS.PlatformServices
{
    public class FileService : IFileService
    {
        public string DbPath
        {
            get
            {
                var personalDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return Path.Combine(personalDir, Consts.DbName);
            }
        }
    }
}