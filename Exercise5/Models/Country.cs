using System;
using System.Collections.Generic;

namespace Exercise5.Models;

public partial class Country
{
    public int IdCountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Trip> ColTrips { get; set; } = new List<Trip>();
}
