using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    Speciality? GetSpecialityById(Guid id);
    void SaveDoctor(Doctor doctor);
}
