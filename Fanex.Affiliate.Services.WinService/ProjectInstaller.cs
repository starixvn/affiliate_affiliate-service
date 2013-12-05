// --------------------------------------------------------------------------
// <copyright file="ProjectInstaller.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.WinService
{
    using System.ComponentModel;

    /// <summary>
    /// The Service Installer.
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();

            serviceInstaller1.ServiceName = Constants.ServiceName;
            serviceInstaller1.DisplayName = Constants.ServiceName;
            serviceInstaller1.Description = Constants.ServiceDescription;
        }
    }
}