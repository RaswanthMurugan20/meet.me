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
using System.Windows.Shapes;
using Whiteboard;
using Whiteboard.Client;

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    public partial class WhiteBoardView : Window
    {
        private Button activeButton;
        private string buttonDefaultColor = "#EC407A";
        private string buttonSelectedColor = "#536DFE";
        private WhiteBoardViewModel viewModel;
        public Canvas GlobCanvas;

      
        public WhiteBoardView()
        {
            InitializeComponent();
            this.GlobCanvas = MyCanvas; 
            viewModel = new WhiteBoardViewModel();
        }

        // Canvas Mouse actions 
        private void OnCanvasMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
           if (e.LeftButton == MouseButtonState.Pressed){
                this.viewModel.start = e.GetPosition(MyCanvas);
           }
            
        }

        private void OnCanvasMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnCanvasMouseMove(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenPopupButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SetBGButtonPopUp.StaysOpen = true;
        }

        private void OpenPopupButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SetBGButtonPopUp.StaysOpen = false;
        }

        //Toolbar selection tool 
        private void ClickedSelectTool(object sender, RoutedEventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonDefaultColor));
            }
            activeButton = sender as Button;
            activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonSelectedColor));
            MessageBox.Show("ClickedSelectTool");
            return;
        }
        private void ClickedRectTool(object sender, RoutedEventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonDefaultColor));
            }
            activeButton = sender as Button;
            activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonSelectedColor));
            MessageBox.Show("ClickedRectTool");
            return;
        }
        private void ClickedEllTool(object sender, RoutedEventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonDefaultColor));
            }
            activeButton = sender as Button;
            activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonSelectedColor));
            MessageBox.Show("ClickedEllTool");
            return;
        }
        private void ClickedFreehandTool(object sender, RoutedEventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonDefaultColor));
            }
            activeButton = sender as Button;
            activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonSelectedColor));
            MessageBox.Show("ClickedFreehandTool");
            return;
        }
        private void ClickedEraserTool(object sender, RoutedEventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonDefaultColor));
            }
            activeButton = sender as Button;
            activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonSelectedColor));
            MessageBox.Show("ClickedEraserTool");
            return;
        }

        private void ClickedLineTool(object sender, RoutedEventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonDefaultColor));
            }
            activeButton = sender as Button;
            activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonSelectedColor));
            MessageBox.Show("ClickedLineTool");
            return;
        }

        //Whiteboard General tools 
        private void ClickedSetBG(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ClickedSetBG");
            return;
        }

        private void ClickedClearFrame(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ClickedClearFrame");
            return;
        }

        private void ClickedSaveFrame(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ClickedSaveFrame");
            return;
        }

        private void ClickedUndoButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ClickedUndo");
            return;
        }

        private void ClickedRedoButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ClickedRedo");
            return;
        }

        private void Bu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bu.Toggled1 == true)
            {
                MessageBox.Show("Toggled On");
            }
            else
            {
                MessageBox.Show("Toggled Off");
            }


        }
        //Parent Window click event
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
