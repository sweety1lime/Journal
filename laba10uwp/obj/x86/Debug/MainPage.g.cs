﻿#pragma checksum "C:\Нужные папки\Программирование\BrainDead\laba10uwp\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0357341F75D4EE02E0E79E32BA717E23"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrainDead
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // MainPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // MainPage.xaml line 13
                {
                    this.gridbg = (global::Windows.UI.Xaml.Media.ImageBrush)(target);
                }
                break;
            case 3: // MainPage.xaml line 34
                {
                    this.LoginGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // MainPage.xaml line 42
                {
                    this.RegGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5: // MainPage.xaml line 76
                {
                    this.RegLogButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RegLogButton).Click += this.RegLogButton_Click;
                }
                break;
            case 6: // MainPage.xaml line 55
                {
                    this.RegTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // MainPage.xaml line 56
                {
                    this.RegPasswordbx = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 8: // MainPage.xaml line 57
                {
                    this.RegNameBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // MainPage.xaml line 58
                {
                    this.RegSurNameBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10: // MainPage.xaml line 59
                {
                    this.RegLastNameBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11: // MainPage.xaml line 60
                {
                    this.Group = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 12: // MainPage.xaml line 61
                {
                    this.subjects = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.subjects).SelectionChanged += this.subjects_SelectionChanged;
                }
                break;
            case 13: // MainPage.xaml line 62
                {
                    this.RegTypeBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.RegTypeBox).SelectionChanged += this.RegTypeBox_SelectionChanged_1;
                }
                break;
            case 14: // MainPage.xaml line 67
                {
                    this.Kurses = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 15: // MainPage.xaml line 68
                {
                    this.Kurs1 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.Kurs1).Checked += this.Kurs1_Checked;
                }
                break;
            case 16: // MainPage.xaml line 69
                {
                    this.Kurs2 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.Kurs2).Checked += this.Kurs2_Checked;
                }
                break;
            case 17: // MainPage.xaml line 70
                {
                    this.Kurs3 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.Kurs3).Checked += this.Kurs3_Checked;
                }
                break;
            case 18: // MainPage.xaml line 71
                {
                    this.Kurs4 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.Kurs4).Checked += this.Kurs4_Checked;
                }
                break;
            case 19: // MainPage.xaml line 39
                {
                    this.LoginTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 20: // MainPage.xaml line 40
                {
                    this.Passwordbx = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 21: // MainPage.xaml line 30
                {
                    global::Windows.UI.Xaml.Controls.Button element21 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element21).Click += this.Button_Click;
                }
                break;
            case 22: // MainPage.xaml line 31
                {
                    global::Windows.UI.Xaml.Controls.Button element22 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element22).Click += this.Button_Click_1;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
