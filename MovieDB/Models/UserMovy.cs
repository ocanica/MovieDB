//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieDB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserMovy
    {
        public string imdbID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public Nullable<int> Rating { get; set; } = 3;
        public Nullable<bool> Favourite { get; set; } = false;
        public string Poster { get; set; }
    }
}
