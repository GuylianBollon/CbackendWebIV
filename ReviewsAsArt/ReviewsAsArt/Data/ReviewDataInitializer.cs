using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Data
{
    public class ReviewDataInitializer
    {
        private readonly ApplicationDbContext _adc;

        public ReviewDataInitializer(ApplicationDbContext adc)
        {
            _adc = adc;
        }

        public void InitializeData()
        {
            _adc.Database.EnsureDeleted();
            if (_adc.Database.EnsureCreated())
            {
                //seeding the database with recipes, see DBContext               
            }
        }
    }
}
