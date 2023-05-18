using Logic;
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
using System.Windows.Shapes;

namespace DesktopUI
{
    /// <summary>
    /// Interaction logic for ClassCreateOrEditWindow.xaml
    /// </summary>
    public partial class ClassCreateOrEditWindow : Window
    {
        private CreateOrEditWindowsState createOrEditWindowsState;
        private UniClass classToEdit;

        public ClassCreateOrEditWindow()
        {
            InitializeComponent();
            TbClassName.Focus();
        }

        public ClassCreateOrEditWindow(CreateOrEditWindowsState state) : this()
        {
            TbTitle.Text = "Create a new class";
            createOrEditWindowsState = state;
        }

        public ClassCreateOrEditWindow(UniClass uniClass,CreateOrEditWindowsState state) : this()
        {
            TbTitle.Text = "Edit class";
            createOrEditWindowsState = state;
            classToEdit = uniClass;
            TbClassName.Text = uniClass.ClassName;
            BtnColorPicker.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(uniClass.ClassColor);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            switch (createOrEditWindowsState) 
            {
                case CreateOrEditWindowsState.Create:
                    ClassesLogic.CreateNewClass(TbClassName.Text, BtnColorPicker.Background.ToString());
                    break;
                case CreateOrEditWindowsState.Edit:
                    ClassesLogic.EditClass(classToEdit,TbClassName.Text, BtnColorPicker.Background.ToString());
                    break;
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnColorPicker_Click(object sender, RoutedEventArgs e)
        {
            PopupColorPicker.IsOpen = true;
        }

        private void BtnWitnColor_Click(object sender, RoutedEventArgs e) 
        {
            Button btn = sender as Button;
            BtnColorPicker.Background = btn.Background;
            PopupColorPicker.IsOpen = false;
        }
    }
}
