#region Header
// --------------------------------------------------------------------------
// Tethys.Throbbers
// ==========================================================================
//
// A custom control library for WPF applications.
//
// ==========================================================================
// <copyright file="RingThrobber.xaml.cs" company="Tethys">
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
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for RingThrobber.
    /// </summary>
    public partial class RingThrobber
    {
        #region DEPENDENCY PROPERTIES
        #region DP - Thickness
        /// <summary>
        /// Dependency property for the thickness of the ring.
        /// </summary>
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double),
                typeof(RingThrobber), new PropertyMetadata(60.0,
                    (o, args) =>
                    {
                        var obj = o as RingThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.Ring.StrokeThickness = (double)args.NewValue;
                        obj.Cavity.StrokeThickness = (double)args.NewValue;
                    }));

        /// <summary>
        /// Gets or sets the thickness of the ring.
        /// </summary>
        public double Thickness
        {
            get { return (double)this.GetValue(ThicknessProperty); }
            set { this.SetValue(ThicknessProperty, value); }
        }
        #endregion

        #region DP - RingBrush
        /// <summary>
        /// Dependency property for the ring brush.
        /// </summary>
        public static readonly DependencyProperty RingBrushProperty =
            DependencyProperty.Register("RingBrush", typeof(Brush),
                typeof(RingThrobber), new PropertyMetadata(Brushes.Black,
                    (o, args) =>
                    {
                        var obj = o as RingThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.Ring.Stroke = (Brush)args.NewValue;
                    }));

        /// <summary>
        /// Gets or sets the line brush.
        /// </summary>
        public Brush RingBrush
        {
            get { return (Brush)this.GetValue(RingBrushProperty); }
            set { this.SetValue(RingBrushProperty, value); }
        }
        #endregion

        #region DP - CavityBrush
        /// <summary>
        /// Dependency property for the cavity brush.
        /// </summary>
        public static readonly DependencyProperty CavityBrushProperty =
            DependencyProperty.Register("CavityBrush", typeof(Brush),
                typeof(RingThrobber), new PropertyMetadata(Brushes.Black,
                    (o, args) =>
                    {
                        var obj = o as RingThrobber;
                        if (obj == null)
                        {
                            return;
                        } // if

                        obj.Cavity.Stroke = (Brush)args.NewValue;
                    }));

        /// <summary>
        /// Gets or sets the cavity brush.
        /// </summary>
        public Brush CavityBrush
        {
            get { return (Brush)this.GetValue(CavityBrushProperty); }
            set { this.SetValue(CavityBrushProperty, value); }
        }
        #endregion

        #region DP - Speed
        /// <summary>
        /// Dependency property for the update/spin speed.
        /// </summary>
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(int),
                typeof(RingThrobber), new PropertyMetadata(60,
                    (o, args) =>
                    {
                        var obj = o as RingThrobber;
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
        #endregion // DEPENDENCY PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION            
        /// <summary>
        /// Initializes a new instance of the <see cref="RingThrobber"/> class.
        /// </summary>
        public RingThrobber()
        {
            this.InitializeComponent();
            this.InitializeTimer(175);
        } // RingThrobber()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS            
        /// <summary>
        /// Handles the animation tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void HandleAnimationTick(object sender, EventArgs e)
        {
            this.SpinnerRotate.Angle = (this.SpinnerRotate.Angle + 36) % 360;
        } // HandleAnimationTick()
        #endregion // PRIVATE METHODS
    } // RingThrobber
}
