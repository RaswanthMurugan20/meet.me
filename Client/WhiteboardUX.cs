using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Controls;
using System.Drawing;
using Whiteboard;
using Whiteboard.Client;

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
        abstract Canvas RenderUXElement(List<UXShape> shps, Canvas cn);
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
        /// Creates a new shape 
        /// </summary>
        public Canvas CreateShape(Canvas cn, IWhiteBoardOperationHandler WBOps ,WhiteBoardViewModel.WBTools activeTool, System.Windows.Point strt, System.Windows.Point end, float strokeWidth, System.Drawing.Color strokeColor, string shapeId = null, bool shapeComp = false)
        {
            List<UXShape> toRender;
            Coordinate C_strt = new Coordinate(strt.X, strt.Y);
            Coordinate C_end = new Coordinate(end.X, end.Y);
            Whiteboard.Color strk_clr = new Whiteboard.Color(strokeColor.R, strokeColor.G, strokeColor.B);
hh
            //Remove existing temporary shapes
            if (shapeId != null && shapeComp == false)
            {
                foreach (var uiElement in cn.Children.OfType<UIElement>().Where(x => x.Uid == shapeId))
                {
                    cn.Children.Remove(uiElement);
                }
            }

            switch (activeTool)
            {
                case WhiteBoardViewModel.WBTools.NewLine:
                    toRender = WBOps.CreateLine(C_strt, C_end, strokeWidth, strk_clr, shapeId, shapeComp );
                    cn = this.RenderUXElement(toRender, cn);
                    break;
                case WhiteBoardViewModel.WBTools.NewRectangle:
                    toRender = WBOps.CreateRectangle(C_strt, C_end, strokeWidth, strk_clr, shapeId, shapeComp);
                    cn = this.RenderUXElement(toRender, cn);
                    break;
                case WhiteBoardViewModel.WBTools.NewEllipse:
                    toRender = WBOps.CreateEllipse(C_strt, C_end, strokeWidth, strk_clr, shapeId, shapeComp);
                    cn = this.RenderUXElement(toRender, cn);
                    break;
            }
            return cn;
        }

        /// <summary>
        /// Translate the shape according to input events  
        /// </args> shps is the 'selectedShapes' list in the ViewModel
        /// </summary>
        public void MoveShape(Canvas cn, IWhiteBoardOperationHandler WBOps, System.Windows.Point strt, System.Windows.Point end, List<UXShape> shps, bool shapeComp)
        {
            Coordinate C_strt = new Coordinate(strt.X, strt.Y);
            Coordinate C_end = new Coordinate(end.X, end.Y);

            List<String> shps_uids = new List<String>();
            foreach (UXShape shp in shps)
            {
                //Assuming that UXShape has a UID entry for the current shape (TEMPORARY)
                String uid = shp.UID;

                //(TEMPORARY)
                List<UXShape> modif_shps = WBOps.TranslateShape(C_strt, C_end, uid, shapeComp);
                this.DeleteShape(modif_shps, cn);
                this.RenderUXElement(modif_shps, cn);
            }
        }

        /// <summary>
        /// Rotate the selected shape by input degrees  
        /// </args> shps is the 'selectedShapes' list in the ViewModel
        /// </summary>
        public void RotateShape(Canvas cn, IWhiteBoardOperationHandler WBOps, Point strt, Point end, List<UXShape> shps, bool shapeComp)
        {
            Coordinate C_strt = new Coordinate(strt.X, strt.Y);
            Coordinate C_end = new Coordinate(end.X, end.Y);

            List<String> shps_uids = new List<String>();
            foreach (UXShape shp in shps)
            {
                //Assuming that UXShape has a UID entry for the current shape (TEMPORARY)
                String uid = shp.UID;

                //(TEMPORARY)
                List<UXShape> modif_shps =  WBOps.RotateShape(C_strt, C_end, uid, shapeComp);
                this.DeleteShape(modif_shps, cn);
                this.RenderUXElement(modif_shps, cn);
            }
        }

        /// <summary>
        /// Create a duplicate of selected shape on Canvas   
        /// </summary>
        public void DuplicateShape(Canvas cn, IWhiteBoardOperationHandler WBOps, List<UXShape> shps, double offs_x = 10.0, double offs_y = 10.0)
        {
            foreach (UXShape shp in shps)
            {
                List<UXShape> toRender;
                //assuming that shp.Shape gives the System.Windows.Shapes instance (TEMPORARY)
                Shape s = shp.Shape;
                double s_x = Canvas.GetLeft(s);
                double s_y = Canvas.GetTop(s);
                double ht = s.Height;
                double wdth = s.Width;

                //Write code to get appropriate start and end point locations for the duplicate shape at an offset of (10,10) from current shape
                //OR
                //Ask WB team to provide a function that does the above internally, eg. WBDuplicateShape(string uid, offs_x=10, offs_y=10)
                //(TEMPORARY)
                if (s is Line)
                {
                    toRender = WBOps.CreateLine();
                }
                else if (s is System.Windows.Shapes.Rectangle)
                {
                    toRender = WBOps.CreateRectangle();
                }
                else if (s is System.Windows.Shapes.Ellipse)
                {
                    toRender = WBOps.CreateEllipse();
                }
                //INCOMPLETE
            }

        }

        /// <summary>
        /// Render fetched shape updates on canvas  
        /// </summary>
        public Canvas RenderUXElement(List<UXShape> shps, Canvas cn)
        {
            foreach (UXShape shp in shps)
            {
                //assuming that shp.Shape gives the System.Windows.Shapes instance (TEMPORARY)
                cn.Children.Add(shp.Shape);
            }
            
            return cn;
        }

        /// <summary>
        /// Delete selected shape   
        /// </summary>
        public Canvas DeleteShape(List<UXShape> shps_can, Canvas cn)
        {
            foreach (UXShape shp in shps_can)
            {
                //Assuming that UXShape shp has a UID entry for the current shape (TEMPORARY)
                foreach (var uiElement in cn.Children.OfType<UIElement>().Where(x => x.Uid == shp.UID))
                {
                    cn.Children.Remove(uiElement);
                }
            }
            return cn;
        }

        /// <summary>
        /// Adjust finer attributes of selected shape, like 
        /// </summary>
        public void CustomizeShape(List<UXShape> shps)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Resize the list of selected shapes 'selectedShapes' based on start and end points 
        /// </summary>
        public void ResizeShape(WhiteBoardOperationHandler WBOps, List<UXShape> shps, System.Windows.Point strt, System.Windows.Point end, bool shapeComp)
        {
            Coordinate C_strt = new Coordinate(strt.X, strt.Y);
            Coordinate C_end = new Coordinate(end.X, end.Y);
            foreach (UXShape shp in shps)
            {
                Shape s = shp.Shape;
                WBOps.ResizeShape(C_strt, C_end, s.Uid, shapeComp);
            }
            return;
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
        public Canvas RenderUXElement(List<UXShape> shps, Canvas cn)
        {
            //Write implementation code
            return cn;
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

        public System.Windows.Point start;
        public System.Windows.Point end;

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
        public Canvas ChangeWbBackground(Canvas cn, System.Windows.Media.Color clr)
        {
            cn.Background = new SolidColorBrush(clr);
            return cn;
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
            // ChangeBoardState() not declared by WB yet, temporary code here (TEMPORARY)
            Whiteboard.BoardState bs = BoardState.ACTIVE;
            return;
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
