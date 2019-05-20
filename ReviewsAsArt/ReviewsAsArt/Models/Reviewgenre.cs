using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Models
{
    public class Reviewgenre
    {
        public string Titel { get; set; }
        private int? _score;
        public int? Score { get { return _score; } set { if (value <= 0) { throw new ArgumentException("Score moet boven de 0 zijn."); } _score = value; } }
        public IEnumerable<Regellijn> Regels { get; set; }
        public IList<UserIdentity> GeblokkeerdeUserIDs { get; set; }
        public string AdminId { get; set; }
        public string AdminGebruikersNaam { get; set; }

        public Reviewgenre()
        {
            Regels = new List<Regellijn>();
            GeblokkeerdeUserIDs = new List<UserIdentity>();
        }
    }
}
