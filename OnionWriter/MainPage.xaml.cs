using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Microsoft.UI.Xaml.Controls;

namespace Test
{

    public partial class Page : Windows.UI.Xaml.Controls.Page
    {
        public Page()
        {
            InitializeComponent();
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
        }

        private void TabView_AddTabButtonClick(Microsoft.UI.Xaml.Controls.TabView sender, object args)
        {
            int number = 1;
            var tabItems = sender.TabItems.ToArray();
            foreach (TabViewItem i in tabItems)
            {

                int n = int.Parse(new string(i.Header.ToString().Where(c => char.IsNumber(c)).ToArray()));
                if(n > number)
                {
                    number = n;
                }
                
            }
            number ++;
            (sender as TabView).TabItems.Add(CreateNewTab(number));
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