using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class VitalSignRepository: IVitalSignRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VitalSignRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<VitalSign> CreateVitalSign(VitalSign vitalSign)
        {
            await _applicationDbContext.VitalSigns.AddAsync(vitalSign);
            await _applicationDbContext.SaveChangesAsync();
            return vitalSign;
        }
    }
}
