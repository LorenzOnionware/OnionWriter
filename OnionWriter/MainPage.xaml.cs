using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage;
using Windows.UI.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml.Documents;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Xml.Linq;
using System.Text;
using System.Xml;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using Windows.UI.Xaml.Printing;
using Windows.Graphics.Printing;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Helpers;
using Microsoft.Win32;
using Windows.ApplicationModel.Activation;
using Microsoft.UI.Xaml.Controls;

namespace Test
{

    public partial class Page : Windows.UI.Xaml.Controls.Page
    {
        public int Counter;

        public Page()
        {
            InitializeComponent();
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
        }

        private void TabView_AddTabButtonClick(Microsoft.UI.Xaml.Controls.TabView sender, object args)
        {

            Counter ++;
            (sender as TabView).TabItems.Add(CreateNewTab(Counter));
        }

        private async void TabView_TabCloseRequested(Microsoft.UI.Xaml.Controls.TabView sender, Microsoft.UI.Xaml.Controls.TabViewTabCloseRequestedEventArgs args)
        {

            var Frame = args.Tab.Content as Frame;
            var Page = Frame.Content as BlankPage2;
            bool isEmpty = Page.TxtboxIsEmpty();
            if (isEmpty == true)
            {
                sender.TabItems.Remove(args.Tab);
            }
            else
            {

                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Do you want to save your file?";
                ((ContentControl)contentDialog).Content = "Do you want to save the changes?";
                contentDialog.PrimaryButtonText = "Save";
                contentDialog.SecondaryButtonText = "Delete";
                contentDialog.CloseButtonText = "Cancel";
                ContentDialog newFileDialog = contentDialog;
                ContentDialogResult result = await newFileDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                    await Page.SaveFileClickTask();
                else if (result == ContentDialogResult.Secondary)
                {
                    sender.TabItems.Remove(args.Tab);
                }
                else
                {
                    return;
                }
            }

            sender.TabItems.Remove(args.Tab);
            var summeoftabs = Tabviev1.TabItems.Count;
            if (summeoftabs == 0)
            {
                Application.Current.Exit();
            }
        }

        private void TabView_Loaded(object sender, RoutedEventArgs e)
        {
            Counter ++;
            (sender as TabView).TabItems.Add(CreateNewTab(1));
        }

        private TabViewItem CreateNewTab(int index)
        {
            TabViewItem newItem = new TabViewItem();

            newItem.Header = $"Document {index}";
            newItem.IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document };

            var Frame = new Frame();
            Frame.NavigateToType(typeof(BlankPage2), null, new FrameNavigationOptions());
            newItem.Content = Frame;
            return newItem;
        }
    }

}