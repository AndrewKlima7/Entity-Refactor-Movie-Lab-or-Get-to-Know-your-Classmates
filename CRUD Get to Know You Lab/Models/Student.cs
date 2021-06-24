using System;
using System.Collections.Generic;

#nullable disable

namespace CRUD_Get_to_Know_You_Lab.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string FavoriteFood { get; set; }
    }
}
