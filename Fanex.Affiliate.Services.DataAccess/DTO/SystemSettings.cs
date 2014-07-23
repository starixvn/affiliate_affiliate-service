namespace Fanex.Affiliate.Services.DataAccess.DTO
{
    /// <summary>
    /// DTO for SystemSettings
    /// This code should be refactor later by using criteria and DTO generator.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("T4", "1.0.0.0")]
    public class SystemSettings
    {
        /// <summary>
        /// Gets or sets the SID.
        /// </summary>
        /// <value>
        /// The SID value.
        /// </value>
        public int SID { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type value.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets SName.
        /// </summary>
        /// <value>
        /// The system name.
        /// </value>
        public string SName { get; set; }

        /// <summary>
        /// Gets or sets SValue.
        /// </summary>
        /// <value>
        /// The system value.
        /// </value>
        public string SValue { get; set; }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets Environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public string Environment { get; set; }
    }
}