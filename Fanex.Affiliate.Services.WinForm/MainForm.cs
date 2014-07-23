// --------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.WinForm
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Jobs;
    using Fanex.Affiliate.Services.Jobs.DTO;

    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            var today = DateTime.Today;
            fromDatePicker.Value = today;
            toDatePicker.Value = today;
            var licenseeNames = LicenseeManager.Licensees.Select(x => x.Name).ToArray();
            licenseesListBox.Items.AddRange(licenseeNames);
        }

        /// <summary>
        /// Tests the MSMQ.
        /// </summary>
        public void TestMsmq()
        {
            Logger.Info("Test logging MSMQ");
        }

        /// <summary>
        /// Form load event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">OnLoad event arguments.</param>
        public void MainFormLoad(object sender, EventArgs e)
        {
            Initialize();
        }

        /// <summary>
        /// Handles the Click event of the importButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ImportButtonClicked(object sender, EventArgs e)
        {
            const string ConfirmImportMessage = "Are you sure to import data right now?";
            const string SelectLicenseeWarning = "Please select a licensee";
            const string SelectDateWarning = "ToDate must be equal or greater than FromDate";

            if (licenseesListBox.SelectedItems.Count <= 0)
            {
                MessageBox.Show(SelectLicenseeWarning, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                return;
            }

            if (fromDatePicker.Value > toDatePicker.Value)
            {
                MessageBox.Show(SelectDateWarning, Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                return;
            }

            var diaglogResult = MessageBox.Show(
                ConfirmImportMessage, 
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button1, 
                MessageBoxOptions.RightAlign);
            if (diaglogResult != DialogResult.Yes)
            {
                return;
            }

            foreach (var item in licenseesListBox.SelectedItems)
            {
                var licenseeName = (string)item;
                var importType = ImportType.ImportMemberDaily;
                if (!rdMemberDaily.Checked)
                {
                    importType = rdMemberInfo.Checked ? ImportType.ImportMemberInfo : ImportType.ImportExchangeRate;
                }

                var importInfo = new ManuallyImportInfo
                {
                    FromDate = fromDatePicker.Value,
                    ToDate = toDatePicker.Value,
                    Licensee = LicenseeManager.GetLicensee(licenseeName)
                };

                var controller = BaseImporter.Create(importType, importInfo.Licensee);
                controller.Import(importInfo);
            }
        }

        public void TestButtonClicked(object sender, EventArgs e)
        {
            TestMsmq();
        }
    }
}