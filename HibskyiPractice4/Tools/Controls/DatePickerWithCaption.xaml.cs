using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for DatePickerWithCaption.xaml
    /// </summary>
    public partial class DatePickerWithCaption : UserControl
    {
        public static readonly DependencyProperty DateProperty = DependencyProperty.Register
        (
            "Date",
            typeof(DateTime?),
            typeof(DatePickerWithCaption),
            new PropertyMetadata(null)
        );

        public DatePickerWithCaption()
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

        public DateTime? Date
        {
            get
            {
                return (DateTime?)GetValue(DateProperty);
            }
            set
            {
                SetValue(DateProperty, value);
            }
        }
    }
}
