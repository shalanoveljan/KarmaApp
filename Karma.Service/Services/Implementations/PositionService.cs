using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Data.Repositories;
using Karma.Service.Exceptions;
using Karma.Service.Responses;
using Karma.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Implementations
{
    public class PositionService : IPositionService
    {

        readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository PositionRepository)
        {
            _positionRepository = PositionRepository;
        }

        public async Task CreateAsync(PositionPostDto dto)
        {
            Position Position = new Position();
            Position.PositionName = dto.PositionName;
            await _positionRepository.AddAsync(Position);
            await _positionRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<PositionGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<PositionGetDto> pagginatedResponse = new PagginatedResponse<PositionGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _positionRepository.GetQuery(x => !x.iSDeleted)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new PositionGetDto { PositionName = x.PositionName, Id = x.Id, CreatedAt = x.CreateAt })
                .ToListAsync();


            return pagginatedResponse;
        }

        public async Task<PositionGetDto> GetAsync(int id)
        {
            Position? Position = await _positionRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Position == null)
            {
                throw new ItemNotFoundException("Position not Found");
            }

            PositionGetDto PositionGetDto=new PositionGetDto
            {
            Id = Position.Id,
            PositionName=Position.PositionName,
            CreatedAt= Position.CreateAt
            };

            return PositionGetDto;


        }

        public async Task RemoveAsync(int id)
        {
            Position? Position = await _positionRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Position == null)
            {
                throw new ItemNotFoundException("Position not Found");
            }

            Position.iSDeleted = true;
            await _positionRepository.UpdateAsync(Position);
            await _positionRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, PositionPostDto dto)
        {
            Position? Position = await _positionRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Position == null)
            {
                throw new ItemNotFoundException("Position not Found");
            }

            //if (!(await _positionRepository.AnyAsync(x => x.Id == id)))
            //{
            //    ModelState.AddModelError("Id", "Agilli ol");
            //    return View();
            //}

            Position.PositionName = dto.PositionName;
            await _positionRepository.UpdateAsync(Position);
            await _positionRepository.SaveChangesAsync();
        }
    }
}
