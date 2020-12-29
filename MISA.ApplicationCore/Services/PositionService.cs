using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class PositionService : BaseService<Position>, IPositionService
    {
        public PositionService(IPositionRepository positionRepository) : base(positionRepository)
        {

        }
    }
}
