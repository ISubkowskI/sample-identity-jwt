﻿namespace Ae.Sample.Identity.Ui.Settings
{
    public sealed class IdentityApiOptions
    {
        public const string IdentityApi = "IdentityApi";

        /// <summary>
        /// z.B. "http://localhost:5005"
        /// </summary>
        public string ApiUrl { get; set; } = String.Empty;

        /// <summary>
        /// z.B. "/api/v1"
        /// </summary>
        public string ApiBasePath { get; set; } = String.Empty;
    }
}
