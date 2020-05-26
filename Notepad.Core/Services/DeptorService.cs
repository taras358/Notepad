using AutoMapper;
using Microsoft.AspNetCore.Http;
using Notepad.Core.Entities;
using Notepad.Core.Exceptions;
using Notepad.Core.Interfaces.Repositories;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Core.Services
{
    public class DeptorService : IDeptorService
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IDebtRepository _debtRepository;
        private readonly IMapper _mapper;

        public DeptorService(IDebtorRepository deptorRepository,
            IDebtRepository  debtRepository,
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

        public async Task<byte[]> DownloadReport(DownloadReportRequest request)
        {
            ValidateRequest(request);
            Debtor debtor = await _debtorRepository.GetById(request.DebtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exists");
            }
            List<Debt> debts = await _debtRepository.FindByQuery(request.DebtorId, request.BeginDate, request.EndDate);
            using (var package = new ExcelPackage())
            {
                int rowIndex = 1;
                int colIndex = 1;
                bool isHeaderSet = false;
                string wsName = $"{debtor.Name} {debtor.Surname}";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(wsName);

                foreach (var debt in debtor.Debts)
                {

                }

            }
            return default;
        }

        private void ValidateRequest(DownloadReportRequest request)
        {
            if(request.EndDate < request.BeginDate)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Incorrent date range");
            }
        }
    }
}