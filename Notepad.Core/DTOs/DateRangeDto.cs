using System;

namespace Notepad.Core.DTOs
{
    public class DateRangeDto
    {
        public DateTimeOffset BeginDate{ get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
