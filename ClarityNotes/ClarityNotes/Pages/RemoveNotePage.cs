using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class RemoveNotePage : ContentView
    {

        private User user;

        public RemoveNotePage(User user)
        {
            this.user = user;
        }
    }
}