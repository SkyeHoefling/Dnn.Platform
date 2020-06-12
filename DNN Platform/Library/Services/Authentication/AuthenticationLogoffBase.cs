﻿

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
using System;

using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Services.Authentication
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The AuthenticationLogoffBase class provides a base class for Authentiication
    /// Logoff controls
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class AuthenticationLogoffBase : UserModuleBase
    {
        private string _AuthenticationType = Null.NullString;
        private string _RedirectURL = Null.NullString;

        /// <summary>
        /// Gets the Dependency Provider to resolve registered
        /// services with the container.
        /// </summary>
        /// <value>
        /// The Dependency Service.
        /// </value>
        protected IServiceProvider DependencyProvider { get; }

        public AuthenticationLogoffBase()
        {
            this.DependencyProvider = Globals.DependencyProvider;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets and Sets the Type of Authentication associated with this control
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string AuthenticationType
        {
            get
            {
                return this._AuthenticationType;
            }

            set
            {
                this._AuthenticationType = value;
            }
        }

        public event EventHandler LogOff;

        public event EventHandler Redirect;

        protected virtual void OnLogOff(EventArgs a)
        {
            if (this.LogOff != null)
            {
                this.LogOff(null, a);
            }
        }

        protected virtual void OnRedirect(EventArgs a)
        {
            if (this.Redirect != null)
            {
                this.Redirect(null, a);
            }
        }
    }
}
