using Microsoft.EntityFrameworkCore;
using ReviewsAsArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Data.Repositories
{
    public class ReviewGenreRepository : IReviewGenreRepository
    {
        private readonly DbSet<Reviewgenre> _rg;
        private readonly ApplicationDbContext _adc;

        public ReviewGenreRepository(ApplicationDbContext adc)
        {
            _rg = adc.Reviewgenres;
            _adc = adc;
        }

        public void AddReviewGenre(Reviewgenre rg)
        {
            _rg.Add(rg);
        }

        public void BlokkeerUser(User user, Reviewgenre rg)
        {
            _rg.Find(rg).GeblokkeerdeUserIDs.Add(new UserIdentity {UserID = user.Id });
        }

        public void DeblokkeerUser(User user, Reviewgenre rg)
        {
            rg.GeblokkeerdeUserIDs.Remove(new UserIdentity {UserID = user.Id });
        }

        public string GetAdminNaam(Reviewgenre rg)
        {
            return rg.AdminGebruikersNaam;
        }

        public IEnumerable<Reviewgenre> GetReviewgenres()
        {
            return _rg.ToList();
        }

        public bool IsAdmin(User user, Reviewgenre rg)
        {
            return _rg.Find(rg).AdminId.Equals(user.Id);
        }

        public bool IsGeblokkeerd(User user, Reviewgenre rg)
        {
            return _rg.FirstOrDefault(r => r == rg).GeblokkeerdeUserIDs.Contains(new UserIdentity {UserID = user.Id });
        }

        public void SaveChanges()
        {
            _adc.SaveChanges();
        }
    }
}
