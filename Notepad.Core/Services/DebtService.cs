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
            debtor.Debts.Add(debt);
            await _debtorRepository.Update(debtor);
        }

        public async Task Delete(DeleteDebtRequest request)
        {
            Debtor debtor = await _debtorRepository.GetByIdWithIncludes(request.DebtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exist");
            }
            debtor.Debts = DeleteDebt(debtor.Debts, request.Amount);
            await _debtorRepository.Update(debtor);
        }

        public async Task Update(UpdateDebtRequest request)
        {
            Debtor debtor = await _debtorRepository.GetById(request.DebtorId);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exist");
            }
            Debt debt = _mapper.Map<UpdateDebtRequest, Debt>(request);
            await _debtRepository.Update(debt);
        }

        private List<Debt> DeleteDebt(IEnumerable<Debt> debts, double amount)
        {
            double restOfAmount = amount;
            Debt restDebt = null;
            List<Debt> updatedDebts = new List<Debt>();
            IEnumerable<Debt> reversedDebts = debts
                                                .OrderBy(x => x.CreationDate);
            foreach (var debt in reversedDebts)
            {
                if (debt.IsRepaid)
                {
                    updatedDebts.Add(debt);
                    continue;
                }
                if (restOfAmount > 0)
                {
                    if (restOfAmount - debt.Amount >= 0)
                    {
                        debt.IsRepaid = true;
                        restOfAmount -= debt.Amount;
                    }
                    else
                    {
                        restDebt = new Debt
                        {
                            Amount = debt.Amount - restOfAmount,
                            Description = debt.Description.Contains("(rest)") ? debt.Description : $"{debt.Description} (rest)"
                        };
                    }
                    debt.IsRepaid = true;
                }

                updatedDebts.Add(debt);
            }
            if (restDebt != null)
            {
                updatedDebts.Add(restDebt);
            }
            return updatedDebts;
        }
    }
}
