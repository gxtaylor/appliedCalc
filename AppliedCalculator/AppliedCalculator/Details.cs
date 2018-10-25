using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppliedCalculator
{
    public class Details
    {
        public DateTime PolicyStart { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public DateTime DOB { get; set; }
        public List<Claim> Claims { get; set; }
    }

    public class Claim
    {
        public int ClaimId { get; set; }
        public DateTime ClaimDate { get; set; }
    }

}
