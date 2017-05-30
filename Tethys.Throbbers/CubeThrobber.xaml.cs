#region Header
// --------------------------------------------------------------------------
// Tethys.Throbbers
// ==========================================================================
//
// A custom control library for WPF applications.
//
// ==========================================================================
// <copyright file="CubeThrobber.xaml.cs" company="Tethys">
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
    /// Interaction logic for CubeThrobber.
    /// </summary>
    public partial class CubeThrobber
    {
        #region PRIVATE PROPERTIES        
        /// <summary>
        /// The step size for turning angle.
        /// </summary>
        private const int StepSize = 9;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region DEPENDENCY PROPERTIES
        #region DP - Cube1Brush
        /// <summary>
        /// Dependency property for the brush of cube 1.
        /// </summary>
        public static readonly DependencyProperty Cube1BrushProperty =
          DependencyProperty.Register("Cube1Brush", typeof(Brush),
          typeof(CubeThrobber), new PropertyMetadata(Brushes.Gray,
              (o, args) =>
                  {
                      var obj = o as CubeThrobber;
                      if (obj == null)
                      {
                          return;
                      } // if

                      obj.Cube1.Stroke = (Brush)args.NewValue;
                      obj.Cube1.Fill = (Brush)args.NewValue;
                  }));

        /// <summary>
        /// Gets or sets the brush of cube 1.
        /// </summary>
        public Brush Cube1Brush
        {
            get { return (Brush)this.GetValue(Cube1BrushProperty); }
            set { this.SetValue(Cube1BrushProperty, value); }
        }
        #endregion

        #region DP - Cube2Brush
        /// <summary>
        /// Dependency property for the brush of cube 2.
        /// </summary>
        public static readonly DependencyProperty Cube2BrushProperty =
          DependencyProperty.Register("Cube2Brush", typeof(Brush),
          typeof(CubeThrobber), new PropertyMetadata(Brushes.Gray,
              (o, args) =>
              {
                  var obj = o as CubeThrobber;
                  if (obj == null)
                  {
                      return;
                  } // if

                  obj.Cube2.Stroke = (Brush)args.NewValue;
                  obj.Cube2.Fill = (Brush)args.NewValue;
              }));

        /// <summary>
        /// Gets or sets the brush of cube 2.
        /// </summary>
        public Brush Cube2Brush
        {
            get { return (Brush)this.GetValue(Cube2BrushProperty); }
            set { this.SetValue(Cube2BrushProperty, value); }
        }
        #endregion

        #region DP - Cube3Brush
        /// <summary>
        /// Dependency property for the brush of cube 3.
        /// </summary>
        public static readonly DependencyProperty Cube3BrushProperty =
          DependencyProperty.Register("Cube3Brush", typeof(Brush),
          typeof(CubeThrobber), new PropertyMetadata(Brushes.Gray,
              (o, args) =>
              {
                  var obj = o as CubeThrobber;
                  if (obj == null)
                  {
                      return;
                  } // if

                  obj.Cube3.Stroke = (Brush)args.NewValue;
                  obj.Cube3.Fill = (Brush)args.NewValue;
              }));

        /// <summary>
        /// Gets or sets the brush of cube 3.
        /// </summary>
        public Brush Cube3Brush
        {
            get { return (Brush)this.GetValue(Cube3BrushProperty); }
            set { this.SetValue(Cube3BrushProperty, value); }
        }
        #endregion

        #region DP - Cube4Brush
        /// <summary>
        /// Dependency property for the brush of cube 4.
        /// </summary>
        public static readonly DependencyProperty Cube4BrushProperty =
          DependencyProperty.Register("Cube4Brush", typeof(Brush),
          typeof(CubeThrobber), new PropertyMetadata(Brushes.Gray,
              (o, args) =>
              {
                  var obj = o as CubeThrobber;
                  if (obj == null)
                  {
                      return;
                  } // if

                  obj.Cube4.Stroke = (Brush)args.NewValue;
                  obj.Cube4.Fill = (Brush)args.NewValue;
              }));

        /// <summary>
        /// Gets or sets the outer circle brush.
        /// </summary>
        public Brush Cube4Brush
        {
            get { return (Brush)this.GetValue(Cube4BrushProperty); }
            set { this.SetValue(Cube4BrushProperty, value); }
        }
        #endregion

        #region DP - Speed
        /// <summary>
        /// Dependency property for the update/spin speed.
        /// </summary>
        public static readonly DependencyProperty SpeedProperty =
          DependencyProperty.Register("Speed", typeof(int),
          typeof(CubeThrobber), new PropertyMetadata(60,
              (o, args) =>
              {
                  var obj = o as CubeThrobber;
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
        /// Initializes a new instance of the <see cref="CubeThrobber"/> class.
        /// </summary>
        public CubeThrobber()
        {
            this.InitializeComponent();
            this.InitializeTimer(60);
        } // CubeThrobber()
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
            this.SpinnerRotate.Angle = (this.SpinnerRotate.Angle + StepSize) % 360;
        } // HandleAnimationTick()
        #endregion // PRIVATE METHOD
    } // CubeThrobber()
} 
