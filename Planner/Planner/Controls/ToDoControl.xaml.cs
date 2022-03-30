﻿using System;
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

namespace Planner.Controls
{
    /// <summary>
    /// Interaction logic for ToDoControl.xaml
    /// </summary>
    public partial class ToDoControl : UserControl
    {
        public ToDoControl()
        {
            InitializeComponent();

            //CheckboxList.ItemsSource = new List<string>() { "Bla", "Bla", "bla bla" };
            CheckboxList.ItemsSource = Enumerable.Range(0, 100).Select(i=>i.ToString()).ToList();
        }
    }
}