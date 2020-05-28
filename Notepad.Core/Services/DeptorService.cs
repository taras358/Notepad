using AutoMapper;
using Microsoft.AspNetCore.Http;
using Notepad.Core.DTOs;
using Notepad.Core.Entities;
using Notepad.Core.Exceptions;
using Notepad.Core.Interfaces.Repositories;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using IronPdf;
using System.Text;

namespace Notepad.Core.Services
{
    public class DeptorService : IDeptorService
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IDebtRepository _debtRepository;
        private readonly IMapper _mapper;

        public DeptorService(IDebtorRepository deptorRepository,
            IDebtRepository debtRepository,
            IMapper mapper)
        {
            _debtorRepository = deptorRepository;
            _debtRepository = debtRepository;
            _mapper = mapper;
        }

        private double CalculateTotalDebt(IEnumerable<Debt> debts)
        {
            if (debts is null || !debts.Any())
            {
                return default;
            }
            double totalDebt = debts
                     .Where(x => !x.IsRepaid)
                     .Select(x => x.Amount)
                     .Sum();
            return totalDebt;
        }

        public async Task<string> Create(CreateDebtorRequest request)
        {
            string fullName = $"{request.Name} {request.Surname}";
            Debtor debtor = await _debtorRepository.GetByFullName(fullName);
            if (debtor != null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor already exists");
            }
            debtor = _mapper.Map<CreateDebtorRequest, Debtor>(request);
            string debtorId = await _debtorRepository.Add(debtor);
            return debtorId;
        }

        public async Task Delete(string debtorId)
        {
            Debtor debtor = await _debtorRepository.GetById(debtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exists");
            }
            await _debtorRepository.Delete(debtor);
        }

        public async Task<DebtorsResponse> FindByFullName(string query)
        {
            List<Debtor> debtors = await _debtorRepository.FindByFullName(query);
            DebtorsResponse debtorsResponse = _mapper.Map<List<Debtor>, DebtorsResponse>(debtors);

            return debtorsResponse;
        }

        public async Task<DebtorsResponse> GetAll()
        {
            List<Debtor> debtors = await _debtorRepository.GetAllWithAllIncludes();
            DebtorsResponse debtorsResponse = _mapper.Map<List<Debtor>, DebtorsResponse>(debtors);
            foreach (var debtor in debtors)
            {
                DebtorResponse debtorResponse = debtorsResponse.Debtors.Find(x => x.Id == debtor.Id);
                debtorResponse.TotalDebt = CalculateTotalDebt(debtor.Debts);
                debtorResponse.Debts = debtorResponse.Debts
                           .OrderBy(x => x.IsRepaid)
                           .ThenByDescending(x => x.CreationDate)
                           .ToList();
            }
            return debtorsResponse;
        }

        public async Task<DebtorResponse> GetById(string debtorId)
        {
            Debtor debtor = await _debtorRepository.GetByIdWithIncludes(debtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exists");
            }
            DebtorResponse debtorResponse = _mapper.Map<Debtor, DebtorResponse>(debtor);
            debtorResponse.TotalDebt = CalculateTotalDebt(debtor.Debts);
            debtorResponse.Debts = debtorResponse.Debts
                            .OrderBy(x => x.IsRepaid)
                            .ThenByDescending(x => x.CreationDate)
                            .ToList();
            return debtorResponse;
        }

        public async Task<string> Update(UpdateDebtorRequest request)
        {
            Debtor debtor = await _debtorRepository.GetById(request.Id);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exists");
            }
            debtor.Name = request.Name;
            debtor.Surname = request.Surname;

