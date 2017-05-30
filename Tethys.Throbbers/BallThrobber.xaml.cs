#region Header
// --------------------------------------------------------------------------
// Tethys.Throbbers
// ==========================================================================
//
// A custom control library for WPF applications.
//
// ==========================================================================
// <copyright file="BallThrobber.xaml.cs" company="Tethys">
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
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for BallThrobber.
    /// </summary>
    public partial class BallThrobber
    {
        #region PRIVATE PROPERTIES        
        /// <summary>
        /// The animated controls.
        /// </summary>
        private List<Shape> animatedControls;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region DEPENDENCY PROPERTIES
        #region DP - BallSize
        /// <summary>
        /// Dependency property for the ball size.
        /// </summary>
        public static readonly DependencyProperty BallSizeProperty =
            DependencyProperty.Register("BallSize", typeof(double),
                typeof(BallThrobber), new PropertyMetadata(60.0,
                    (o, args) =>
                    {
                        var obj = o as BallThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.CreateControls();
                    }));

        /// <summary>
        /// Gets or sets the ball size.
        /// </summary>
        public double BallSize
        {
            get { return (double)this.GetValue(BallSizeProperty); }
            set { this.SetValue(BallSizeProperty, value); }
        }
        #endregion

        #region DP - BallBrush
        /// <summary>
        /// Dependency property for the ball brush.
        /// </summary>
        public static readonly DependencyProperty BallBrushProperty =
            DependencyProperty.Register("BallBrush", typeof(Brush),
                typeof(BallThrobber), new PropertyMetadata(Brushes.Black,
                    (o, args) =>
                    {
                        var obj = o as BallThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.SetControlBrush((Brush)args.NewValue);
                    }));

        /// <summary>
        /// Gets or sets the ball brush.
        /// </summary>
        public Brush BallBrush
        {
            get { return (Brush)this.GetValue(BallBrushProperty); }
            set { this.SetValue(BallBrushProperty, value); }
        }
        #endregion

        #region DP - Speed
        /// <summary>
        /// Dependency property for the update/spin speed.
        /// </summary>
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(int),
                typeof(BallThrobber), new PropertyMetadata(60,
                    (o, args) =>
                    {
                        var obj = o as BallThrobber;
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
                typeof(BallThrobber), new PropertyMetadata(10,
                    (o, args) =>
                    {
                        var obj = o as BallThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.CreateControls();
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
        #endregion // DEPENDENCY PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION            
        /// <summary>
        /// Initializes a new instance of the <see cref="BallThrobber"/> class.
        /// </summary>
        public BallThrobber()
        {
            this.InitializeComponent();
            this.InitializeTimer(175);
        } // BallThrobber()
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
            this.animatedControls = new List<Shape>();
            for (var i = 0; i < this.AnimatedItemCount; i++)
            {
                var p1 = CalcPathCircleCoord(60, step * i, center);

                var ellipse = new Ellipse();
                ellipse.Height = this.BallSize;
                ellipse.Width = this.BallSize;
                ellipse.Fill = this.BallBrush;
                ellipse.Opacity = (1.0 / this.AnimatedItemCount) * i;
                ellipse.SetValue(Canvas.LeftProperty, p1.X);
                ellipse.SetValue(Canvas.TopProperty, p1.Y);
                this.Canvas.Children.Add(ellipse);
                this.animatedControls.Add(ellipse);
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
                control.Fill = brush;
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
    } // BallThrobber
}
