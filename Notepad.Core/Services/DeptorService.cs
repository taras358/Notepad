using AutoMapper;
using Microsoft.AspNetCore.Http;
using Notepad.Core.Entities;
using Notepad.Core.Exceptions;
using Notepad.Core.Interfaces.Repositories;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Core.Services
{
    public class DeptorService : IDeptorService
    {
        private readonly IDebtorRepository _deptorRepository;
        private readonly IMapper _mapper;

        public DeptorService(IDebtorRepository deptorRepository,
            IMapper mapper)
        {
            _deptorRepository = deptorRepository;
            _mapper = mapper;
        }
        public async Task<string> Create(CreateDebtorRequest request)
        {
            string fullName = $"{request.Name} {request.Surname}";
            Debtor debtor = await _deptorRepository.GetByFullName(fullName);
            if (debtor != null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor already exists");
            }
            debtor = _mapper.Map<CreateDebtorRequest, Debtor>(request);
            string debtorId = await _deptorRepository.Add(debtor);
            return debtorId;
        }

        public async Task<DebtorsResponse> FindByFullName(string query)
        {
            List<Debtor> debtors = await _deptorRepository.FindByFullName(query);
            DebtorsResponse debtorsResponse = _mapper.Map<List<Debtor>, DebtorsResponse>(debtors);

            return debtorsResponse;
        }

        public async Task<DebtorsResponse> GetAll()
        {
            List<Debtor> debtors = await _deptorRepository.GetAll();
            DebtorsResponse debtorsResponse = _mapper.Map<List<Debtor>, DebtorsResponse>(debtors);

            return debtorsResponse;
        }

        public async Task<DebtorResponse> GetById(string debtorId)
        {
            Debtor debtor = await _deptorRepository.GetByIdWithIncludes(debtorId);
            DebtorResponse debtorResponse = _mapper.Map<Debtor, DebtorResponse>(debtor);
            return debtorResponse;
        }

        public async Task<string> Update(UpdateDebtorRequest request)
        {
            string fullName = $"{request.Name} {request.Surname}";
            Debtor debtor = await _deptorRepository.GetByFullName(fullName);
            if (debtor is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, "Debtor does not exists");
            }
            debtor = _mapper.Map<UpdateDebtorRequest, Debtor>(request);
            await _deptorRepository.Update(debtor);
            return debtor.Id;
        }
    }
}
