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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using Fanex.Affiliate.Services.Controller;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Util;
    using Fanex.Affiliate.Services.DataService.Implement;
    using Fanex.Affiliate.Services.DataService.Xml;
    using Fanex.Affiliate.Services.DataService.Xml.Objects;
    using Fanex.Data;

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
        public void TestMSMQ()
        {
            Logger.Info("Test logging MSMQ");
        }

        /// <summary>
        /// Form load event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">OnLoad event arguments.</param>
        public void MainForm_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        /// <summary>
        /// Handles the Click event of the importButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (licenseesListBox.SelectedItems.Count <= 0)
                {
                    MessageBox.Show(this, "Please select a licensee", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (fromDatePicker.Value > toDatePicker.Value)
                {
                    MessageBox.Show(this, "ToDate must be equal or greater than FromDate", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                const string Message = "Are you sure to import data right now?";
                var diaglogResult = MessageBox.Show(this, Message, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

                    var controller = ImportController.Create(importType, importInfo.Licensee);
                    controller.Import(importInfo);
                }
            }
            catch (Exception ex)
            {
                string message = ExceptionFormatter.GetMessage(ex);
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TestButton_Click(object sender, EventArgs e)
        {
            TestMSMQ();
        }

        #region TempCode, Will Be Deleted

        /// <summary>
        /// Do something.
        /// </summary>
        /// <param name="sender">Button sender.</param>
        /// <param name="e">Event arguments.</param>
        public void UpdateAllMemberInfoButtonClicked(object sender, EventArgs e)
        {
            var extraMessage = string.Empty;
            try
            {
                if (licenseesListBox.SelectedItems.Count <= 0)
                {
                    MessageBox.Show(this, "Please select a licensee", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                const string Message = "Are you sure to update data right now?";
                var diaglogResult = MessageBox.Show(this, Message, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diaglogResult != DialogResult.Yes)
                {
                    return;
                }

                foreach (var item in licenseesListBox.SelectedItems)
                {
                    var licenseeName = (string)item;

                    // Get all member user name
                    var licensee = LicenseeManager.GetLicensee(licenseeName);
                    var userList = GetLiceseeMembers(licenseeName);
                    var logTracer = LogTracer.Create();
                    var commonService = new CommonDataService(licensee, logTracer);
                    var webService = new WebDataService(licensee, logTracer);
                    var systemName = commonService.GetSystemName();

                    foreach (var username in userList)
                    {
                        var custInfo = webService.GetLicenseeCustInfo(systemName, username);
                        extraMessage += string.Format("\nRegister:{0}.Created:{1}.username:{2}", custInfo.RegisterDate, custInfo.FirstDepositDate, username);
                        UpdateMembers(licenseeName, username, custInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ExceptionFormatter.GetMessage(ex);
                message += extraMessage;
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get licensee members information.
        /// </summary>
        /// <param name="licenseeName">Licensee name.</param>
        /// <returns>List of username.</returns>
        public IList<string> GetLiceseeMembers(string licenseeName)
        {
            var result = new List<string>();
            var actualKey = licenseeName.ToUpper() + "_Aff_GetAllMembers";
            using (var objectDb = new ObjectDb(actualKey))
            {
                var db = objectDb.Database;
                var command = objectDb.DbCommand;
                command.CommandTimeout = 3600;

                using (var reader = db.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        result.Add(reader["username"].ToString());
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Update extra member information.
        /// </summary>
        /// <param name="licenseeName">Licensee name.</param>
        /// <param name="username">Username of member.</param>
        /// <param name="member">Member information.</param>
        public void UpdateMembers(string licenseeName, string username, GetLicenseeCustInfo member)
        {
            var actualKey = licenseeName.ToUpper() + "_Aff_MemberInfoUpdate";
            using (var objectDb = new ObjectDb(actualKey))
            {
                var db = objectDb.Database;
                var command = objectDb.DbCommand;
                command.CommandTimeout = 3600;
                var parameters = new Hashtable();
                parameters.Add("username", username);
                parameters.Add("ibcusername", member.IBCusername);
                parameters.Add("firstname", member.FirstName);
                parameters.Add("lastname", member.LastName);
                parameters.Add("currency", member.Currency);
                parameters.Add("email", member.Email);
                parameters.Add("createddate", member.FirstDepositDate.Year > 1900 ? new DateTime?(member.FirstDepositDate) : null);
                parameters.Add("registerdate", member.RegisterDate.Year > 1900 ? new DateTime?(member.RegisterDate) : null);
                parameters.Add("btag", member.btag);
                parameters.Add("phone", member.Phone);
                parameters.Add("address", member.Address);

                objectDb.AssignParamValues(parameters);
                db.ExecuteNonQuery(command);
            }
        }

        #endregion

        #region TempCode, Will Be Deleted

        /// <summary>
        /// Update daily member win lost.
        /// </summary>
        /// <param name="sender">Button sender.</param>
        /// <param name="e">Event Argument.</param>
        private void UpdateAllMemberDailyButtonClick(object sender, EventArgs e)
        {
            var extraMessage = string.Empty;
            try
            {
                if (licenseesListBox.SelectedItems.Count <= 0)
                {
                    MessageBox.Show(this, "Please select a licensee", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                const string Message = "Are you sure to update data right now?";
                var diaglogResult = MessageBox.Show(this, Message, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diaglogResult != DialogResult.Yes)
                {
                    return;
                }

                foreach (var item in licenseesListBox.SelectedItems)
                {
                    var licenseeName = (string)item;

                    // Get all member user name
                    var licensee = LicenseeManager.GetLicensee(licenseeName);
                    var logTracer = LogTracer.Create();
                    var memberDailyService = new MemberDailyDataService(licensee);
                    memberDailyService.SetLogTracer(logTracer);

                    var dateList = MemberDailyGetAllWinLostDate(licenseeName);
                    foreach (var date in dateList)
                    {
                        var memberXml = MemberDailyGetLiceseeMembers(licenseeName, date);
                        var winlostDataXml = memberDailyService.GetPlayersWinlostByProduct(memberXml, licensee.UsernamePrefix, date);
                        MemberDailyUpdateWinLost(licenseeName, winlostDataXml, date);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ExceptionFormatter.GetMessage(ex);
                message += extraMessage;
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get all win lost dates of licensee.
        /// </summary>
        /// <param name="licenseeName">Licensee name.</param>
        /// <returns>List of win lost date.</returns>
        private IList<DateTime> MemberDailyGetAllWinLostDate(string licenseeName)
        {
            var result = new List<DateTime>();
            var actualKey = licenseeName.ToUpper() + "_DBA_GetAllWinlostDate";
            using (var objectDb = new ObjectDb(actualKey))
            {
                var db = objectDb.Database;
                var command = objectDb.DbCommand;
                command.CommandTimeout = 3600;

                using (var reader = db.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        result.Add(Convert.ToDateTime(reader["winlostdate"]));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get licensee members information.
        /// </summary>
        /// <param name="licenseeName">Licensee name.</param>
        /// <param name="winlostDate">Win Lost Date.</param>
        /// <returns>List of username.</returns>
        private string MemberDailyGetLiceseeMembers(string licenseeName, DateTime winlostDate)
        {
            var actualKey = licenseeName.ToUpper() + "_DBA_GetMemberList";
            using (var objectDb = new ObjectDb(actualKey))
            {
                var db = objectDb.Database;
                var command = objectDb.DbCommand;
                command.CommandTimeout = 3600;
                objectDb.AddInParameter("winlostdate", DbType.Date, winlostDate);

                var dataSet = db.ExecuteDataSet(command);
                var dataTable = dataSet.Tables[0];
                return XmlConverter.ToXmlString(dataTable);
            }
        }

        /// <summary>
        /// Update member daily data.
        /// </summary>
        /// <param name="licenseeName">Licensee name.</param>
        /// <param name="xmlData">Data in xml format.</param>
        /// <param name="winlostDate">Win lost date.</param>
        private void MemberDailyUpdateWinLost(string licenseeName, string xmlData, DateTime winlostDate)
        {
            var actualKey = licenseeName.ToUpper() + "_DBA_Aff_MemberDailyUpdateData";
            using (var objectDb = new ObjectDb(actualKey))
            {
                var db = objectDb.Database;
                var command = objectDb.DbCommand;
                command.CommandTimeout = 3600;
                var parameters = new Hashtable();
                objectDb.AddInParameter("adminid", DbType.Int32, 1);
                objectDb.AddInParameter("xmldata", DbType.String, xmlData);
                objectDb.AddInParameter("winlostdate", DbType.Date, winlostDate);
                objectDb.AddOutParameter("errcode", DbType.Int32, 4);
                objectDb.AddOutParameter("errmsg", DbType.String, 4000);

                db.ExecuteNonQuery(command);

                var errMsg = objectDb.GetOuputParams()["errmsg"].ToString();
                if (!string.IsNullOrEmpty(errMsg))
                {
                    throw new Exception(errMsg);
                }
            }
        }

        #endregion
    }
}