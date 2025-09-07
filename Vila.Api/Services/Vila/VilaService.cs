using Microsoft.EntityFrameworkCore;
using VILA.Api.Context;
using VILA.Api.Dtos;
using VILA.Api.Mappings;
using VILA.Api.Models;
using VILA.Api.Paging;
using VILA.Api.Utility;

namespace VILA.Api.Services.Vila
{
    public class VilaService : IVilaService
    {
        private readonly DataContext _context;
        public VilaService(DataContext context)
        {
            _context = context;
        }

        public bool Create(Models.Vila vila)
        {
            _context.Vilas.Add(vila);
            
            return Save();
        }

        public bool Delete(Models.Vila vila)
        {
           

            _context.Vilas.Remove(vila);

            return Save();
        }

        public List<VilaDto> GetAll()
        {
            return _context.Vilas.Select(x=> new VilaDto()
            {
                Id = x.Id,
                Address = x.Address,
                BuildDate = x.BuildDate.ToPersainDate(),
                City = x.City,
                Mobile = x.Mobile,
                State = x.State,
                Name = x.Name,
                SellPrice = x.SellPrice,
                DayPrice = x.DayPrice,
                Image = x.Image,
            }).ToList();

           
        }

        public Models.Vila GetById(int id)
        {
            return _context.Vilas.FirstOrDefault(x => x.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false; 

        }

        public VilaPaging Search(int pageId, string filter, int take)
        {
          IQueryable<Models.Vila> result = _context.Vilas.Include(x => x.Details);

            if (!string.IsNullOrEmpty(filter))
               result  = result.Where(x =>
                x.Name.Contains(filter) ||
                x.State.Contains(filter) ||
                x.City.Contains(filter) ||
                x.Address.Contains(filter)
                    );

            VilaPaging Paging = new();

            Paging.Generate(result,pageId,take);
            Paging.Filter = filter;
            Paging.Vilas = new();

            int skip = (pageId - 1) * take;
            var list = result.Skip(skip).Take(take).ToList().ToSearchDto();

            Paging.Vilas.AddRange(list);

            return Paging;


        }

        public VilaAdminPaging SearchVilaAdmin(int pageId, string filter, int take)
        {
            IQueryable<Models.Vila> result = _context.Vilas;
            if (!string.IsNullOrEmpty(filter))
                result = result.
                    Where(r => r.Name.Contains(filter) || r.State.Contains(filter) ||
                    r.City.Contains(filter) || r.Address.Contains(filter));


            VilaAdminPaging paging = new();
            paging.Generate(result, pageId, take);
            paging.Filter = filter;
            paging.VilaDtos = new();
            int skip = (pageId - 1) * take;
            var list = result.Skip(skip).Take(take).ToList().ToDto();

            paging.VilaDtos.AddRange(list);
            return paging;
        }

        public bool Update(Models.Vila vila)
        {
            _context.Vilas.Update(vila);

            return Save();
        }
    }
}
