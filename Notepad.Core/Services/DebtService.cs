using AutoMapper;
using Microsoft.AspNetCore.Http;
using Notepad.Core.Entities;
using Notepad.Core.Exceptions;
using Notepad.Core.Interfaces.Repositories;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Core.Services
{
    public class DebtService : IDebtService
    {
        private readonly IDebtRepository _debtRepository;
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;

        public DebtService(IDebtRepository debtRepository,
            IDebtorRepository debtorRepository,
            IMapper mapper)
        {
            _debtRepository = debtRepository;
            _debtorRepository = debtorRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateDebtRequest request)
        {
            Debtor debtor = await _debtorRepository.GetByIdWithIncludes(request.DebtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exist");
            }
            Debt debt = _mapper.Map<CreateDebtRequest, Debt>(request);
            debtor.Depts.Add(debt);
            debtor.TotalDebt = CalculateTotalDebt(debtor.Depts);
            await _debtorRepository.Update(debtor);
        }

        public async Task Delete(DeleteDebtRequest request)
        {
            Debtor debtor = await _debtorRepository.GetByIdWithIncludes(request.DebtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exist");
            }
            debtor.Depts = DeleteDebt(debtor.Depts, request.Amount);
            debtor.TotalDebt = CalculateTotalDebt(debtor.Depts);
            await _debtorRepository.Update(debtor);
        }

        private List<Debt> DeleteDebt(IEnumerable<Debt> debts, double amount)
        {
            double restOfAmount = amount;
            List<Debt> updatedDebts = new List<Debt>();
            IEnumerable<Debt> reversedDebts = debts.Reverse();
            foreach (var debt in reversedDebts)
            {
                if (restOfAmount != -1)
                {
                    if (restOfAmount - debt.Amount >= 0)
                    {
                        debt.IsRepaid = true;
                        restOfAmount -= debt.Amount;
                    }
                    else
                    {
                        debt.CreationDate = DateTimeOffset.UtcNow;
                        debt.Amount = Math.Round(debt.Amount - restOfAmount, 2);
                        debt.Description = debt.Description.Contains("(rest)") ? debt.Description: $"{debt.Description} (rest)";
                        restOfAmount = -1;
                    }
                }

                updatedDebts.Add(debt);
            }
            return updatedDebts;
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
            return Math.Round(totalDebt, 2);
        }
    }
}