            await _debtorRepository.Update(debtor);
            return debtor.Id;
        }

        public async Task<byte[]> DownloadExcelReport(DownloadReportRequest request)
        {
            ValidateRequest(request);
            Debtor debtor = await _debtorRepository.GetById(request.DebtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exists");
            }
            DateRangeDto rangeDto = ConvertDateRangeToValid(request);
            List<Debt> debts = await _debtRepository.FindByQuery(request.DebtorId, rangeDto);
            using (var package = new ExcelPackage())
            {
                int rowIndex = 1;
                int colIndex = 1;
                string wsName = $"{debtor.Name} {debtor.Surname}";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(wsName);

                worksheet.Cells[rowIndex, colIndex].Value = "Date";
                worksheet.Cells[rowIndex, ++colIndex].Value = "Amount (hrn)";
                worksheet.Cells[rowIndex, ++colIndex].Value = "Description";
                rowIndex++;
                SetHeaderStyle(worksheet);
                debts.Reverse();
                foreach (var debt in debts)
                {
                    if (!debt.IsRepaid)
                    {
                        colIndex = 1;
                        worksheet.Cells[rowIndex, colIndex].Value = debt.CreationDate.ToString("dd/MM/yyyy hh:mm");
                        worksheet.Cells[rowIndex, ++colIndex].Value = debt.Amount;
                        worksheet.Cells[rowIndex, ++colIndex].Value = debt.Description;
                        rowIndex++;
                    }
                }
                foreach (var debt in debts)
                {
                    if (debt.IsRepaid)
                    {
                        colIndex = 1;
                        worksheet.Cells[rowIndex, colIndex].Value = debt.CreationDate.ToString("dd/MM/yyyy hh:mm");
                        worksheet.Cells[rowIndex, ++colIndex].Value = debt.Amount;
                        worksheet.Cells[rowIndex, ++colIndex].Value = debt.Description;

                        worksheet.Cells[rowIndex, 1, rowIndex, colIndex].Style.Font.Strike = true;
                        rowIndex++;

                    }
                }
                SetBorderStyle(worksheet);

                return package.GetAsByteArray();
            }
        }

        public async Task<byte[]> DownloadPdfReport(DownloadReportRequest request)
        {
            Debtor debtor = await _debtorRepository.GetById(request.DebtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exists");
            }
            DateRangeDto rangeDto = ConvertDateRangeToValid(request);
            List<Debt> debts = await _debtRepository.FindByQuery(request.DebtorId, rangeDto);


            ////////////////
            ///

            //var document = new PdfDocument();
            //var page = document.AddPage();
            //var gfx = XGraphics.FromPdfPage(page);
            //var font = new XFont("OpenSans", 20, XFontStyle.Bold);

            //gfx.DrawString("Hello World!", font, XBrushes.Black, new XRect(20, 20, page.Width, page.Height), XStringFormats.Center);

            //using (var stream = new MemoryStream())
            //{
            //    document.Save(stream);
            //    return stream.ToArray();
            //}


            //////////////////////

            var sb = new StringBuilder();
            sb.Append($@"
                         <html>
                             <head>
                             </head>
                             <body>
                                 <div class='header' style='text-align: center;'><h2>{debtor.Name} {debtor.Surname}</h2></div>
                                    <table align='center' style='width: 90%; border-collapse: collapse;'>
                                     <tr>
                                         <th style='background-color: #e9ecef; border-right: 1px solid #dee2e6; border-color: #dee2e6; width: 30%; padding: 5px 10px; text-align: left;'>Date</th>
                                         <th style='background-color: #e9ecef; border-right: 1px solid #dee2e6; border-color: #dee2e6; width: 30%; padding: 5px 10px; text-align: left;'>Amount</th>
                                         <th style='background-color: #e9ecef; border-right: 1px solid #dee2e6; border-color: #dee2e6; width: 30%; padding: 5px 10px; text-align: left;'>Description</th>
                                     </tr>");
            debts.Reverse();
            foreach (var debt in debts)
            {
                if (!debt.IsRepaid)
                { 
                    sb.AppendFormat(@"<tr>
                                     <td style='border: 1px solid #dee2e6; padding: 5px 10px; text-align: left;'>{0}</td>
                                     <td style='border: 1px solid #dee2e6; padding: 5px 10px; text-align: left;'>{1}</td>
                                     <td style='border: 1px solid #dee2e6; padding: 5px 10px; text-align: left;'>{2}</td>
                                   </tr>", debt.CreationDate.ToString("dd/MM/yyyy hh:mm"), debt.Amount, debt.Description);
                }
            }
            foreach (var debt in debts)
            {
                if (debt.IsRepaid)
                {
                    sb.AppendFormat(@"<tr style='text-decoration: line-through'>
                                     <td style='border: 1px solid #dee2e6; padding: 5px 10px; text-align: left;'>{0}</td>
                                     <td style='border: 1px solid #dee2e6; padding: 5px 10px; text-align: left;'>{1}</td>
                                     <td style='border: 1px solid #dee2e6; padding: 5px 10px; text-align: left;'>{2}</td>
                                   </tr>", debt.CreationDate.ToString("dd/MM/yyyy hh:mm"), debt.Amount, debt.Description);
                }
            }

            sb.Append(@"
                                 </table>
                             </body>
                         </html>");

            string template = sb.ToString();
            var Renderer = new HtmlToPdf();
            var result = Renderer.RenderHtmlAsPdf(template).Stream.ToArray();
            return result;
        }

        private DateRangeDto ConvertDateRangeToValid(DownloadReportRequest request)
        {
            string _parsePattern = "MM/dd/yyyy";

            DateTimeOffset beginDate = DateTimeOffset.ParseExact(request.BeginDate, _parsePattern, CultureInfo.InvariantCulture);
            DateTimeOffset endDate = DateTimeOffset.ParseExact(request.EndDate, _parsePattern, CultureInfo.InvariantCulture);

            return new DateRangeDto
            {
                BeginDate = beginDate,
                EndDate = endDate
            };
        }

        private void ValidateRequest(DownloadReportRequest request)
        {
            //if(request.EndDate < request.BeginDate)
            //{
            //    throw new AppCustomException(StatusCodes.Status400BadRequest, "Incorrent date range");
            //}
        }

        private void SetBorderStyle(ExcelWorksheet worksheet)
        {
            ExcelRange body = worksheet.Cells[worksheet.Dimension.Address];

            body.AutoFitColumns();
            body.Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Gray);

            body.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            body.Style.Border.Left.Color.SetColor(System.Drawing.Color.Gray);
            body.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            body.Style.Border.Right.Color.SetColor(System.Drawing.Color.Gray);
        }

        private void SetHeaderStyle(ExcelWorksheet worksheet)
        {
            var headerRow = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns]?.Style;
            if (headerRow != null)
            {
                headerRow.Font.Bold = true;
                headerRow.Fill.PatternType = ExcelFillStyle.Solid;
                headerRow.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }
        }
    }
}