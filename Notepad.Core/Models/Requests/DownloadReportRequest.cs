using System;

namespace Notepad.Core.Models.Requests
{
    public class DownloadReportRequest
    {
        public string DebtorId { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
    }
}
