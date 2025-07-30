using VILA.Api.Context;
using VILA.Api.Dtos;
using VILA.Api.Mappings;

namespace VILA.Api.Services.Detail
{

        public class DetailService : IDetailService
        {
            private readonly DataContext _context;
            public DetailService(DataContext context)
            {
                _context = context;
            }

            public bool Create(Models.Detail model)
            {
                _context.Details.Add(model);
                return Save();    
            }

            public bool Delete(Models.Detail model)
            {
                _context.Remove(model);
               return Save();
            }

            public List<DetailDto> GetAllVilaDetails(int vilaId)
            {
              return _context.Details.Where(x=>x.VilaId ==vilaId).Select(x=> new DetailDto()
               {
                   VilaId = x.VilaId,
                   DetailId = x.Id,
                   Value = x.Value,
                   What = x.What,
                   
               }).ToList();
            }

            public Models.Detail getById(int Id)
            {
                return _context.Details.FirstOrDefault(x => x.Id == Id);
            }

            public bool Save()
            {
               return _context.SaveChanges() >= 0  ? true : false;
            }

            public bool Update(Models.Detail model)
            {
                _context.Update(model);
                return Save();
            }
        }
}



      

