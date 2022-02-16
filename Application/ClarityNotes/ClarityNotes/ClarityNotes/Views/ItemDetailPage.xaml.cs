using ClarityNotes.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ClarityNotes.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}