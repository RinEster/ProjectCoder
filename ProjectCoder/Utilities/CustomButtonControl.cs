using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectCoder.Utilities
{
    public class CustomButtonControl : RadioButton
    {
        static CustomButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButtonControl), new FrameworkPropertyMetadata(typeof(CustomButtonControl)));
        }        
    }
}
