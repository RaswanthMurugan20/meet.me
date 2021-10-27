using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Controls;
using Whiteboard;

namespace Client
{
    /// <summary>
    /// Interface which listens to fetched server updates by IWhiteBoardState and local updates by IShapeOperation
    /// </summary>
    interface IWhiteBoardUpdater
    {
        /// <summary>
        /// Fetch updates from IWhiteBoardState for rendering in the view  
        /// </summary>
        abstract void FetchServerUpdates();

        /// <summary>
        /// Render fetched updates on canvas  
        /// </summary>
        abstract void RenderUXElement(List<UXShape> shps);
    }

    /// <summary>
    /// Class to manage existing and new shapes by providing various methods by aggregating WhiteBoard Module  
    /// </summary>
    public class ShapeManager : IWhiteBoardUpdater
    {

        private List<int> selectedShapes;

        /// <summary>
        /// Fetch shape updates from IWhiteBoardState for rendering in the view   
        /// </summary>
        public void FetchServerUpdates()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Render fetched shape updates on canvas  
        /// </summary>
        public void RenderUXElement(List<UXShape> shps)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handle input events for selection  
        /// </arg> Shape sh: The shape which is clicked on
        /// </arg> int mode: 0 for single selection, 1 for multiple selection (i.e, Ctrl is pressed while a shape is clicked)
        /// </summary>
        public void SelectShape(Shape sh, int mode = 0)
        {
            switch(mode)
            {
               //single shape selection case
              case 0:
                selectedShapes = new List<int> { int.Parse(sh.Uid) };
                break;
               //multiple shape selection case
              case 1:
                selectedShapes.Add(int.Parse(sh.Uid));
                break;
             }
            return;
        }

        /// <summary>
        /// Create a new shape 
        /// </summary>
        public void CreateShape(WhiteBoardViewModel.WBTools activeTool)
        {
            List<UXShape> toRender;
            switch (activeTool)
            {
                case WhiteBoardViewModel.WBTools.NewLine:
                    break;
                case WhiteBoardViewModel.WBTools.NewRectangle:
                    break;
                case WhiteBoardViewModel.WBTools.NewEllipse:
                    break;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Translate the shape according to input events  
        /// </summary>
        public void MoveShape()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rotate the selected shape by input degrees  
        /// </summary>
        public void RotateShape()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a duplicate of selected shape on Canvas   
        /// </summary>
        public void DuplicateShape()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete selected shape   
        /// </summary>
        public void DeleteShape()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adjust finer attributes of selected shape  
        /// </summary>
        public void CustomizeShape()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set background color of the selected shape   
        /// </summary>
        public void SetBackgroundColor()
        {
            throw new NotImplementedException();
        }

    }

    /// <summary>
    /// Class to manage existing and new FreeHand instances by providing various methods by aggregating WhiteBoard Module    
    /// </summary>
    public class FreeHand : IWhiteBoardUpdater
    {

        /// <summary>
        /// Fetch FreeHand instances updates from IWhiteBoardState for rendering in the view   
        /// </summary>
        public void FetchServerUpdates()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Render FreeHand instances shape updates on canvas  
        /// </summary>
        public void RenderUXElement(List<UXShape> shps)
        {
            throw new NotImplementedException();
        }

    }

    /// <summary>
    /// View Model of Whiteboard in MVVM design pattern 
    /// </summary>
    public class WhiteBoardViewModel 
    {
        IWhiteBoardOperationHandler WBOps = new WhiteBoardOperationHandler();


        /// UX sets this enum to different options when user clicks on the appropriate tool icon
        public enum WBTools
        {
            Initial, /// Initialised value, never to be used again
            Selection,
            NewLine,
            NewRectangle,
            NewEllipse,
            Rotate,
            Move,
            Eraser,
            FreeHand
        };

        public Point start;
        public Point end;

        public WBTools activeTool;
        private ShapeManager shapeManager;
        private FreeHand freeHand;

        /// <summary>
        /// Class to manage existing and new shapes by providing various methods by aggregating WhiteBoard Module  
        /// </summary>
        public WhiteBoardViewModel()
        {
            this.shapeManager = new ShapeManager();
            this.freeHand = new FreeHand(); 
            this.activeTool = WBTools.Initial;
        }

        /// <summary>
        /// Changes the Background color of Canvas in View 
        /// </summary>
        public void ChangeWbBackground()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the activeTool based on selected function on Toolbar 
        /// </summary>
        public void ChangeActiveTool()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Changes the Privilege level of the current user  
        /// </summary>
        public void ChangePrivilegeSwitch()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles click event on View 
        /// </summary>
        public void HandleClickEvent(MouseButtonEventArgs bt )
        {

            if (bt.RightButton == MouseButtonState.Pressed) {
                
            }
            else if (bt.RightButton == MouseButtonState.Released)
            {

            }
            else if (bt.LeftButton == MouseButtonState.Pressed)
            {
                
            }
            else if (bt.LeftButton == MouseButtonState.Released)
            {

            }

            return;
        }
    }
}
