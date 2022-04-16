﻿
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class CompanyService : GenericService<Company>, ICompanyService {
        public CompanyService(ApplicationDbContext applicationDbContext, IHttpContextAccessor a) : base(applicationDbContext, a) {
        }

        public async Task<DataTableResult<Company>> GetCompanyPage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<Company> list = await FindAllAsync(e => (e.Name.Contains(searchKey) || e.Description.Contains(searchKey) 
                || searchKey == null) && !e.IsDeleted, start, length);
            return new DataTableResult<Company> {
                Data = list,
                RecordsFiltered = all,
                RecordsTotal = all,
                Draw = param.Draw
            };
        }
    }
}
