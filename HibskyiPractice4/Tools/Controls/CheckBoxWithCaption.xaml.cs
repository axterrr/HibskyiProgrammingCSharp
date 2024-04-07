using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Tools.Controls
{
    /// <summary>
    /// Interaction logic for CheckBoxWithCaption.xaml
    /// </summary>
    public partial class CheckBoxWithCaption : UserControl
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register
        (
            "IsChecked",
            typeof(bool?),
            typeof(CheckBoxWithCaption),
            new PropertyMetadata(null)
        );

        public CheckBoxWithCaption()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get
            {
                return TbCaption.Text;
            }
            set
            {
                TbCaption.Text = value;
            }
        }

        public bool? IsChecked
        {
            get
            {
                return (bool?) GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }
    }
}
