using System;

namespace Notepad.Core.Models.Requests
{
    public class DownloadReportRequest
    {
        public string DebtorId { get; set; }
        public DateTimeOffset BeginDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
