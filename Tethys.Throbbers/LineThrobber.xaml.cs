#region Header
// --------------------------------------------------------------------------
// Tethys.Throbbers
// ==========================================================================
//
// A custom control library for WPF applications.
//
// ==========================================================================
// <copyright file="LineThrobber.xaml.cs" company="Tethys">
// Copyright  2017 by T. Graf
//            All rights reserved.
//            Licensed under the Apache License, Version 2.0.
//            Unless required by applicable law or agreed to in writing, 
//            software distributed under the License is distributed on an
//            "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
//            either express or implied. 
// </copyright>
// 
// System ... Microsoft .Net Framework 4.5. 
// Tools .... Microsoft Visual Studio 2017
//
// ---------------------------------------------------------------------------
#endregion

namespace Tethys.Throbbers
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for LineThrobber.
    /// </summary>
    public partial class LineThrobber
    {
        #region PRIVATE PROPERTIES        
        /// <summary>
        /// The animated controls.
        /// </summary>
        private List<Line> animatedControls;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region DEPENDENCY PROPERTIES
        #region DP - OuterRadius
        /// <summary>
        /// Dependency property for the outer radius.
        /// </summary>
        public static readonly DependencyProperty OuterRadiusProperty =
            DependencyProperty.Register("OuterRadius", typeof(double),
                typeof(LineThrobber), new PropertyMetadata(60.0,
                    (o, args) =>
                    {
                        var obj = o as LineThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.CreateControls();
                    }));

        /// <summary>
        /// Gets or sets the outer radius.
        /// </summary>
        public double OuterRadius
        {
            get { return (double)this.GetValue(OuterRadiusProperty); }
            set { this.SetValue(OuterRadiusProperty, value); }
        }
        #endregion

        #region DP - InnerRadius
        /// <summary>
        /// Dependency property for the inner radius.
        /// </summary>
        public static readonly DependencyProperty InnerRadiusProperty =
            DependencyProperty.Register("InnerRadius", typeof(double),
                typeof(LineThrobber), new PropertyMetadata(10.0,
                    (o, args) =>
                    {
                        var obj = o as LineThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.CreateControls();
                    }));

        /// <summary>
        /// Gets or sets the inner radius.
        /// </summary>
        public double InnerRadius
        {
            get { return (double)this.GetValue(InnerRadiusProperty); }
            set { this.SetValue(InnerRadiusProperty, value); }
        }
        #endregion

        #region DP - LineBrush
        /// <summary>
        /// Dependency property for the line brush.
        /// </summary>
        public static readonly DependencyProperty LineBrushProperty =
            DependencyProperty.Register("LineBrush", typeof(Brush),
                typeof(LineThrobber), new PropertyMetadata(Brushes.Black,
                    (o, args) =>
                    {
                        var obj = o as LineThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.SetControlBrush((Brush)args.NewValue);
                    }));

        /// <summary>
        /// Gets or sets the line brush.
        /// </summary>
        public Brush LineBrush
        {
            get { return (Brush)this.GetValue(LineBrushProperty); }
            set { this.SetValue(LineBrushProperty, value); }
        }
        #endregion

        #region DP - Speed
        /// <summary>
        /// Dependency property for the update/spin speed.
        /// </summary>
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(int),
                typeof(LineThrobber), new PropertyMetadata(60,
                    (o, args) =>
                    {
                        var obj = o as LineThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.AnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)args.NewValue);
                    }));

        /// <summary>
        /// Gets or sets the update/spin speed.
        /// </summary>
        public int Speed
        {
            get { return (int)this.GetValue(SpeedProperty); }
            set { this.SetValue(SpeedProperty, value); }
        }
        #endregion

        #region DP - AnimatedItemCount
        /// <summary>
        /// Dependency property for animated item count.
        /// </summary>
        public static readonly DependencyProperty AnimatedItemCountProperty =
            DependencyProperty.Register("AnimatedItemCount", typeof(int),
                typeof(LineThrobber), new PropertyMetadata(10,
                    (o, args) =>
                    {
                        var obj = o as LineThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.CreateControls();
                        obj.SetControlBrush(obj.LineBrush);
                    }));

        /// <summary>
        /// Gets or sets the update/spin speed.
        /// </summary>
        public int AnimatedItemCount
        {
            get { return (int)this.GetValue(AnimatedItemCountProperty); }
            set { this.SetValue(AnimatedItemCountProperty, value); }
        }
        #endregion

        #region DP - LineThickness
        /// <summary>
        /// Dependency property for the line (spoke) thickness.
        /// </summary>
        public static readonly DependencyProperty LineThicknessProperty =
            DependencyProperty.Register("LineThickness", typeof(double),
                typeof(LineThrobber), new PropertyMetadata(10.0,
                    (o, args) =>
                    {
                        var obj = o as LineThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.CreateControls();
                    }));

        /// <summary>
        /// Gets or sets the line (spoke) thickness.
        /// </summary>
        public double LineThickness
        {
            get { return (double)this.GetValue(LineThicknessProperty); }
            set { this.SetValue(LineThicknessProperty, value); }
        }
        #endregion
        #endregion // DEPENDENCY PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION            
        /// <summary>
        /// Initializes a new instance of the <see cref="LineThrobber"/> class.
        /// </summary>
        public LineThrobber()
        {
            this.InitializeComponent();
            this.InitializeTimer(175);
        } // LineThrobber()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region UI HANDLING
        /// <summary>
        /// Handles the loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            this.CreateControls();
        } // HandleLoaded()

        /// <summary>
        /// Creates the controls.
        /// </summary>
        private void CreateControls()
        {
            if (this.AnimatedItemCount < 1)
            {
                this.AnimatedItemCount = 1;
            } // if

            if (this.animatedControls != null)
            {
                // remove old controls
                foreach (var animatedControl in this.animatedControls)
                {
                    this.Canvas.Children.Remove(animatedControl);
                } // foreach
            } // if

            var step = 360 / this.AnimatedItemCount;
            var center = new Point(60, 60);
            this.animatedControls = new List<Line>();
            for (var i = 0; i < this.AnimatedItemCount; i++)
            {
                var p1 = CalcPathCircleCoord(this.InnerRadius, step * i, center);
                var p2 = CalcPathCircleCoord(this.OuterRadius, step * i, center);
                var line = new Line();
                line.X1 = p1.X;
                line.Y1 = p1.Y;
                line.X2 = p2.X;
                line.Y2 = p2.Y;
                line.StrokeThickness = this.LineThickness;
                line.StrokeStartLineCap = PenLineCap.Round;
                line.StrokeEndLineCap = PenLineCap.Round;
                line.Stroke = this.LineBrush;
                line.Opacity = (1.0 / this.AnimatedItemCount) * i;
    
                this.Canvas.Children.Add(line);
                this.animatedControls.Add(line);
            } // for
        } // CreateControls()

        /// <summary>
        /// Sets the control brush.
        /// </summary>
        /// <param name="brush">The brush.</param>
        private void SetControlBrush(Brush brush)
        {
            if (this.animatedControls == null)
            {
                return;
            } // if

            foreach (var control in this.animatedControls)
            {
                control.Stroke = brush;
            } // foreach
        } // SetControlBrush()
        #endregion // UI HANDLING

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS            
        /// <summary>
        /// Handles the animation tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void HandleAnimationTick(object sender, EventArgs e)
        {
            var delta = 360.0 / this.animatedControls.Count;
            this.SpinnerRotate.Angle = (this.SpinnerRotate.Angle + delta) % 360;
        } // HandleAnimationTick()
        #endregion // PRIVATE METHODS
    } // LineThrobber
}
