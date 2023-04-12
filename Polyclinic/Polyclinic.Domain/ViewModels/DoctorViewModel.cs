using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyclinic.Domain.ViewModels;

public class DoctorViewModel
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string MiddleName { get; set; }

    public string Specialization { get; set; }
}
