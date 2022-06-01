<<<<<<< HEAD

﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.XForms.RichTextEditor;


namespace ClarityNotes
{
    public class EditorPage : ContentPage
    {
        Note note;
        User user;
        SfRichTextEditor editor;


        public EditorPage(Note note, User user)
        {
            this.note = note;
            this.user = user;

            Button traduce = new Button();
            traduce.BackgroundColor = Color.Transparent;
            traduce.HeightRequest = 50;
            traduce.WidthRequest = 70;
            traduce.Text = "Traduire";
            traduce.Clicked += OnTraduceCliked;

            editor = new SfRichTextEditor();
            editor.AutoSize = AutoSizeOption.TextChanges;
            editor.VerticalOptions = LayoutOptions.CenterAndExpand;
            editor.HtmlText = note.Content;
            editor.HeightRequest = 1000;
            editor.PlaceHolder = "Votre note";
            if (editor.ToolbarItems.Count == 1)
                editor.ToolbarItems.Add(traduce);
            else
                editor.ToolbarItems[1] = traduce;

            editor.TextChanged += OnTextChanged;

            StackLayout stack = new StackLayout();
            stack.VerticalOptions = LayoutOptions.Start;
            stack.Children.Add(editor);
            this.Content = stack;
        }

        public void OnTextChanged(object sender, Syncfusion.XForms.RichTextEditor.TextChangedEventArgs e)
        {
            note.Update(editor.HtmlText, user);
            string text = e.Text;
        }

        public void OnTraduceCliked(object sender, EventArgs e)
        {
            this.editor.Text += "traduire a marché";
        }
    }
}