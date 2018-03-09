namespace WeatherApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        public Account()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Username = "";
            this.Password = "";
            this.Name = "";
            this.Lastname = "";
            this.Age = 0;
            this.Status = UserStatus.Pending; 
        }
      
        [Required,StringLength(50)]
        public string Id { get; set; }
    
        [Required,StringLength(30)]
        public string Name { get; set; }
       
        [Required,StringLength(30)]
        public string Lastname { get; set; }

        [Required]
        public int Age { get; set; }
     
        [Required, StringLength(40), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
     
        [Required(ErrorMessage = "Username is not valid"), StringLength(30)]
        public string Username { get; set; }

        [Required(ErrorMessage="Password is not valid"), MinLength(7), StringLength(50), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public UserStatus Status { get; set; }
    }


    public enum UserStatus
    {
        Pending = 0,
        Registrated=1,
        Admin=2
    }
}
