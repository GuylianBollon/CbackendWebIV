using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        private int? _score;
        public Reviewgenre Rg { get; set; }
        public int? Score { get { return _score; } set { if (value < 0) { throw new ArgumentException("Score moet 0 of hoger zijn."); } if (value > this.Rg.Score) { throw new ArgumentException("Score moet onder of gelijk aan het maximum zijn"); } _score = value; } }
        public IEnumerable<Commentaar> Commentaars { get; set; }
        public Werk Werk { get; set; }

        public Review()
        {
            Commentaars = new List<Commentaar>();
        }
    }
}
