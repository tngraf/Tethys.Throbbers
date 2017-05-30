#region Header
// --------------------------------------------------------------------------
// Tethys.Throbbers
// ==========================================================================
//
// A custom control library for WPF applications.
//
// ==========================================================================
// <copyright file="CircularThrobberBase.cs" company="Tethys">
// Copyright  2017 by T. Graf
//            All rights reserved.
//            Licensed under the Apache License, Version 2.0.
//            Unless required by applicable law or agreed to in writing, 
//            software distributed under the License is distributed on an
//            "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
//            either express or implied. 
// </copyright>
// 
// System ... Microsoft .Net Framework 4.5.2 
// Tools .... Microsoft Visual Studio 2017
//
// ---------------------------------------------------------------------------
#endregion

namespace Tethys.Throbbers
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    /// <summary>
    /// Base class for circular throbbers.
    /// </summary>
    /// <seealso cref="UserControl" />
    public class CircularThrobberBase : UserControl
    {
        #region PROTECTED PROPERTIES
        /// <summary>
        /// Gets or sets the animation timer.
        /// </summary>
        protected DispatcherTimer AnimationTimer { get; set; }
        #endregion // PROTECTED PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.AnimationTimer.Tick += this.HandleAnimationTick;
            this.AnimationTimer.Start();
        } // Start()

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.AnimationTimer.Stop();
            Mouse.OverrideCursor = Cursors.Arrow;
            this.AnimationTimer.Tick -= this.HandleAnimationTick;
        } // Stop()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS            
        /// <summary>
        /// Initializes the timer.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void InitializeTimer(int value)
        {
            this.AnimationTimer = new DispatcherTimer(
                DispatcherPriority.ContextIdle, this.Dispatcher);

            this.AnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, value);
        } // InitializeTimer()

        /// <summary>
        /// Handles the animation tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void HandleAnimationTick(object sender, EventArgs e)
        {
        } // HandleAnimationTick()

        /// <summary>
        /// Handles the unloaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing 
        /// the event data.</param>
        protected virtual void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            this.Stop();
        } // HandleUnloaded()

        /// <summary>
        /// Handles the visible changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance
        /// containing the event data.</param>
        protected virtual void HandleVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isVisible = (bool)e.NewValue;

            if (isVisible)
            {
                this.Start();
            }
            else
            {
                this.Stop();
            } // if
        } // HandleVisibleChanged()

        /// <summary>
        /// Calculates the position of a point on a circle, based on radius and angle,
        /// translated to WPF coordinates.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>A tuple with the coordinates.</returns>
        protected static Point CalcPathCircleCoord(double radius, double angle)
        {
            var basic = CalcCircleCoord(radius, angle);

            var result = new Point(radius - basic.X, radius - basic.Y);
            return result;
        } // CalcPathCircleCoord()

        /// <summary>
        /// Calculates the position of a point on a circle, based on radius and angle,
        /// translated to WPF coordinates.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="center">The center.</param>
        /// <returns>
        /// A tuple with the coordinates.
        /// </returns>
        protected static Point CalcPathCircleCoord(double radius, double angle, Point center)
        {
            var basic = CalcCircleCoord(radius, angle);

            var result = new Point(center.X - basic.X, center.Y - basic.Y);
            return result;
        } // CalcPathCircleCoord()

        /// <summary>
        /// Calculates the position of a point on a circle, based on radius and angle.
        /// </summary>
        /// <param name="radius">The radius (0 ... 360).</param>
        /// <param name="angle">The angle.</param>
        /// <returns>A tuple with the coordinates.</returns>
        protected static Point CalcCircleCoord(double radius, double angle)
        {
            var a = (angle * 2 * Math.PI) / 360;
            var x = radius * Math.Cos(a);
            var y = radius * Math.Sin(a);

            return new Point(x, y);
        } // CalcCircleCoord()
        #endregion // PROTECTED METHOD
    } // CircularThrobberBase
}
