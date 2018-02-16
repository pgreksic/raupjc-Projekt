using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelBin.Data
{
    public class PixelBinRepository : IPixelBinRepository
    {
        private readonly PixelBinDbContext _dbContext;

        public PixelBinRepository(PixelBinDbContext dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
